using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentLambdaAttributeInfo : AttributeInfoBase
{
    private FluentLambdaAttributeInfo(int builderStep, string method, LambdaBuilderInfo builderInfo)
        : base(builderStep)
    {
        Method = method;
        BuilderInfo = builderInfo;
    }

    internal string Method { get; }
    internal LambdaBuilderInfo BuilderInfo { get; }
    internal override IReadOnlyList<string> FluentMethodNames => new string[] { Method };

    internal static FluentLambdaAttributeInfo Create(
        AttributeData attributeData,
        string memberName,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        (int builderStep, string method) = attributeData.GetConstructorArguments<int, string>();
        method = NameCreator.CreateName(method, memberName);
        return new FluentLambdaAttributeInfo(builderStep, method, lambdaBuilderInfo);
    }
}