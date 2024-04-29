namespace M31.FluentApi.Generator.CodeBuilding;

internal class GenericConstraints : ICode
{
    private readonly List<ParameterWithConstraints> parametersWithConstraints;

    internal GenericConstraints()
    {
        parametersWithConstraints = new List<ParameterWithConstraints>();
    }

    internal GenericConstraints(GenericConstraints constraints)
    {
        parametersWithConstraints = constraints.parametersWithConstraints.ToList();
    }

    internal IReadOnlyCollection<ParameterWithConstraints> ParametersWithConstraints => parametersWithConstraints;
    internal int Count => parametersWithConstraints.Count;

    internal void Add(string parameter, IReadOnlyCollection<string> constraints)
    {
        parametersWithConstraints.Add(new ParameterWithConstraints(parameter, constraints));
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.AppendNewLineSeparated(
            parametersWithConstraints.Select(p => $"where {p.Parameter} : {string.Join(", ", p.Constraints)}"));
    }

    internal class ParameterWithConstraints
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