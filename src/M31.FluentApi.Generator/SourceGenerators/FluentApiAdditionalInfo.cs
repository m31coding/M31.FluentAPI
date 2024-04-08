using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

/// <summary>
/// Contains additional information belonging to <see cref="FluentApiInfo"/>. This class is not used for equality checks
/// and hence does not influence the caching in the incremental source generator.
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