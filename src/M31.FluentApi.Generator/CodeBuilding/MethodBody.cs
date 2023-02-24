namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodBody : ICode
{
    private readonly List<string> lines;

    internal MethodBody()
    {
        lines = new List<string>();
    }

    internal void AppendLine(string line)
    {
        lines.Add(line);
    }

    internal IReadOnlyCollection<string> Lines => lines;

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .OpenBlock()
            .AppendLines(lines)
            .CloseBlock();
    }
}