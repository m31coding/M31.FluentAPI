namespace M31.FluentApi.Generator.CodeBuilding;

internal class Method : ICode
{
    internal Method(MethodSignature methodSignature)
    {
        MethodComments = new MethodComments();
        MethodSignature = methodSignature;
        MethodBody = new MethodBody();
    }

    internal Method(MethodComments methodComments, MethodSignature methodSignature, MethodBody methodBody)
    {
        MethodComments = methodComments;
        MethodSignature = methodSignature;
        MethodBody = methodBody;
    }

    internal MethodComments MethodComments { get; }
    internal MethodSignature MethodSignature { get; }
    internal MethodBody MethodBody { get; }

    internal void AddCommentLine(string commentLine)
    {
        MethodComments.AddLine(commentLine);
    }

    internal void AppendBodyLine(string line)
    {
        MethodBody.AppendLine(line);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .Append(MethodComments)
            .Append(MethodSignature)
            .Append(MethodBody);
    }
}