using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal abstract class CollectionMethodCreator
{
    private readonly FluentCollectionAttributeInfo collectionAttributeInfo;
    private readonly string genericTypeArgument;
    private readonly MemberSymbolInfo symbolInfo;

    internal CollectionMethodCreator(
        FluentCollectionAttributeInfo collectionAttributeInfo,
        string genericTypeArgument,
        MemberSymbolInfo symbolInfo)
    {
        this.collectionAttributeInfo = collectionAttributeInfo;
        this.genericTypeArgument = genericTypeArgument;
        this.symbolInfo = symbolInfo;
    }

    internal BuilderMethod? CreateWithItemsMethod(MethodCreator methodCreator)
    {
        return symbolInfo.TypeForCodeGeneration == $"{genericTypeArgument}[]"
            ? null
            : methodCreator.CreateMethod(symbolInfo, collectionAttributeInfo.WithItems);
    }

    internal BuilderMethod? CreateWithItemsParamsMethod(MethodCreator methodCreator)
    {
        Parameter parameter = new Parameter($"params {genericTypeArgument}[]", symbolInfo.NameInCamelCase);
        return methodCreator.CreateMethodWithComputedValue(
            symbolInfo,
            collectionAttributeInfo.WithItems,
            parameter,
            p => CreateCollectionFromArray(genericTypeArgument, p));
    }

    internal BuilderMethod CreateWithItemMethod(MethodCreator methodCreator)
    {
        Parameter parameter = new Parameter(genericTypeArgument, collectionAttributeInfo.SingularNameInCamelCase);
        return methodCreator.CreateMethodWithComputedValue(
            symbolInfo,
            collectionAttributeInfo.WithItem,
            parameter,
            p => CreateCollectionFromSingleItem(genericTypeArgument, p));
    }

    internal BuilderMethod CreateWithZeroItemsMethod(MethodCreator methodCreator)
    {
        string collectionWithZeroItemsCode = CreateCollectionWithZeroItems(genericTypeArgument);
        return methodCreator.CreateMethodWithFixedValue(
            symbolInfo,
            collectionAttributeInfo.WithZeroItems,
            collectionWithZeroItemsCode);
    }

    protected abstract string CreateCollectionFromArray(string genericTypeArgument, string arrayParameter);
    protected abstract string CreateCollectionFromSingleItem(string genericTypeArgument, string itemParameter);
    protected abstract string CreateCollectionWithZeroItems(string genericTypeArgument);
    internal abstract IReadOnlyCollection<string> RequiredUsings { get; }
}