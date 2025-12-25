using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

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
        if (symbolInfo.PubliclyWritable)
        {
            GenerateInnerBodyForPublicSymbol(symbolInfo);
        }
        else
        {
            GenerateInnerBodyForPrivateSymbol(symbolInfo);
            UnsafeAccessors = true;
        }
    }

    protected abstract void GenerateInnerBodyForPublicSymbol(TSymbolInfo symbolInfo);
    protected abstract void GenerateInnerBodyForPrivateSymbol(TSymbolInfo symbolInfo);
}