namespace M31.FluentApi.Generator.CodeBuilding;

internal class GenericConstraints : ICode
{
    private readonly List<ParameterWithConstraints> parametersWithConstraints;

    internal GenericConstraints()
    {
        parametersWithConstraints = new List<ParameterWithConstraints>();
    }

    internal void Add(string parameter, IReadOnlyCollection<string> constraints)
    {
        parametersWithConstraints.Add(new ParameterWithConstraints(parameter, constraints));
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        foreach (ParameterWithConstraints p in parametersWithConstraints)
        {
            codeBuilder.AppendLine($"where {p.Parameter} : {string.Join(", ", p.Constraints)}");
        }

        return codeBuilder;
    }

    private class ParameterWithConstraints
    {
        internal ParameterWithConstraints(string parameter, IReadOnlyCollection<string> constraints)
        {
            Parameter = parameter;
            Constraints = constraints;
        }

        internal string Parameter { get; }
        internal IReadOnlyCollection<string> Constraints { get; }
    }
}