namespace M31.FluentApi.Generator.CodeBuilding;

internal class GenericConstraintClause : ICode
{
    internal GenericConstraintClause(string parameter, IReadOnlyCollection<string> constraints)
    {
        Parameter = parameter;
        Constraints = constraints;
    }

    internal string Parameter { get; }
    internal IReadOnlyCollection<string> Constraints { get; }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append($"where {Parameter} : {string.Join(", ", Constraints)}");
    }
}