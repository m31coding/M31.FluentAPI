using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DocumentationGeneration;

internal class CommentsGenerator : ICodeBoardActor
{
    // todo: cancellation
    public void Modify(CodeBoard codeBoard)
    {
        foreach (FluentApiInfo fluentApiInfo in codeBoard.FluentApiInfos)
        {
            switch (fluentApiInfo.SymbolInfo)
            {
                case MemberSymbolInfo memberInfo:
                    HandleMemberSymbolInfo(memberInfo, codeBoard);
                    break;

                case MethodSymbolInfo methodSymbolInfo:
                    HandleMethodSymbolInfo(methodSymbolInfo, codeBoard);
                    break;

                default:
                    throw new ArgumentException($"Unknown symbol info type: {fluentApiInfo.SymbolInfo.GetType()}");
            }
        }
    }

    private void HandleMemberSymbolInfo(MemberSymbolInfo memberInfo, CodeBoard codeBoard)
    {
        return; // todo
    }

    private void HandleMethodSymbolInfo(MethodSymbolInfo methodInfo, CodeBoard codeBoard)
    {
        Comments transformedComments = CommentsTransformer.TransformComments(methodInfo.Comments);
        codeBoard.TransformedComments.AssignMethodComments(methodInfo, transformedComments);
    }
}
