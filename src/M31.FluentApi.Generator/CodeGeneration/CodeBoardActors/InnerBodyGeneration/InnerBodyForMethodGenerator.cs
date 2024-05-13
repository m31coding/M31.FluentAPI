// Closing brace should be followed by blank line
#pragma warning disable SA1513

using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyForMethodGenerator : InnerBodyGeneratorBase<MethodSymbolInfo>
{
    internal InnerBodyForMethodGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
    }

    protected override string SymbolType(MethodSymbolInfo symbolInfo)
    {
        return "Method";
    }

    protected override void GenerateInnerBodyWithoutReflection(MethodSymbolInfo symbolInfo)
    {
        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(
            string instancePrefix,
            IReadOnlyCollection<Parameter> outerMethodParameters,
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
                    .Append($"({string.Join(", ", outerMethodParameters.Select(CreateArgument))});")
                    .ToString(),
            };
        }

        static string CreateArgument(Parameter outerMethodParameter)
        {
            // ref/in/out semester
            return $"{outerMethodParameter.ParameterAnnotations?.ToCallsiteAnnotations()}{outerMethodParameter.Name}";
        }
    }

    protected override void GenerateInnerBodyWithReflection(MethodSymbolInfo symbolInfo, string infoFieldName)
    {
        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(
            string instancePrefix,
            IReadOnlyCollection<Parameter> outerMethodParameters,
            string? returnType)
        {
            return outerMethodParameters.Any(p =>
                p.HasAnnotation(ParameterKinds.Ref) || p.HasAnnotation(ParameterKinds.Out))
                ? BuildReflectionCodeWithRefAndOutParameters(
                    infoFieldName,
                    instancePrefix,
                    symbolInfo.GenericInfo,
                    outerMethodParameters,
                    returnType)
                : BuildDefaultReflectionCode(
                    infoFieldName,
                    instancePrefix,
                    symbolInfo.GenericInfo,
                    outerMethodParameters,
                    returnType);
        }
    }

    private List<string> BuildReflectionCodeWithRefAndOutParameters(
        string infoFieldName,
        string instancePrefix,
        GenericInfo? genericInfo,
        IReadOnlyCollection<Parameter> outerMethodParameters,
        string? returnType)
    {
        bool returnResult = !IsNoneOrVoid(returnType);
        string variableName = returnResult ? GetVariableName("result", outerMethodParameters) : string.Empty;
        string suppressNullability = returnResult ? "!" : string.Empty;

        List<string> lines = new List<string>();

        // object?[] args = new object?[] { semester };
        lines.Add(
            $"object?[] args = new object?[] {{ {string.Join(", ", outerMethodParameters.Select(GetArgument))} }};");

        // semesterMethodInfo.Invoke(createStudent.student, args) or
        // semesterMethodInfo.MakeGenericInfo(typeof(T1), typeof(T2)).Invoke(createStudent.student, args) or
        // string result = (string) toJsonMethodInfo.Invoke(createStudent.student, args)
        lines.Add(CodeBoard.NewCodeBuilder()
            .Append($"{returnType} {variableName} = ({returnType}) ", returnResult)
            .Append($"{infoFieldName}.{MakeGenericMethod(genericInfo)}")
            .Append($"Invoke({instancePrefix}{CodeBoard.Info.ClassInstanceName}, args){suppressNullability};")
            .ToString());

        foreach (var parameter in outerMethodParameters.Select((p, i) => new { Value = p, Index = i }))
        {
            if (parameter.Value.HasAnnotation(ParameterKinds.Ref) || parameter.Value.HasAnnotation(ParameterKinds.Out))
            {
                lines.Add(AssignValueToArgument(parameter.Index, parameter.Value));
            }
        }

        if (returnResult)
        {
            lines.Add($"return {variableName};");
        }

        return lines;

        string GetArgument(Parameter parameter)
        {
            return parameter.HasAnnotation(ParameterKinds.Out) ? "null" : parameter.Name;
        }

        string AssignValueToArgument(int index, Parameter parameter)
        {
            // For out and ref parameters, the invoke method writes the result values into the args array.
            // semester = (int) args[0];
            return $"{parameter.Name} = ({parameter.Type}) args[{index}]!;";
        }
    }

    private string GetVariableName(string desiredVariableName, IReadOnlyCollection<Parameter> outerMethodParameters)
    {
        string variableName = desiredVariableName;
        int i = 2;

        IEnumerable<string> reservedVariableNames = outerMethodParameters
            .Select(p => p.Name)
            .Concat(new string[] { CodeBoard.Info.ClassInstanceName, CodeBoard.Info.BuilderInstanceName });

        // ReSharper disable once PossibleMultipleEnumeration
        while (reservedVariableNames.Contains(variableName))
        {
            variableName = $"{variableName}{i++}";
        }

        return variableName;
    }

    private List<string> BuildDefaultReflectionCode(
        string infoFieldName,
        string instancePrefix,
        GenericInfo? genericInfo,
        IReadOnlyCollection<Parameter> outerMethodParameters,
        string? returnType)
    {
        bool returnResult = !IsNoneOrVoid(returnType);
        string suppressNullability = returnResult ? "!" : string.Empty;

        return new List<string>()
        {
            // semesterMethodInfo.Invoke(createStudent.student, new object[] { semester }); or
            // semesterMethodInfo.MakeGenericMethod(typeof(T1), typeof(T2))
            //     .Invoke(createStudent.student, new object[] { semester });
            CodeBoard.NewCodeBuilder()
                .Append($"return ({returnType}) ", returnResult)
                .Append($"{infoFieldName}.{MakeGenericMethod(genericInfo)}")
                .Append($"Invoke({instancePrefix}{CodeBoard.Info.ClassInstanceName}, ")
                .Append($"new object?[] {{ {string.Join(", ", outerMethodParameters.Select(p => p.Name))} }})" +
                        $"{suppressNullability};")
                .ToString(),
        };
    }

    private static string MakeGenericMethod(GenericInfo? genericInfo)
    {
        if (genericInfo == null)
        {
            return string.Empty;
        }

        return $"MakeGenericMethod({string.Join(", ", genericInfo.ParameterStrings.Select(p => $"typeof({p})"))}).";
    }

    protected override void InitializeInfoField(string fieldName, MethodSymbolInfo symbolInfo)
    {
        Method staticConstructor = CodeBoard.StaticConstructor!;
        const string indentation = CodeBuilder.OneLevelIndentation;

        string typeArguments =
            @$"new Type[] {{ {string.Join(", ",
                symbolInfo.ParameterInfos.Select(CreateMethodParameter))} }}";

        // withNameMethodInfo = typeof(Student<T1, T2>).GetMethod(
        //     "WithName",
        //     0,                                                   -> generic parameter count
        //     BindingFlags.Instance | BindingFlags.NonPublic,
        //     null                                                 -> binder
        //     new Type[] { typeof(string) },
        //     null)!;                                              -> modifiers
        //
        // Generic types are created via Type.MakeGenericMethodParameter(int position). In addition, a ref type is
        // specified via MakeByRefType().
        staticConstructor.AppendBodyLine($"{fieldName} = " +
                                         $"typeof({CodeBoard.Info.FluentApiClassNameWithTypeParameters}).GetMethod(");
        staticConstructor.AppendBodyLine($"{indentation}\"{symbolInfo.Name}\",");
        staticConstructor.AppendBodyLine($"{indentation}{GetGenericParameterCount(symbolInfo.GenericInfo)},");
        staticConstructor.AppendBodyLine($"{indentation}{InfoFieldBindingFlagsArgument(symbolInfo)},");
        staticConstructor.AppendBodyLine($"{indentation}null,");
        staticConstructor.AppendBodyLine($"{indentation}{typeArguments},");
        staticConstructor.AppendBodyLine($"{indentation}null)!;");

        CodeBoard.CodeFile.AddUsing("System");

        static string CreateMethodParameter(ParameterSymbolInfo parameterInfo)
        {
            return parameterInfo.IsGenericParameter
                ? $"Type.MakeGenericMethodParameter({parameterInfo.GenericTypeParameterPosition!.Value})" +
                  $"{MakeByRefType(parameterInfo.ParameterKinds)}"
                : $"typeof({parameterInfo.TypeForCodeGeneration}){MakeByRefType(parameterInfo.ParameterKinds)}";

            static string MakeByRefType(ParameterKinds parameterKinds)
            {
                return parameterKinds.HasFlag(ParameterKinds.In) ||
                       parameterKinds.HasFlag(ParameterKinds.Out) ||
                       parameterKinds.HasFlag(ParameterKinds.Ref)
                    ? ".MakeByRefType()"
                    : string.Empty;
            }
        }

        static string GetGenericParameterCount(GenericInfo? genericInfo)
        {
            return genericInfo == null ? "0" : genericInfo.ParameterCount.ToString();
        }
    }

    private static bool IsNoneOrVoid(string? returnType)
    {
        return returnType is null or "void";
    }
}