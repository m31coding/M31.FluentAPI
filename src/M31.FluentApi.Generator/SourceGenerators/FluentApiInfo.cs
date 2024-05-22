using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.SourceGenerators;

/// <summary>
/// Represents the information for one member or method of the fluent API class. GetHashCode and Equals must be
/// implemented carefully to ensure correct caching in the incremental source generator.
/// The property <see cref="FluentApiAdditionalInfo"/> holds members that are irrelevant or unsuitable for equality
/// checks.
/// </summary>
internal class FluentApiInfo
{
    internal FluentApiInfo(
        FluentApiSymbolInfo symbolInfo,
        AttributeInfoBase attributeInfo,
        IReadOnlyCollection<OrthogonalAttributeInfoBase> orthogonalAttributeInfos,
        IReadOnlyCollection<ControlAttributeInfoBase> controlAttributeInfos,
        FluentApiAdditionalInfo fluentApiAdditionalInfo)
    {
        SymbolInfo = symbolInfo;
        AttributeInfo = attributeInfo;
        OrthogonalAttributeInfos = orthogonalAttributeInfos;
        ControlAttributeInfos = controlAttributeInfos;
        AdditionalInfo = fluentApiAdditionalInfo;
    }

    internal FluentApiSymbolInfo SymbolInfo { get; }
    internal AttributeInfoBase AttributeInfo { get; }
    internal IReadOnlyCollection<OrthogonalAttributeInfoBase> OrthogonalAttributeInfos { get; }
    internal IReadOnlyCollection<ControlAttributeInfoBase> ControlAttributeInfos { get; }
    internal FluentApiAdditionalInfo AdditionalInfo { get; }
    internal string FluentMethodName => AttributeInfo.FluentMethodName;

    protected bool Equals(FluentApiInfo other)
    {
        return SymbolInfo.Equals(other.SymbolInfo) &&
               AttributeInfo.Equals(other.AttributeInfo) &&
               OrthogonalAttributeInfos.SequenceEqual(other.OrthogonalAttributeInfos) &&
               ControlAttributeInfos.SequenceEqual(other.ControlAttributeInfos);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FluentApiInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(SymbolInfo, AttributeInfo)
            .AddSequence(OrthogonalAttributeInfos)
            .AddSequence(ControlAttributeInfos);
    }
}