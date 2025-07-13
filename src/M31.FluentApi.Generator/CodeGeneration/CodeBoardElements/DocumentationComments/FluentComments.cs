namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

internal class FluentComments
{
    private readonly Dictionary<string, Comments> memberComments;
    private readonly Dictionary<MethodSymbolInfo, Comments> methodComments;

    internal FluentComments()
    {
        memberComments = new Dictionary<string, Comments>();
        methodComments = new Dictionary<MethodSymbolInfo, Comments>();
    }

    internal Comments GetMemberComments(string memberName)
    {
        return memberComments[memberName];
    }

    internal Comments GetMethodComments(MethodSymbolInfo methodSymbolInfo)
    {
        return methodComments[methodSymbolInfo];
    }
}
