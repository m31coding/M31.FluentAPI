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
            CreateAttributeInfo(attributeData.MainAttributeData, symbol);

        (AttributeDataExtended data, OrthogonalAttributeInfoBase info)[] orthogonalDataAndInfos =
            attributeData.OrthogonalAttributeData
                .Select(data => (data, CreateOrthogonalAttributeInfo(data, symbol.Name)))
                .ToArray();

        (AttributeDataExtended data, ControlAttributeInfoBase info)[] controlDataAndInfos =
            attributeData.ControlAttributeData
                .Select(data => (data, CreateControlAttributeInfo(data)))
                .ToArray();

        FluentReturnAttributeInfo? fluentReturnAttributeInfo = controlDataAndInfos.Select(d => d.info)
            .OfType<FluentReturnAttributeInfo>().FirstOrDefault();

        FluentApiSymbolInfo symbolInfo = SymbolInfoCreator.Create(symbol);
        FluentApiAdditionalInfo additionalInfo = new FluentApiAdditionalInfo(
            symbol,
            attributeData.MainAttributeData,
            ToDictionary(orthogonalDataAndInfos),
            ToDictionary(controlDataAndInfos),
            fluentReturnAttributeInfo);

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

    private static AttributeInfoBase CreateAttributeInfo(AttributeDataExtended attributeData, ISymbol symbol)
    {
        string memberName = symbol.Name;

        return attributeData.FullName switch
        {
            FullNames.FluentMemberAttribute =>
                FluentMemberAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentPredicateAttribute =>
                FluentPredicateAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentCollectionAttribute =>
                FluentCollectionAttributeInfo.Create(attributeData.AttributeData, memberName),
            FullNames.FluentLambdaAttribute =>
                FluentLambdaAttributeInfo.Create(attributeData.AttributeData, memberName, GetLambdaBuilderInfo(symbol)),
            FullNames.FluentMethodAttribute =>
                FluentMethodAttributeInfo.Create(attributeData.AttributeData, memberName),
            _ => throw new Exception($"Unexpected attribute class name: {attributeData.FullName}")
        };
    }

    private static LambdaBuilderInfo GetLambdaBuilderInfo(ISymbol symbol)
    {
        ITypeSymbol type = symbol is IPropertySymbol propertySymbol ? propertySymbol.Type :
            symbol is IFieldSymbol fieldSymbol ? fieldSymbol.Type :
            throw new GenerationException($"Unexpected symbol type: {symbol.GetType().Name}");

        AttributeDataExtended[] parentData = type.GetAttributes().Select(AttributeDataExtended.Create)
            .OfType<AttributeDataExtended>().Where(a => a.FullName == Attributes.FullNames.FluentApiAttribute)
            .ToArray();

        // todo: create diagnostics if parent is no FluentApi. Create FluentApiInfoCreator.
        AttributeDataExtended parent = parentData.First();
        FluentApiAttributeInfo info = FluentApiAttributeInfo.Create(parent.AttributeData, type.Name);
        string builderClassName = info.BuilderClassName; // CreateAddress
        string fluentApiTypeForCodeGeneration = CodeTypeExtractor.GetTypeForCodeGeneration(type); // Namespace.Address
        string builderTypeForCodeGeneration =
            GetBuilderTypeForCodeGeneration(fluentApiTypeForCodeGeneration, builderClassName); // Namespace.CreateAddress.
        return new LambdaBuilderInfo(builderClassName, builderTypeForCodeGeneration);
    }

    private static string GetBuilderTypeForCodeGeneration(string fluentApiTypeForCodeGeneration, string builderClassName)
    {
        string[] split = fluentApiTypeForCodeGeneration.Split('.');
        return string.Join(".", split.Take(split.Length - 1).Concat(new string[] { builderClassName }));
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