namespace M31.FluentApi.Generator.CodeBuilding;

internal class GenericConstraints : ICode
{
    private readonly List<GenericConstraintClause> genericConstraintClauses;

    internal GenericConstraints()
    {
        genericConstraintClauses = new List<GenericConstraintClause>();
    }

    internal GenericConstraints(GenericConstraints constraints)
    {
        genericConstraintClauses = constraints.genericConstraintClauses.ToList();
    }

    internal IReadOnlyCollection<GenericConstraintClause> GenericConstraintClauses => genericConstraintClauses;
    internal int Count => genericConstraintClauses.Count;

    internal void Add(string parameter, IReadOnlyCollection<string> constraints)
    {
        genericConstraintClauses.Add(new GenericConstraintClause(parameter, constraints));
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.AppendNewLineSeparated(genericConstraintClauses);
    }
}