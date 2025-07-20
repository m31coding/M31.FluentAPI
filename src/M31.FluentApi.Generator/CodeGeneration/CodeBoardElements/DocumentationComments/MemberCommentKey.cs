namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

internal record MemberCommentKey
{
    internal MemberCommentKey(string memberName, string method)
    {
        MemberName = memberName;
        Method = method;
    }

    internal string MemberName { get; }
    internal string Method { get; }

    public override string ToString()
    {
        return $"{MemberName}-{Method}";
    }
}
