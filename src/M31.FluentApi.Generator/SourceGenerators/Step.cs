namespace M31.FluentApi.Generator.SourceGenerators;

internal record Step
{
    public Step(int? value)
    {
        Value = value;
    }

    internal int? Value { get; }
}