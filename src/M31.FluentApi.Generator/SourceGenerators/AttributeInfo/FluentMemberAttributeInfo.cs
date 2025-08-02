using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentMemberAttributeInfo : AttributeInfoBase
{
    private FluentMemberAttributeInfo(
        int builderStep,
        string method,
        int parameterPosition,
        LambdaBuilderInfo? lambdaBuilderInfo)
        : base(builderStep)
    {
        Method = method;
        ParameterPosition = parameterPosition;
        LambdaBuilderInfo = lambdaBuilderInfo;
    }

    internal string Method { get; }
    internal int ParameterPosition { get; }
    internal override IReadOnlyList<string> FluentMethodNames => new string[] { Method };
    internal LambdaBuilderInfo? LambdaBuilderInfo { get; } // Todo: add to fluent method names, also for the FluentCollection.

    internal static FluentMemberAttributeInfo Create(
        AttributeData attributeData,
        string memberName,
        LambdaBuilderInfo? lambdaBuilderInfo)
    {
        (int builderStep, string method, int parameter) = attributeData.GetConstructorArguments<int, string, int>();
        method = NameCreator.CreateName(method, memberName);
        return new FluentMemberAttributeInfo(builderStep, method, parameter, lambdaBuilderInfo);
    }
}