using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentBreakAttributeInfo : ControlAttributeInfoBase
{
    internal static FluentBreakAttributeInfo Create(AttributeData attributeData)
    {
        return new FluentBreakAttributeInfo();
    }
}