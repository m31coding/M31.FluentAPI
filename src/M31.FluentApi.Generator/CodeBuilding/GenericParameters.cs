namespace M31.FluentApi.Generator.CodeBuilding;

internal class GenericParameters : ICode
{
    private readonly List<string> values;

    internal GenericParameters(params string[] parameters)
    {
        values = parameters.ToList();
    }

    internal GenericParameters(GenericParameters parameters)
    {
        values = parameters.values.ToList();
    }

    internal IReadOnlyCollection<string> Values => values;

    internal void Add(string parameter)
    {
        values.Add(parameter);
    }

    internal void Add(params string[] parameters)
    {
        values.AddRange(parameters);
    }

    internal void Add(IEnumerable<string> parameters)
    {
        values.AddRange(parameters);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append(() => $"<{string.Join(", ", values)}>", values.Count > 0);
    }

    public override string ToString()
    {
        return Values.Count == 0 ? string.Empty : $"<{string.Join(", ", values)}>";
    }
}