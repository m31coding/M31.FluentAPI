using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class LineForMemberGenerator : LineGeneratorBase<MemberSymbolInfo>
{
    internal LineForMemberGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
    }

    protected override string SymbolType(MemberSymbolInfo symbolInfo)
    {
        return symbolInfo.IsProperty ? "Property" : "Field";
    }

    protected override void GenerateLineWithoutReflection(MemberSymbolInfo symbolInfo)
    {
        // createStudent.student.Semester = semester;
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{instancePrefix}{CodeBoard.Info.ClassInstanceName}.{symbolInfo.Name} = {value}{GetPostfix(value)};");
        CodeBoard.MemberToSetMemberCode[symbolInfo.Name] = setMemberCode;

        string GetPostfix(string value)
        {
            return !symbolInfo.IsNullable && value == "null" ? "!" : string.Empty;
        }
    }

    protected override void GenerateLineWithReflection(MemberSymbolInfo symbolInfo, string infoFieldName)
    {
        // semesterPropertyInfo.SetValue(createStudent.student, semester);
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{infoFieldName}.SetValue({instancePrefix}{CodeBoard.Info.ClassInstanceName}, {value});");
        CodeBoard.MemberToSetMemberCode[symbolInfo.Name] = setMemberCode;
    }

    protected override void InitializeInfoField(string fieldName, MemberSymbolInfo symbolInfo)
    {
        // semesterPropertyInfo = typeof(Student)
        //     .GetProperty("Semester", BindingFlags.Instance | BindingFlags.NonPublic););
        string code = $"{fieldName} =" +
                      $" typeof({CodeBoard.Info.FluentApiClassName})" +
                      $".Get{SymbolType(symbolInfo)}(\"{symbolInfo.Name}\", " +
                      $"{InfoFieldBindingFlagsArgument(symbolInfo)})!;";

        CodeBoard.StaticConstructor!.AppendBodyLine(code);
    }
}