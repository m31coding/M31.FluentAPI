using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentCollectionAttributeInfo : AttributeInfoBase
{
    private FluentCollectionAttributeInfo(
        int builderStep,
        string singularName,
        string withItems,
        string? withItem,
        string? withZeroItems,
        LambdaBuilderInfo? lambdaBuilderInfo)
        : base(builderStep)
    {
        SingularName = singularName;
        SingularNameInCamelCase = singularName.FirstCharToLower();
        WithItems = withItems;
        WithItem = withItem;
        WithZeroItems = withZeroItems;
        LambdaBuilderInfo = lambdaBuilderInfo;
    }

    internal string SingularName { get; }
    internal string SingularNameInCamelCase { get; }
    internal string WithItems { get; }
    internal string? WithItem { get; }
    internal string? WithZeroItems { get; }
    internal LambdaBuilderInfo? LambdaBuilderInfo { get; }

    internal override IReadOnlyList<string> FluentMethodNames =>
        new string?[] { WithItems, WithItem, WithZeroItems }.OfType<string>().ToArray();

    internal static FluentCollectionAttributeInfo Create(
        AttributeData attributeData,
        string memberName,
        LambdaBuilderInfo? lambdaBuilderInfo)
    {
        (int builderStep, string singularName, string withItems, string? withItem, string? withZeroItems) =
            attributeData.GetConstructorArguments<int, string, string, string?, string?>();

        withItems = NameCreator.CreateName(withItems, memberName, singularName);

        if (withItem != null)
        {
            withItem = NameCreator.CreateName(withItem, memberName, singularName);
        }

        if (withZeroItems != null)
        {
            withZeroItems = NameCreator.CreateName(withZeroItems, memberName, singularName);
        }

        return new FluentCollectionAttributeInfo(
            builderStep, singularName, withItems, withItem, withZeroItems, lambdaBuilderInfo);
    }
}