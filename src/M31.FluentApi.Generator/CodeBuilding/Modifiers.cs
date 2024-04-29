namespace M31.FluentApi.Generator.CodeBuilding;

internal class Modifiers : ICode
{
    private readonly List<string> values;

    internal Modifiers()
    {
        values = new List<string>();
    }

    internal Modifiers(params string[] values)
    {
        this.values = values.ToList();
    }

    internal Modifiers(Modifiers modifiers)
    {
        values = modifiers.values.ToList();
    }

    internal IReadOnlyCollection<string> Values => values;

    internal void Add(params string[] modifiers)
    {
        values.AddRange(modifiers);
    }

    internal void Add(IEnumerable<string> modifiers)
    {
        values.AddRange(modifiers);
    }

    internal bool Contains(string modifier)
    {
        return Values.Contains(modifier);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append(ToString());
    }

    public override string ToString()
    {
        return Values.Count == 0 ? string.Empty : $"{string.Join(" ", Values)} ";
    }
}