using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentContinueWithAttributeInfo : ControlAttributeInfoBase
{
    private FluentContinueWithAttributeInfo(int continueWithBuilderStep)
    {
        ContinueWithBuilderStep = continueWithBuilderStep;
    }

    internal int ContinueWithBuilderStep { get; }

    internal static FluentContinueWithAttributeInfo Create(AttributeData attributeData)
    {
        int builderStep = attributeData.GetConstructorArguments<int>();
        return new FluentContinueWithAttributeInfo(builderStep);
    }
}