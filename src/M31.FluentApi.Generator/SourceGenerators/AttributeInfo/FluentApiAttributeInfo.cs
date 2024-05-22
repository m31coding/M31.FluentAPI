using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record FluentApiAttributeInfo
{
    private FluentApiAttributeInfo(string builderClassName)
    {
        BuilderClassName = builderClassName;
    }

    internal string BuilderClassName { get; }

    internal static FluentApiAttributeInfo Create(AttributeData attributeData, string className)
    {
        string builderClassName = attributeData.GetConstructorArguments<string>();
        builderClassName = NameCreator.CreateName(builderClassName, className);
        return new FluentApiAttributeInfo(builderClassName);
    }
}