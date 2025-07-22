using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentMethodAttributeInfo : AttributeInfoBase
{
    private FluentMethodAttributeInfo(int builderStep, string method)
        : base(builderStep)
    {
        Method = method;
    }

    internal string Method { get; }
    internal override string FluentMethodName => Method;
    internal override IReadOnlyCollection<string> FluentMethodNames => new string[] { Method };

    internal static FluentMethodAttributeInfo Create(AttributeData attributeData, string memberName)
    {
        (int builderStep, string method) = attributeData.GetConstructorArguments<int, string>();
        method = NameCreator.CreateName(method, memberName);
        return new FluentMethodAttributeInfo(builderStep, method);
    }
}