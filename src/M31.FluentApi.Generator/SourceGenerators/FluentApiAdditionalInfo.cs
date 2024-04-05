using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

/// <summary>
/// Contains additional information related to <see cref="FluentApiInfo"/>. Unlike the former, this class is not used
/// for caching in the incremental source generator.
/// </summary>
internal class FluentApiAdditionalInfo
{
    internal FluentApiAdditionalInfo(
        ISymbol symbol,
        AttributeDataExtended mainAttributeData,
        Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended> orthogonalAttributeData,
        Dictionary<ControlAttributeInfoBase, AttributeDataExtended> controlAttributeData)
    {
        Symbol = symbol;
        MainAttributeData = mainAttributeData;
        OrthogonalAttributeData = orthogonalAttributeData;
        ControlAttributeData = controlAttributeData;
    }

    internal ISymbol Symbol { get; }
    internal AttributeDataExtended MainAttributeData { get; }
    internal Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended> OrthogonalAttributeData { get; }
    internal Dictionary<ControlAttributeInfoBase, AttributeDataExtended> ControlAttributeData { get; }
}