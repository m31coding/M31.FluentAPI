using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DocumentationGeneration;

internal class CommentsGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        foreach (FluentApiInfo fluentApiInfo in codeBoard.FluentApiInfos)
        {
            if (codeBoard.CancellationRequested)
            {
                return;
            }

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
        IGrouping<string, Comment>[] groups = GroupByMethodName(memberInfo.Comments);

        foreach (var group in groups)
        {
            MemberCommentKey key = new MemberCommentKey(memberInfo.Name, group.Key);
            Comments comments = new Comments(group.ToArray());
            Comments transformedComments = CommentsTransformer.TransformComments(comments);
            codeBoard.TransformedComments.AssignMemberComments(key, transformedComments);
        }
    }

    private IGrouping<string, Comment>[] GroupByMethodName(Comments transformedComments)
    {
        List<(string, Comments)> methodComments = new List<(string, Comments)>();
        return transformedComments.List.GroupBy(GetMethodName).ToArray();

        static string GetMethodName(Comment comment)
        {
            return comment.Attributes.FirstOrDefault(a => a.Key == "method")?.Value ?? string.Empty;
        }
    }

    private void HandleMethodSymbolInfo(MethodSymbolInfo methodInfo, CodeBoard codeBoard)
    {
        Comments transformedComments = CommentsTransformer.TransformComments(methodInfo.Comments);
        codeBoard.TransformedComments.AssignMethodComments(methodInfo, transformedComments);
    }
}
