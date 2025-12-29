// Closing brace should be followed by blank line
#pragma warning disable SA1513

using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.Generics; // todo: remove if not needed

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyForMethodGenerator : InnerBodyGeneratorBase<MethodSymbolInfo>
{
    internal InnerBodyForMethodGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
    }

    protected override void GenerateInnerBodyForPublicSymbol(MethodSymbolInfo symbolInfo)
    {
        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(
            string instancePrefix,
            IReadOnlyCollection<Parameter> outerMethodParameters,
            ReservedVariableNames reservedVariableNames,
            string? returnType)
        {
            return new List<string>()
            {
                // createStudent.student.InSemester<T1, T2>(semester); or
                // return createStudent.student.ToJson();
                CodeBoard.NewCodeBuilder()
                    .Append($"return ", !IsNoneOrVoid(returnType))
                    .Append($"{instancePrefix}{CodeBoard.Info.ClassInstanceName}.{symbolInfo.Name}")
                    .Append(symbolInfo.GenericInfo?.ParameterListInAngleBrackets)
                    .Append($"({string.Join(", ", outerMethodParameters.Select(CreateParameter))});")
                    .ToString(),
            };
        }
    }

    protected override void GenerateInnerBodyForPrivateSymbol(MethodSymbolInfo symbolInfo)
    {
        string callMethodName = $"Call{symbolInfo.NameInPascalCase}";

        // [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "WithName")]
        // private static extern void CallWithName(Student student, string name);
        MethodSignature unsafeAccessorSignature =
            MethodSignature.Create(symbolInfo.ReturnType, callMethodName, null, true);
        // todo: generic constraints handled correctly for generic return type?
        unsafeAccessorSignature.AddModifiers("private", "static", "extern");

        unsafeAccessorSignature.AddParameter(
            new Parameter(CodeBoard.Info.FluentApiClassNameWithTypeParameters, CodeBoard.Info.ClassInstanceName));
        CodeBuildingHelpers.AddGenericParameters(unsafeAccessorSignature, CodeBoard.Info.GenericInfo); // todo: test generic case

        List<Parameter> parameters = CodeBuildingHelpers.CreateParameters(symbolInfo.ParameterInfos);
        parameters.ForEach(unsafeAccessorSignature.AddParameter);
        CodeBuildingHelpers.AddGenericParameters(unsafeAccessorSignature, symbolInfo.GenericInfo);

        unsafeAccessorSignature.AddAttribute(
            $"[UnsafeAccessor(UnsafeAccessorKind.Method, Name = \"{symbolInfo.NameInPascalCase}\")]");
        CodeBoard.BuilderClass.AddMethodSignature(unsafeAccessorSignature);

        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(
            string instancePrefix,
            IReadOnlyCollection<Parameter> outerMethodParameters,
            ReservedVariableNames reservedVariableNames,
            string? returnType)
        {
            string firstArgument = $"{instancePrefix}{CodeBoard.Info.ClassInstanceName}";
            IEnumerable<string> otherArguments = outerMethodParameters.Select(CreateParameter);

            return new List<string>()
            {
                // CallWithName(student, name); // todo: better example
                CodeBoard.NewCodeBuilder()
                    .Append("return ", !IsNoneOrVoid(returnType))
                    .Append($"{callMethodName}")
                    .Append(symbolInfo.GenericInfo?.ParameterListInAngleBrackets)
                    .Append($"({string.Join(", ", new[] { firstArgument }.Concat(otherArguments))});")
                    .ToString(),
            };
        }
    }

    private static string CreateParameter(Parameter outerMethodParameter)
    {
        // ref/in/out semester
        return $"{outerMethodParameter.ParameterAnnotations?.ToCallsiteAnnotations()}{outerMethodParameter.Name}";
    }

    private static bool IsNoneOrVoid(string? returnType)
    {
        return returnType is null or "void";
    }
}