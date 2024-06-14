using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal class CollectionMethods : IBuilderMethodCreator
{
    internal CollectionMethods(
        MemberSymbolInfo symbolInfo,
        FluentCollectionAttributeInfo collectionAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        CollectionAttributeInfo = collectionAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentCollectionAttributeInfo CollectionAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        if (SymbolInfo.CollectionType == null)
        {
            throw new GenerationException($"The collection type {SymbolInfo.Type} is not supported.");
        }

        string genericTypeArgument = SymbolInfo.CollectionType.GenericTypeArgument;

        CollectionMethodCreator collectionMethodCreator = SymbolInfo.CollectionType.Collection switch
        {
            GeneratedCollection.List =>
                new ListCreator(CollectionAttributeInfo, genericTypeArgument, SymbolInfo),
            GeneratedCollection.Array =>
                new ArrayCreator(CollectionAttributeInfo, genericTypeArgument, SymbolInfo),
            GeneratedCollection.HashSet =>
                new HashSetCreator(CollectionAttributeInfo, genericTypeArgument, SymbolInfo),
            _ => throw new ArgumentException(
                $"Collection type {SymbolInfo.CollectionType.Collection} is not supported.")
        };

        List<BuilderMethod?> builderMethods = new List<BuilderMethod?>()
        {
            collectionMethodCreator.CreateWithItemsMethod(methodCreator),
            collectionMethodCreator.CreateWithItemsParamsMethod(methodCreator),
            collectionMethodCreator.CreateWithItemMethod(methodCreator),
            collectionMethodCreator.CreateWithItemLambdaMethod(methodCreator),
            collectionMethodCreator.CreateWithZeroItemsMethod(methodCreator),
        };

        return new BuilderMethods(
            builderMethods.OfType<BuilderMethod>().ToList(),
            collectionMethodCreator.RequiredUsings);
    }
}