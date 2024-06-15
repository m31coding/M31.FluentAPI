using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal class HashSetCreator : CollectionMethodCreator
{
    internal HashSetCreator(
        FluentCollectionAttributeInfo collectionAttributeInfo,
        string genericTypeArgument,
        MemberSymbolInfo symbolInfo)
        : base(collectionAttributeInfo, genericTypeArgument, symbolInfo)
    {
        RequiredUsings.Add("System.Collections.Generic");
    }

    protected override string CreateCollectionFromArray(string genericTypeArgument, string arrayParameter)
    {
        return CreateCollectionFromEnumerable(genericTypeArgument, arrayParameter);
    }

    protected override string CreateCollectionFromEnumerable(string genericTypeArgument, string enumerableParameter)
    {
        return $"new HashSet<{genericTypeArgument}>({enumerableParameter})";
    }

    protected override string CreateCollectionFromSingleItem(string genericTypeArgument, string itemParameter)
    {
        return $"new HashSet<{genericTypeArgument}>(1){{ {itemParameter} }}";
    }

    protected override string CreateCollectionWithZeroItems(string genericTypeArgument)
    {
        return $"new HashSet<{genericTypeArgument}>(0)";
    }
}