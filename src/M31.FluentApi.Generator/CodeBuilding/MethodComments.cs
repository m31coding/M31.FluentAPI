namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodComments : ICode
{
    private readonly List<string> comments;

    internal MethodComments()
    {
        comments = new List<string>();
    }

    internal MethodComments(List<string> comments)
    {
        this.comments = comments;
    }

    internal IReadOnlyCollection<string> Comments => comments;
    internal bool Any => Comments.Count > 0;

    internal void AddLine(string commentLine)
    {
        comments.Add(commentLine);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .AppendLines(comments);
    }
}