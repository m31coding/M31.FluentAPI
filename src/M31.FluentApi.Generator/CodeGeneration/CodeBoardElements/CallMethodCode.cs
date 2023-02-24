namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class CallMethodCode
{
    private readonly Func<string, string[], string> buildCodeWithInstancePrefixAndValues;

    internal CallMethodCode(Func<string, string[], string> buildCodeWithInstancePrefixAndValues)
    {
        this.buildCodeWithInstancePrefixAndValues = buildCodeWithInstancePrefixAndValues;
    }

    internal string BuildCode(string instancePrefix, string[] parameters)
    {
        return buildCodeWithInstancePrefixAndValues(instancePrefix, parameters);
    }

    public override string ToString()
    {
        return buildCodeWithInstancePrefixAndValues(string.Empty, Array.Empty<string>());
    }
}