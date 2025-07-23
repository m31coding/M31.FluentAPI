namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal abstract record OrthogonalAttributeInfoBase
{
    internal abstract IReadOnlyList<string> FluentMethodNames { get; }
}