namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal abstract record AttributeInfoBase
{
    protected AttributeInfoBase(int builderStep)
    {
        BuilderStep = builderStep;
    }

    internal int BuilderStep { get; }
    internal abstract string FluentMethodName { get; } // todo: rename or remove
    internal abstract IReadOnlyCollection<string> FluentMethodNames { get; }
}