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

    protected override void GenerateInnerBodyForPublicSymbol(MemberSymbolInfo symbolInfo)
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

    protected override void GenerateInnerBodyForPrivateSymbol(MemberSymbolInfo symbolInfo, string setMethodName)
    {
        // SetName(createStudent.student, name);
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{setMethodName}({instancePrefix}{CodeBoard.Info.ClassInstanceName}, {value});");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);
    }
}