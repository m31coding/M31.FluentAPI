using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal class ListCreator : CollectionMethodCreator
{
    internal ListCreator(
        FluentCollectionAttributeInfo collectionAttributeInfo,
        string genericTypeArgument,
        MemberSymbolInfo symbolInfo)
        : base(collectionAttributeInfo, genericTypeArgument, symbolInfo)
    {
        RequiredUsings.Add("System.Collections.Generic");
    }

    protected override string CreateCollectionFromArray(string genericTypeArgument, string arrayParameter)
    {
        return $"new List<{genericTypeArgument}>({arrayParameter})";
    }

    protected override string CreateCollectionFromSingleItem(string genericTypeArgument, string itemParameter)
    {
        return $"new List<{genericTypeArgument}>(1){{ {itemParameter} }}";
    }

    protected override string CreateCollectionWithZeroItems(string genericTypeArgument)
    {
        return $"new List<{genericTypeArgument}>(0)";
    }
}