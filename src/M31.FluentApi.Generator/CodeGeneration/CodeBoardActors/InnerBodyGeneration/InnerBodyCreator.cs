using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyCreator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        InnerBodyForMemberGenerator innerBodyForMemberGenerator = new InnerBodyForMemberGenerator(codeBoard);
        InnerBodyForMethodGenerator innerBodyForMethodGenerator = new InnerBodyForMethodGenerator(codeBoard);

        foreach (FluentApiSymbolInfo symbolInfo in codeBoard.FluentApiInfos.Select(m => m.SymbolInfo))
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            switch (symbolInfo)
            {
                case MemberSymbolInfo memberInfo:
                    innerBodyForMemberGenerator.GenerateInnerBody(memberInfo);
                    break;

                case MethodSymbolInfo methodInfo:
                    innerBodyForMethodGenerator.GenerateInnerBody(methodInfo);
                    break;

                default:
                    throw new ArgumentException($"Unknown symbol info type: {symbolInfo.GetType()}");
            }
        }

        if (innerBodyForMemberGenerator.UnsafeAccessors || innerBodyForMethodGenerator.UnsafeAccessors)
        {
            ImportCompilerServices(codeBoard);
        }
    }

    private static void ImportCompilerServices(CodeBoard codeBoard)
    {
        codeBoard.CodeFile.AddUsing("System.Runtime.CompilerServices");
    }
}