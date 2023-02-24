namespace M31.FluentApi.Generator.CodeBuilding;

internal class Parameters : ICode
{
    private readonly List<Parameter> parameters;

    internal Parameters()
    {
        parameters = new List<Parameter>();
    }

    internal IReadOnlyCollection<Parameter> Values => parameters;

    internal void AddParameter(Parameter parameter)
    {
        parameters.Add(parameter);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append("(").Append(parameters, ", ").Append(")");
    }
}