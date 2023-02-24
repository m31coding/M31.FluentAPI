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
        // createStudent.student.InSemester(semester);
        CallMethodCode callMethodCode =
            new CallMethodCode((instancePrefix, values) =>
                $"{instancePrefix}{CodeBoard.Info.ClassInstanceName}.{symbolInfo.Name}({string.Join(", ", values)});");
        CodeBoard.MethodToCallMethodCode[CreateMethodIdentity(symbolInfo)] = callMethodCode;
    }

    protected override void GenerateLineWithReflection(MethodSymbolInfo symbolInfo, string infoFieldName)
    {
        // semesterMethodInfo.Invoke(createStudent.student, new object[] { semester });
        CallMethodCode callMethodCode =
            new CallMethodCode((instancePrefix, values) =>
                $"{infoFieldName}.Invoke({instancePrefix}{CodeBoard.Info.ClassInstanceName}, " +
                $"new object[] {{ {string.Join(", ", values)} }});");
        CodeBoard.MethodToCallMethodCode[CreateMethodIdentity(symbolInfo)] = callMethodCode;
    }

    private static MethodIdentity CreateMethodIdentity(MethodSymbolInfo methodSymbolInfo)
    {
        return MethodIdentity.Create(methodSymbolInfo.Name,
            methodSymbolInfo.ParameterInfos.Select(i => i.TypeForCodeGeneration));
    }

    protected override void InitializeInfoField(string fieldName, MethodSymbolInfo symbolInfo)
    {
        Method staticConstructor = CodeBoard.StaticConstructor!;
        const string indentation = CodeBuilder.OneLevelIndentation;

        string typeArguments =
            @$"new Type[] {{ {string.Join(", ",
                symbolInfo.ParameterInfos.Select(i => $"typeof({i.TypeForCodeGeneration})"))} }}";

        // withNameMethodInfo = typeof(Student).GetMethod(
        //     "WithName",
        //     BindingFlags.Instance | BindingFlags.NonPublic,
        //     new Type[] { typeof(string) })!;
        staticConstructor.AppendBodyLine($"{fieldName} = typeof({CodeBoard.Info.FluentApiClassName}).GetMethod(");
        staticConstructor.AppendBodyLine($"{indentation}\"{symbolInfo.Name}\",");
        staticConstructor.AppendBodyLine($"{indentation}{InfoFieldBindingFlagsArgument(symbolInfo)},");
        staticConstructor.AppendBodyLine($"{indentation}{typeArguments})!;");

        CodeBoard.CodeFile.AddUsing("System");
    }
}
