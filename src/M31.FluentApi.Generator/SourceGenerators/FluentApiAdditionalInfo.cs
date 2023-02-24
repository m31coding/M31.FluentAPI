using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiAdditionalInfo
{
    internal FluentApiAdditionalInfo(
        ISymbol symbol,
        AttributeDataExtended mainAttributeData,
        Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended> orthogonalAttributeData)
    {
        Symbol = symbol;
        MainAttributeData = mainAttributeData;
        OrthogonalAttributeData = orthogonalAttributeData;
    }

    internal ISymbol Symbol { get; }
    internal AttributeDataExtended MainAttributeData { get; }
    internal Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended> OrthogonalAttributeData { get; }
}