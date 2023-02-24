namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class SetMemberCode
{
    private readonly Func<string, string, string> buildCodeWithInstancePrefixAndValue;

    internal SetMemberCode(Func<string, string, string> buildCodeWithInstancePrefixAndValue)
    {
        this.buildCodeWithInstancePrefixAndValue = buildCodeWithInstancePrefixAndValue;
    }

    internal string BuildCode(string instancePrefix, string value)
    {
        return buildCodeWithInstancePrefixAndValue(instancePrefix, value);
    }

    public override string ToString()
    {
        return buildCodeWithInstancePrefixAndValue("{prefix}", "{value}");
    }
}