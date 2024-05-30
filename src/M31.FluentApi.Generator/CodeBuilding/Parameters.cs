namespace M31.FluentApi.Generator.CodeBuilding;

internal class Parameters : ICode
{
    private readonly List<Parameter> values;

    private Parameters(List<Parameter> parameters)
    {
        values = parameters;
    }

    internal Parameters()
    {
        values = new List<Parameter>();
    }

    internal Parameters(Parameters parameters)
    {
        values = parameters.values.ToList();
    }

    internal Parameters WithoutDefaultValues()
    {
        return new Parameters(values.Select(v => v.WithoutDefaultValue()).ToList());
    }

    internal IReadOnlyCollection<Parameter> Values => values;

    internal void AddParameter(Parameter parameter)
    {
        values.Add(parameter);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append("(").Append(values, ", ").Append(")");
    }
}