using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

internal class TransformedComments
{
    private readonly Dictionary<MemberCommentKey, Comments> memberComments;
    private readonly Dictionary<MethodSymbolInfo, Comments> methodComments;

    internal TransformedComments()
    {
        memberComments = new Dictionary<MemberCommentKey, Comments>();
        methodComments = new Dictionary<MethodSymbolInfo, Comments>();
    }

    internal void AssignMemberComments(MemberCommentKey memberCommentKey, Comments comments)
    {
        if (memberComments.ContainsKey(memberCommentKey))
        {
            throw new InvalidOperationException(
                $"{nameof(Comments)} for key {memberCommentKey} has already been assigned.");
        }

        memberComments[memberCommentKey] = comments;
    }

    internal Comments GetMemberComments(MemberCommentKey memberCommentKey)
    {
        if (memberComments.TryGetValue(memberCommentKey, out Comments comments))
        {
            return comments;
        }

        return new Comments(Array.Empty<Comment>());
    }

    internal void AssignMethodComments(MethodSymbolInfo methodSymbolInfo, Comments comments)
    {
        if (methodComments.ContainsKey(methodSymbolInfo))
        {
            throw new InvalidOperationException(
                $"{nameof(Comments)} for method {methodSymbolInfo.Name} has already been assigned.");
        }

        methodComments[methodSymbolInfo] = comments;
    }

    internal Comments GetMethodComments(MethodSymbolInfo methodSymbolInfo)
    {
        return methodComments[methodSymbolInfo];
    }
}
