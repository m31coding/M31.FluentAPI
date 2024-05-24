using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyForMemberGenerator : InnerBodyGeneratorBase<MemberSymbolInfo>
{
    internal InnerBodyForMemberGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
    }

    protected override string SymbolType(MemberSymbolInfo symbolInfo)
    {
        return symbolInfo.IsProperty ? "Property" : "Field";
    }

    protected override void GenerateInnerBodyWithoutReflection(MemberSymbolInfo symbolInfo)
    {
        // createStudent.student.Semester = semester;
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{instancePrefix}{CodeBoard.Info.ClassInstanceName}.{symbolInfo.Name} = {value}{GetPostfix(value)};");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);

        string GetPostfix(string value)
        {
            return !symbolInfo.IsNullable && value == "null" ? "!" : string.Empty;
        }
    }

    protected override void GenerateInnerBodyWithReflection(MemberSymbolInfo symbolInfo, string infoFieldName)
    {
        // CreateStudent.semesterPropertyInfo.SetValue(createStudent.student, semester);
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{CodeBoard.Info.BuilderClassNameWithTypeParameters}.{infoFieldName}" +
                $".SetValue({instancePrefix}{CodeBoard.Info.ClassInstanceName}, {value});");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);
    }

    protected override void InitializeInfoField(string fieldName, MemberSymbolInfo symbolInfo)
    {
        // semesterPropertyInfo = typeof(Student<T1, T2>)
        //     .GetProperty("Semester", BindingFlags.Instance | BindingFlags.NonPublic););
        string code = $"{fieldName} =" +
                      $" typeof({CodeBoard.Info.FluentApiClassNameWithTypeParameters})" +
                      $".Get{SymbolType(symbolInfo)}(\"{symbolInfo.Name}\", " +
                      $"{InfoFieldBindingFlagsArgument(symbolInfo)})!;";

        CodeBoard.StaticConstructor!.AppendBodyLine(code);
    }
}