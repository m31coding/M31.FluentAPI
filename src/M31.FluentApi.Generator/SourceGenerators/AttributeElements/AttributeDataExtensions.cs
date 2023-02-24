using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal static class AttributeDataExtensions
{
    internal static TArg GetConstructorArguments<TArg>(this AttributeData attributeData)
    {
        return (TArg)attributeData.ConstructorArguments[0].Value!;
    }

    internal static (TArg1, TArg2) GetConstructorArguments<TArg1, TArg2>(this AttributeData attributeData)
    {
        return ((TArg1)attributeData.ConstructorArguments[0].Value!,
            (TArg2)attributeData.ConstructorArguments[1].Value!);
    }

    internal static (TArg1, TArg2, TArg3) GetConstructorArguments<TArg1, TArg2, TArg3>(this AttributeData attributeData)
    {
        return ((TArg1)attributeData.ConstructorArguments[0].Value!,
            (TArg2)attributeData.ConstructorArguments[1].Value!,
            (TArg3)attributeData.ConstructorArguments[2].Value!);
    }

    internal static (TArg1, TArg2, TArg3, TArg4) GetConstructorArguments<TArg1, TArg2, TArg3, TArg4>(
        this AttributeData attributeData)
    {
        return ((TArg1)attributeData.ConstructorArguments[0].Value!,
            (TArg2)attributeData.ConstructorArguments[1].Value!,
            (TArg3)attributeData.ConstructorArguments[2].Value!,
            (TArg4)attributeData.ConstructorArguments[3].Value!);
    }

    internal static (TArg1, TArg2, TArg3, TArg4, TArg5) GetConstructorArguments<TArg1, TArg2, TArg3, TArg4, TArg5>(
        this AttributeData attributeData)
    {
        return ((TArg1)attributeData.ConstructorArguments[0].Value!,
            (TArg2)attributeData.ConstructorArguments[1].Value!,
            (TArg3)attributeData.ConstructorArguments[2].Value!,
            (TArg4)attributeData.ConstructorArguments[3].Value!,
            (TArg5)attributeData.ConstructorArguments[4].Value!);
    }
}