namespace M31.FluentApi.Generator.SourceGenerators;

internal record Step
{
    public Step(int? value, string mode)
    {
        Value = value;
        Mode = mode;
    }

    internal int? Value { get; }
    internal string Mode { get; }
}