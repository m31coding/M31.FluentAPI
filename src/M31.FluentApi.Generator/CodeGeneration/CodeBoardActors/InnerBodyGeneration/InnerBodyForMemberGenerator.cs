using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;

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
        if (symbolInfo.IsProperty)
        {
            GenerateInnerBodyForPrivateProperty(symbolInfo);
        }
        else
        {
            GenerateInnerBodyForPrivateField(symbolInfo);
        }
    }

    private void GenerateInnerBodyForPrivateProperty(MemberSymbolInfo symbolInfo)
    {
        string setMethodName = $"Set{symbolInfo.NameInPascalCase}";

        // [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_Name")]
        // private static extern void SetName(Student<T1, T2> student, string value);
        MethodSignature unsafeAccessorSignature =
            MethodSignature.Create("void", setMethodName, null, true);
        unsafeAccessorSignature.AddModifiers("private", "static", "extern");

        unsafeAccessorSignature.AddParameter(
            symbolInfo.DeclaringClassNameWithTypeParameters,
            symbolInfo.DeclaringClassName.FirstCharToLower()); // Student<T1, T2> student
        unsafeAccessorSignature.AddParameter(symbolInfo.TypeForCodeGeneration, "value");

        unsafeAccessorSignature.AddAttribute(
            $"[UnsafeAccessor(UnsafeAccessorKind.Method, Name = \"set_{symbolInfo.Name}\")]");
        CodeBoard.BuilderClass.AddMethodSignature(unsafeAccessorSignature);

        // SetName(createStudent.student, name);
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{setMethodName}({instancePrefix}{CodeBoard.Info.ClassInstanceName}, {value}!);");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);
    }

    private void GenerateInnerBodyForPrivateField(MemberSymbolInfo symbolInfo)
    {
        string getFieldName = $"{symbolInfo.NameInPascalCase}Field";

        // [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "semester")]
        // private static extern ref int SemesterField(Student student);
        MethodSignature unsafeAccessorSignature =
            MethodSignature.Create(symbolInfo.TypeForCodeGeneration, getFieldName, null, true);
        unsafeAccessorSignature.AddModifiers("private", "static", "extern", "ref");

        unsafeAccessorSignature.AddParameter(
            symbolInfo.DeclaringClassNameWithTypeParameters,
            symbolInfo.DeclaringClassName.FirstCharToLower()); // Student student

        unsafeAccessorSignature.AddAttribute(
            $"[UnsafeAccessor(UnsafeAccessorKind.Field, Name = \"{symbolInfo.Name}\")]");
        CodeBoard.BuilderClass.AddMethodSignature(unsafeAccessorSignature);

        // SemesterField(createStudent.student) = semester;
        SetMemberCode setMemberCode =
            new SetMemberCode((instancePrefix, value) =>
                $"{getFieldName}({instancePrefix}{CodeBoard.Info.ClassInstanceName}) = {value}!;");
        CodeBoard.InnerBodyCreationDelegates.AssignSetMemberCode(symbolInfo.Name, setMemberCode);
    }
}