using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentSkippableAttributeInfo : ControlAttributeInfoBase
{
    internal static FluentSkippableAttributeInfo Create()
    {
        return new FluentSkippableAttributeInfo();
    }
}