namespace M31.FluentApi.Generator.CodeBuilding;

internal class Method : ICode
{
    internal Method(MethodSignature methodSignature)
    {
        MethodSignature = methodSignature;
        MethodBody = new MethodBody();
    }

    internal MethodSignature MethodSignature { get; }
    internal MethodBody MethodBody { get; }

    internal void AppendBodyLine(string line)
    {
        MethodBody.AppendLine(line);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .Append(MethodSignature)
            .Append(MethodBody);
    }
}