using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentReturnAttributeInfo : ControlAttributeInfoBase
{
    internal static FluentReturnAttributeInfo Create(AttributeData attributeData)
    {
        return new FluentReturnAttributeInfo();
    }
}