using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceGenerators.AttributeElements.Attributes;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiInfo
{
    internal FluentApiInfo(
        FluentApiSymbolInfo symbolInfo,
        AttributeInfoBase attributeInfo,
        IReadOnlyCollection<OrthogonalAttributeInfoBase> orthogonalAttributeInfos)
    {
        SymbolInfo = symbolInfo;
        AttributeInfo = attributeInfo;
        OrthogonalAttributeInfos = orthogonalAttributeInfos;
    }

    internal FluentApiSymbolInfo SymbolInfo { get; }
    internal AttributeInfoBase AttributeInfo { get; }
    internal IReadOnlyCollection<OrthogonalAttributeInfoBase> OrthogonalAttributeInfos { get; }
    internal string FluentMethodName => AttributeInfo.FluentMethodName;

    internal static FluentApiInfo Create(
        ISymbol symbol,
        FluentApiAttributeData attributeData,
        out FluentApiAdditionalInfo additionalInfo)
    {
        AttributeInfoBase attributeInfo =
            CreateAttributeInfo(attributeData.MainAttributeData, symbol.Name);

        Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended> orthogonalAttributeData
            = new Dictionary<OrthogonalAttributeInfoBase, AttributeDataExtended>();

        List<OrthogonalAttributeInfoBase> orthogonalAttributeInfos = new List<OrthogonalAttributeInfoBase>();

        foreach (AttributeDataExtended orthogonalDataExtended in attributeData.OrthogonalAttributeData)
        {
            OrthogonalAttributeInfoBase orthogonalAttributeInfo =
                CreateOrthogonalAttributeInfo(orthogonalDataExtended, symbol.Name);
            orthogonalAttributeData[orthogonalAttributeInfo] = orthogonalDataExtended;
            orthogonalAttributeInfos.Add(orthogonalAttributeInfo);
        }

        FluentApiSymbolInfo symbolInfo = SymbolInfoCreator.Create(symbol);
        additionalInfo = new FluentApiAdditionalInfo(symbol, attributeData.MainAttributeData, orthogonalAttributeData);
        return new FluentApiInfo(symbolInfo, attributeInfo, orthogonalAttributeInfos);
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

    protected bool Equals(FluentApiInfo other)
    {
        return SymbolInfo.Equals(other.SymbolInfo) &&
               AttributeInfo.Equals(other.AttributeInfo) &&
               OrthogonalAttributeInfos.SequenceEqual(other.OrthogonalAttributeInfos);
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
        return new HashCode().Add(SymbolInfo, AttributeInfo).AddSequence(OrthogonalAttributeInfos);
    }
}