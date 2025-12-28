using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyForMemberGenerator : InnerBodyGeneratorBase<MemberSymbolInfo>
{
    internal InnerBodyForMemberGenerator(CodeBoard codeBoard)
        : base(codeBoard)
    {
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

    protected override void GenerateInnerBodyForPrivateSymbol(MemberSymbolInfo symbolInfo)
    {
        string setMethodName = $"Set{symbolInfo.NameInPascalCase}";

        // [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_Name")]
        // private static extern void SetName(Student<T1, T2> student, string value);
        MethodSignature unsafeAccessorSignature =
            MethodSignature.Create("void", setMethodName, null, true);
        unsafeAccessorSignature.AddModifiers("private", "static", "extern");

        unsafeAccessorSignature.AddParameter(
            CodeBoard.Info.FluentApiClassNameWithTypeParameters,
            CodeBoard.Info.ClassInstanceName); // Student<T1, T2> student
        unsafeAccessorSignature.AddParameter(symbolInfo.TypeForCodeGeneration, "value"); // string value

        unsafeAccessorSignature.AddAttribute(
            $"[UnsafeAccessor(UnsafeAccessorKind.Method, Name = \"set_{symbolInfo.NameInPascalCase}\")]");
        CodeBoard.BuilderClass.AddMethodSignature(unsafeAccessorSignature);

        // SetName(createStudent.student, name);
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{setMethodName}({instancePrefix}{CodeBoard.Info.ClassInstanceName}, {value});");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);
    }
}