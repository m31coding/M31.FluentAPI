using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceGenerators.AttributeElements.Attributes;

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

    internal static FluentApiInfo Create(ISymbol symbol, FluentApiAttributeData attributeData)
    {
        AttributeInfoBase attributeInfo =
            CreateAttributeInfo(attributeData.MainAttributeData, symbol.Name);

        (AttributeDataExtended data, OrthogonalAttributeInfoBase info)[] orthogonalDataAndInfos =
            attributeData.OrthogonalAttributeData
                .Select(data => (data, CreateOrthogonalAttributeInfo(data, symbol.Name)))
                .ToArray();

        (AttributeDataExtended data, ControlAttributeInfoBase info)[] controlDataAndInfos =
            attributeData.ControlAttributeData
                .Select(data => (data, CreateControlAttributeInfo(data)))
                .ToArray();

        FluentApiSymbolInfo symbolInfo = SymbolInfoCreator.Create(symbol);
        FluentApiAdditionalInfo additionalInfo = new FluentApiAdditionalInfo(
            symbol,
            attributeData.MainAttributeData,
            ToDictionary(orthogonalDataAndInfos),
            ToDictionary(controlDataAndInfos));

        return new FluentApiInfo(
            symbolInfo,
            attributeInfo,
            ToInfos(orthogonalDataAndInfos),
            ToInfos(controlDataAndInfos),
            additionalInfo);
    }

    private static Dictionary<TAttributeInfoBase, AttributeDataExtended> ToDictionary<TAttributeInfoBase>(
        (AttributeDataExtended data, TAttributeInfoBase info)[] dataAndInfoArray)
    {
        return dataAndInfoArray.ToDictionary(dataAndInfo => dataAndInfo.info, dataAndInfo => dataAndInfo.data);
    }

    private static List<TAttributeInfoBase> ToInfos<TAttributeInfoBase>(
        (AttributeDataExtended data, TAttributeInfoBase info)[] dataAndInfoArray)
    {
        return dataAndInfoArray.Select(dataAndInfo => dataAndInfo.info).ToList();
    }

    private static AttributeInfoBase CreateAttributeInfo(AttributeDataExtended attributeData, string memberName)
    {
        return attributeData.FullName switch
        {
            FullNames.FluentMemberAttribute =>
                FluentMemberAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentPredicateAttribute =>
                FluentPredicateAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentCollectionAttribute =>
                FluentCollectionAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentMethodAttribute =>
                FluentMethodAttributeInfo.Create(attributeData.AttributeData, memberName),
            _ => throw new Exception($"Unexpected attribute class name: {attributeData.FullName}")
        };
    }

    private static OrthogonalAttributeInfoBase CreateOrthogonalAttributeInfo(
        AttributeDataExtended attributeDataExtended,
        string memberName)
    {
        return attributeDataExtended.FullName switch
        {
            FullNames.FluentNullableAttribute =>
                FluentNullableAttributeInfo.Create(attributeDataExtended.AttributeData, memberName),
            FullNames.FluentDefaultAttribute =>
                FluentDefaultAttributeInfo.Create(attributeDataExtended.AttributeData, memberName),
            _ => throw new Exception($"Unexpected attribute class name: {attributeDataExtended.FullName}")
        };
    }

    private static ControlAttributeInfoBase CreateControlAttributeInfo(AttributeDataExtended attributeDataExtended)
    {
        return attributeDataExtended.FullName switch
        {
            FullNames.FluentContinueWithAttribute =>
                FluentContinueWithAttributeInfo.Create(attributeDataExtended.AttributeData),
            FullNames.FluentBreakAttribute =>
                FluentBreakAttributeInfo.Create(attributeDataExtended.AttributeData),
            FullNames.FluentReturnAttribute =>
                FluentReturnAttributeInfo.Create(attributeDataExtended.AttributeData),
            _ => throw new Exception($"Unexpected attribute class name: {attributeDataExtended.FullName}")
        };
    }

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