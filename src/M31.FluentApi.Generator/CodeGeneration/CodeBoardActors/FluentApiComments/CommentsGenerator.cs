using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.FluentApiComments;

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
                    HandleMemberSymbolInfo(memberInfo, fluentApiInfo, codeBoard);
                    break;

                case MethodSymbolInfo methodSymbolInfo:
                    HandleMethodSymbolInfo(methodSymbolInfo, codeBoard);
                    break;

                default:
                    throw new ArgumentException($"Unknown symbol info type: {fluentApiInfo.SymbolInfo.GetType()}");
            }
        }
    }

    private void HandleMemberSymbolInfo(MemberSymbolInfo memberInfo, FluentApiInfo fluentApiInfo, CodeBoard codeBoard)
    {
        string? singleMethodName = TryGetSingleMethodName(fluentApiInfo);
        IGrouping<string, Comment>[] groups = GroupByMethodName(memberInfo.Comments, singleMethodName);

        foreach (var group in groups)
        {
            MemberCommentKey key = new MemberCommentKey(memberInfo.Name, group.Key);
            Comments comments = new Comments(group.ToArray());
            Comments transformedComments = CommentsTransformer.TransformComments(comments);
            codeBoard.TransformedComments.AssignMemberComments(key, transformedComments);
        }
    }

    private static string? TryGetSingleMethodName(FluentApiInfo fluentApiInfo)
    {
        string[] fluentMethodNames = fluentApiInfo.AttributeInfo.FluentMethodNames
            .Concat(fluentApiInfo.OrthogonalAttributeInfos.SelectMany(o => o.FluentMethodNames)).Distinct().ToArray();
        return fluentMethodNames.Length != 1 ? null : fluentMethodNames[0];
    }

    private IGrouping<string, Comment>[] GroupByMethodName(Comments transformedComments, string? fallbackMethodName)
    {
        return transformedComments.List.GroupBy(GetMethodName).Where(g => g.Key != string.Empty).ToArray();

        string GetMethodName(Comment comment)
        {
            return comment.Attributes.FirstOrDefault(a => a.Key == "method")?.Value ??
                   fallbackMethodName ?? string.Empty;
        }
    }

    private void HandleMethodSymbolInfo(MethodSymbolInfo methodInfo, CodeBoard codeBoard)
    {
        Comments transformedComments = CommentsTransformer.TransformComments(methodInfo.Comments);
        codeBoard.TransformedComments.AssignMethodComments(methodInfo, transformedComments);
    }
}