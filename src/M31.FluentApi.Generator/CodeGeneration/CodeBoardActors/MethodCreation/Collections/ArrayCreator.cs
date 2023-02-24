using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal class ArrayCreator : CollectionMethodCreator
{
    internal ArrayCreator(
        FluentCollectionAttributeInfo collectionAttributeInfo,
        string genericTypeArgument,
        MemberSymbolInfo symbolInfo)
        : base(collectionAttributeInfo, genericTypeArgument, symbolInfo)
    {
    }

    protected override string CreateCollectionFromArray(string genericTypeArgument, string arrayParameter)
    {
        return arrayParameter;
    }

    protected override string CreateCollectionFromSingleItem(string genericTypeArgument, string itemParameter)
    {
        return $"new {genericTypeArgument}[1]{{ {itemParameter} }}";
    }

    protected override string CreateCollectionWithZeroItems(string genericTypeArgument)
    {
        return $"new {genericTypeArgument}[0]";
    }

    internal override IReadOnlyCollection<string> RequiredUsings => Array.Empty<string>();
}