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

    internal Comments GetMemberComments(MemberCommentKey memberCommentKey, string[] parameterNames)
    {
        if (memberComments.TryGetValue(memberCommentKey, out Comments comments))
        {
            return new Comments(comments.List.Where(IsRelevant).ToArray());
        }

        return new Comments(Array.Empty<Comment>());

        bool IsRelevant(Comment comment)
        {
            if (comment.Tag != "param")
            {
                return true;
            }

            string? parameterName = comment.Attributes.FirstOrDefault(a => a.Key == "name")?.Value;
            return parameterName != null && parameterNames.Contains(parameterName, StringComparer.InvariantCulture);
        }
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