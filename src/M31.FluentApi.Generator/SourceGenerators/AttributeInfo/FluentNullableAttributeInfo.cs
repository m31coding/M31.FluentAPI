using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentNullableAttributeInfo : OrthogonalAttributeInfoBase
{
    private FluentNullableAttributeInfo(string method)
    {
        Method = method;
    }

    internal string Method { get; }
    internal override IReadOnlyCollection<string> FluentMethodNames => new string[] { Method };

    internal static FluentNullableAttributeInfo Create(AttributeData attributeData, string memberName)
    {
        string method = attributeData.GetConstructorArguments<string>();
        method = NameCreator.CreateName(method, memberName);
        return new FluentNullableAttributeInfo(method);
    }
}