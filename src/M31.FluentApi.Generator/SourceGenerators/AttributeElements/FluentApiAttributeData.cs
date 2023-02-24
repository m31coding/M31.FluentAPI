namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal class FluentApiAttributeData
{
    internal FluentApiAttributeData(AttributeDataExtended mainAttributeData,
        IReadOnlyCollection<AttributeDataExtended> orthogonalAttributeData)
    {
        MainAttributeData = mainAttributeData;
        OrthogonalAttributeData = orthogonalAttributeData;
    }

    internal AttributeDataExtended MainAttributeData { get; }
    internal IReadOnlyCollection<AttributeDataExtended> OrthogonalAttributeData { get; }
}