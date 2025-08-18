using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentPredicateAttributeInfo : AttributeInfoBase
{
    private FluentPredicateAttributeInfo(int builderStep, string method, string negatedMethod)
        : base(builderStep)
    {
        Method = method;
        NegatedMethod = negatedMethod;
    }

    internal string Method { get; }
    internal string NegatedMethod { get; }
    internal override IReadOnlyList<string> FluentMethodNames => new string[] { Method, NegatedMethod };

    internal static FluentPredicateAttributeInfo Create(AttributeData attributeData, string memberName)
    {
        (int builderStep, string method, string negatedMethod) =
            attributeData.GetConstructorArguments<int, string, string>();
        method = NameCreator.CreateName(method, memberName);
        negatedMethod = NameCreator.CreateName(negatedMethod, memberName);
        return new FluentPredicateAttributeInfo(builderStep, method, negatedMethod);
    }
}