// Closing brace should be followed by blank line
#pragma warning disable SA1513

using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class LineForMethodGenerator : LineGeneratorBase<MethodSymbolInfo>
{
    internal LineForMethodGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
    }

    protected override string SymbolType(MethodSymbolInfo symbolInfo)
    {
        return "Method";
    }

    protected override void GenerateLineWithoutReflection(MethodSymbolInfo symbolInfo)
    {
        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(string instancePrefix, IReadOnlyCollection<Parameter> outerMethodParameters)
        {
            return new List<string>()
            {
                // createStudent.student.InSemester<T1, T2>(semester);
                $"{instancePrefix}{CodeBoard.Info.ClassInstanceName}.{symbolInfo.Name}" +
                symbolInfo.GenericInfo?.ParameterListInAngleBrackets +
                $"({string.Join(", ", outerMethodParameters.Select(CreateArgument))});",
            };
        }

        static string CreateArgument(Parameter outerMethodParameter)
        {
            // ref/in/out semester
            return $"{outerMethodParameter.ParameterAnnotations?.ToCallsiteAnnotations()}{outerMethodParameter.Name}";
        }
    }

    protected override void GenerateLineWithReflection(MethodSymbolInfo symbolInfo, string infoFieldName)
    {
        CallMethodCode callMethodCode = new CallMethodCode(BuildCallMethodCode, CodeBoard.NewLineString);
        CodeBoard.InnerBodyCreationDelegates.AssignCallMethodCode(symbolInfo, callMethodCode);

        List<string> BuildCallMethodCode(string instancePrefix, IReadOnlyCollection<Parameter> outerMethodParameters)
        {
            return outerMethodParameters.Any(p =>
                p.HasAnnotation(ParameterKinds.Ref) || p.HasAnnotation(ParameterKinds.Out))
                ? BuildReflectionCodeWithParameterModifiers(infoFieldName, instancePrefix, outerMethodParameters)
                : BuildDefaultReflectionCode(infoFieldName, instancePrefix, outerMethodParameters);
        }
    }

    private List<string> BuildReflectionCodeWithParameterModifiers(
        string infoFieldName,
        string instancePrefix,
        IReadOnlyCollection<Parameter> outerMethodParameters)
    {
        List<string> lines = new List<string>();

        // object?[] args = new object?[] { semester };
        lines.Add(
            $"object?[] args = new object?[] {{ {string.Join(", ", outerMethodParameters.Select(GetArgument))} }};");

        // semesterMethodInfo.Invoke(createStudent.student, args)
        lines.Add($"{infoFieldName}.Invoke({instancePrefix}{CodeBoard.Info.ClassInstanceName}, args);");

        foreach (var parameter in outerMethodParameters.Select((p, i) => new { Value = p, Index = i }))
        {
            if (parameter.Value.HasAnnotation(ParameterKinds.Ref) || parameter.Value.HasAnnotation(ParameterKinds.Out))
            {
                lines.Add(AssignValueToArgument(parameter.Index, parameter.Value));
            }
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

    private List<string> BuildDefaultReflectionCode(
        string infoFieldName,
        string instancePrefix,
        IReadOnlyCollection<Parameter> outerMethodParameters)
    {
        return new List<string>()
        {
            // semesterMethodInfo.Invoke(createStudent.student, new object[] { semester });
            $"{infoFieldName}.Invoke({instancePrefix}{CodeBoard.Info.ClassInstanceName}, " +
            $"new object?[] {{ {string.Join(", ", outerMethodParameters.Select(p => p.Name))} }});",
        };
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
        //     BindingFlags.Instance | BindingFlags.NonPublic,
        //     new Type[] { typeof(string) })!;   or   new Type[] { Type.MakeGenericMethodParameter(0) })!;
        staticConstructor.AppendBodyLine($"{fieldName} = " +
                                         $"typeof({CodeBoard.Info.FluentApiClassNameWithTypeParameters}).GetMethod(");
        staticConstructor.AppendBodyLine($"{indentation}\"{symbolInfo.Name}\",");
        staticConstructor.AppendBodyLine($"{indentation}{InfoFieldBindingFlagsArgument(symbolInfo)},");
        staticConstructor.AppendBodyLine($"{indentation}{typeArguments})!;");

        CodeBoard.CodeFile.AddUsing("System");

        static string CreateMethodParameter(ParameterSymbolInfo parameterInfo)
        {
            if (parameterInfo.IsGenericParameter)
            {
                return $"Type.MakeGenericMethodParameter({parameterInfo.GenericTypeParameterPosition!.Value})";
            }
            else
            {
                return $"typeof({parameterInfo.TypeForCodeGeneration})";
            }
        }
    }
}