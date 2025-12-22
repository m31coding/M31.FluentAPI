using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal abstract class InnerBodyGeneratorBase<TSymbolInfo>
    where TSymbolInfo : FluentApiSymbolInfo
{
    protected CodeBoard CodeBoard { get; }

    internal InnerBodyGeneratorBase(CodeBoard codeBoard)
    {
        CodeBoard = codeBoard;
        UnsafeAccessors = false;
    }

    internal bool UnsafeAccessors { get; private set; }

    internal void GenerateInnerBody(TSymbolInfo symbolInfo)
    {
        if (symbolInfo.Accessibility.IsPublicOrInternal())
        {
            GenerateInnerBodyForPublicSymbol(symbolInfo);
        }
        else
        {
            GenerateUnsafeAccessorAndInnerBodyForPrivateSymbol(symbolInfo);
            UnsafeAccessors = true;
        }
    }

    protected abstract string SymbolType(TSymbolInfo symbolInfo);
    protected abstract void GenerateInnerBodyForPublicSymbol(TSymbolInfo symbolInfo);
    protected abstract void GenerateInnerBodyForPrivateSymbol(TSymbolInfo symbolInfo, string setMethodName);

    private void GenerateUnsafeAccessorAndInnerBodyForPrivateSymbol(TSymbolInfo symbolInfo)
    {
        if (symbolInfo is not MemberSymbolInfo memberSymbolInfo) // todo: method
        {
            return;
        }

        string setMethodName = $"Set{symbolInfo.NameInPascalCase}";

        // [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_Name")]
        // private static extern void SetName(Student<T1, T2> student, string value);
        MethodSignature methodSignature =
            MethodSignature.Create("void", setMethodName, null, false);
        methodSignature.AddModifiers("private", "static", "extern");

        methodSignature.AddParameter(
            CodeBoard.Info.FluentApiClassNameWithTypeParameters,
            CodeBoard.Info.ClassInstanceName); // Student<T1, T2> student
        methodSignature.AddParameter(memberSymbolInfo.TypeForCodeGeneration, "value"); // string value

        methodSignature.AddAttribute(
            $"[UnsafeAccessor(UnsafeAccessorKind.Method, Name = \"set_{symbolInfo.NameInPascalCase}\")]");
        CodeBoard.BuilderClass.AddMethodSignature(methodSignature);

        GenerateInnerBodyForPrivateSymbol(symbolInfo, setMethodName);
    }
}