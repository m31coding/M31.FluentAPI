using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;
using static M31.FluentApi.Generator.SourceGenerators.AttributeElements.Attributes;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiInfoCreator
{
    private readonly ClassInfoReport classInfoReport;

    internal FluentApiInfoCreator(ClassInfoReport classInfoReport)
    {
        this.classInfoReport = classInfoReport;
    }

    internal FluentApiInfo? Create(ISymbol symbol, FluentApiAttributeData attributeData)
    {
        AttributeInfoBase? attributeInfo =
            CreateAttributeInfo(attributeData.MainAttributeData, symbol);

        if (attributeInfo == null)
        {
            return null;
        }

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

    private AttributeInfoBase? CreateAttributeInfo(AttributeDataExtended attributeData, ISymbol symbol)
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
                CreateFluentLambdaAttributeInfo(attributeData, memberName, symbol),
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
                FluentBreakAttributeInfo.Create(),
            FullNames.FluentReturnAttribute =>
                FluentReturnAttributeInfo.Create(attributeDataExtended.AttributeData),
            FullNames.FluentSkippableAttribute =>
                FluentSkippableAttributeInfo.Create(),
            _ => throw new Exception($"Unexpected attribute class name: {attributeDataExtended.FullName}")
        };
    }

    private FluentLambdaAttributeInfo? CreateFluentLambdaAttributeInfo(
        AttributeDataExtended attributeDataExtended,
        string memberName,
        ISymbol symbol)
    {
        LambdaBuilderInfo? lambdaBuilderInfo = GetLambdaBuilderInfo(attributeDataExtended, symbol);
        return lambdaBuilderInfo == null
            ? null
            : FluentLambdaAttributeInfo.Create(attributeDataExtended.AttributeData, memberName, lambdaBuilderInfo);
    }

    private LambdaBuilderInfo? GetLambdaBuilderInfo(AttributeDataExtended attributeDataExtended, ISymbol symbol)
    {
        ITypeSymbol type = symbol switch
        {
            IPropertySymbol propertySymbol => propertySymbol.Type,
            IFieldSymbol fieldSymbol => fieldSymbol.Type,
            _ => throw new GenerationException($"Unexpected symbol type: {symbol.GetType().Name}")
        };

        AttributeDataExtended[] fluentApiAttributes = type.GetAttributes().Select(AttributeDataExtended.Create)
            .OfType<AttributeDataExtended>().Where(a => a.FullName == FullNames.FluentApiAttribute)
            .ToArray();

        if (fluentApiAttributes.Length == 0)
        {
            classInfoReport.ReportDiagnostic(
                FluentLambdaMemberWithoutFluentApi.CreateDiagnostic(attributeDataExtended, type.Name));
            return null;
        }

        // M31FA004 ensures that there is at most one FluentApi attribute.
        AttributeDataExtended fluentApiAttribute = fluentApiAttributes.First();

        FluentApiAttributeInfo info = FluentApiAttributeInfo.Create(fluentApiAttribute.AttributeData, type.Name);
        string builderClassName = info.BuilderClassName; // CreateAddress
        string fluentApiTypeForCodeGeneration = CodeTypeExtractor.GetTypeForCodeGeneration(type); // Namespace.Address
        string builderTypeForCodeGeneration =
            GetBuilderTypeForCodeGeneration(
                fluentApiTypeForCodeGeneration,
                builderClassName); // Namespace.CreateAddress.
        return new LambdaBuilderInfo(builderClassName, builderTypeForCodeGeneration);
    }

    private static string GetBuilderTypeForCodeGeneration(
        string fluentApiTypeForCodeGeneration,
        string builderClassName)
    {
        string[] split = fluentApiTypeForCodeGeneration.Split('.');
        return string.Join(".", split.Take(split.Length - 1).Concat(new string[] { builderClassName }));
    }
}