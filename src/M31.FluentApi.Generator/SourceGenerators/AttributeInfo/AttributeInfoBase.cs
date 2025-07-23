namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal abstract record AttributeInfoBase
{
    protected AttributeInfoBase(int builderStep)
    {
        BuilderStep = builderStep;
    }

    internal int BuilderStep { get; }
    internal abstract IReadOnlyList<string> FluentMethodNames { get; }
}