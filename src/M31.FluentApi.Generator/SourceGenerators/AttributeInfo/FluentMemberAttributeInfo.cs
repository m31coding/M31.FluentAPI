using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentMemberAttributeInfo : AttributeInfoBase
{
    private FluentMemberAttributeInfo(int builderStep, string method, int parameterPosition)
        : base(builderStep)
    {
        Method = method;
        ParameterPosition = parameterPosition;
    }

    internal string Method { get; }
    internal int ParameterPosition { get; }
    internal override string FluentMethodName => Method;

    internal static FluentMemberAttributeInfo Create(AttributeData attributeData, string memberName)
    {
        (int builderStep, string method, int parameter) = attributeData.GetConstructorArguments<int, string, int>();
        method = NameCreator.CreateName(method, memberName);
        return new FluentMemberAttributeInfo(builderStep, method, parameter);
    }
}