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
        string withItem,
        string withZeroItems)
        : base(builderStep)
    {
        SingularName = singularName;
        SingularNameInCamelCase = singularName.FirstCharToLower();
        WithItems = withItems;
        WithItem = withItem;
        WithZeroItems = withZeroItems;
    }

    internal string SingularName { get; }
    internal string SingularNameInCamelCase { get; }
    internal string WithItems { get; }
    internal string WithItem { get; }
    internal string WithZeroItems { get; }
    internal override string FluentMethodName => WithItems;

    internal static FluentCollectionAttributeInfo Create(AttributeData attributeData, string memberName)
    {
        (int builderStep, string singularName, string withItems, string withItem, string withZeroItems) =
            attributeData.GetConstructorArguments<int, string, string, string, string>();

        withItems = NameCreator.CreateName(withItems, memberName, singularName);
        withItem = NameCreator.CreateName(withItem, memberName, singularName);
        withZeroItems = NameCreator.CreateName(withZeroItems, memberName, singularName);

        return new FluentCollectionAttributeInfo(builderStep, singularName, withItems, withItem, withZeroItems);
    }
}