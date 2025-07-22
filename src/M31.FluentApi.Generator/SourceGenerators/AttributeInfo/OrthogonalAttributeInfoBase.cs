namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal abstract record OrthogonalAttributeInfoBase
{
    internal abstract IReadOnlyCollection<string> FluentMethodNames { get; }
}