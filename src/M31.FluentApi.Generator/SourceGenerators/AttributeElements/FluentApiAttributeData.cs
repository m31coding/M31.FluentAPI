namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal class FluentApiAttributeData
{
    internal FluentApiAttributeData(
        AttributeDataExtended mainAttributeData,
        IReadOnlyCollection<AttributeDataExtended> orthogonalAttributeData,
        IReadOnlyCollection<AttributeDataExtended> controlAttributeData)
    {
        MainAttributeData = mainAttributeData;
        OrthogonalAttributeData = orthogonalAttributeData;
        ControlAttributeData = controlAttributeData;
    }

    internal AttributeDataExtended MainAttributeData { get; }
    internal IReadOnlyCollection<AttributeDataExtended> OrthogonalAttributeData { get; }
    internal IReadOnlyCollection<AttributeDataExtended> ControlAttributeData { get; }
}