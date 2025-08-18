namespace M31.FluentApi.Generator.CodeBuilding;

internal class CommentedMethodSignature : ICode
{
    internal MethodSignature MethodSignature { get; }
    internal MethodComments MethodComments { get; }

    internal CommentedMethodSignature(MethodSignature methodSignature, MethodComments methodComments)
    {
        MethodSignature = methodSignature;
        MethodComments = methodComments;
    }

    internal CommentedMethodSignature TransformSignature(Func<MethodSignature, MethodSignature> transform)
    {
        return new CommentedMethodSignature(transform(MethodSignature), MethodComments);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .Append(MethodComments)
            .Append(MethodSignature);
    }
}