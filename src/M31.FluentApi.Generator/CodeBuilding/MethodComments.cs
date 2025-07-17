namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodComments : ICode
{
    private readonly List<string> comments;

    internal MethodComments()
    {
        comments = new List<string>();
    }

    internal IReadOnlyCollection<string> Comments => comments;

    internal void AddCommentLine(string commentLine)
    {
        comments.Add(commentLine);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .AppendLines(comments);
    }
}
