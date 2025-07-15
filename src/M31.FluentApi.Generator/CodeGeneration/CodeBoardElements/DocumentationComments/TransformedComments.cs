using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

internal class TransformedComments
{
    private readonly Dictionary<string, Comments> memberComments;
    private readonly Dictionary<MethodSymbolInfo, Comments> methodComments;

    internal TransformedComments()
    {
        memberComments = new Dictionary<string, Comments>();
        methodComments = new Dictionary<MethodSymbolInfo, Comments>();
    }

    internal void AssignMemberComments(string memberName, Comments comments)
    {
        if (memberComments.ContainsKey(memberName))
        {
            throw new InvalidOperationException(
                $"{nameof(Comments)} for member {memberName} has already been assigned.");
        }

        memberComments[memberName] = comments;
    }

    internal Comments GetMemberComments(string memberName)
    {
        return memberComments[memberName];
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
