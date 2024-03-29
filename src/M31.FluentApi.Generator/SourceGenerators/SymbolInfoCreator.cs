using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.Collections;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal static class SymbolInfoCreator
{
    internal static FluentApiSymbolInfo Create(ISymbol symbol)
    {
        return symbol switch
        {
            IPropertySymbol propertySymbol => CreateMemberSymbolInfo(propertySymbol),
            IFieldSymbol fieldSymbol => CreateMemberSymbolInfo(fieldSymbol),
            IMethodSymbol methodSymbol => CreateMethodSymbolInfo(methodSymbol),
            _ => throw new ArgumentException($"Unexpected symbol type: {symbol.GetType()}."),
        };
    }

    private static MemberSymbolInfo CreateMemberSymbolInfo(IFieldSymbol fieldSymbol)
    {
        return new MemberSymbolInfo(
            fieldSymbol.Name,
            fieldSymbol.Type.ToString(),
            fieldSymbol.DeclaredAccessibility,
            RequiresReflection(fieldSymbol),
            CodeTypeExtractor.GetTypeForCodeGeneration(fieldSymbol.Type),
            fieldSymbol.NullableAnnotation == NullableAnnotation.Annotated,
            false,
            CollectionInference.InferCollectionType(fieldSymbol.Type));
    }

    private static MemberSymbolInfo CreateMemberSymbolInfo(IPropertySymbol propertySymbol)
    {
        return new MemberSymbolInfo(
            propertySymbol.Name,
            propertySymbol.Type.ToString(),
            propertySymbol.DeclaredAccessibility,
            RequiresReflection(propertySymbol),
            CodeTypeExtractor.GetTypeForCodeGeneration(propertySymbol.Type),
            propertySymbol.NullableAnnotation == NullableAnnotation.Annotated,
            true,
            CollectionInference.InferCollectionType(propertySymbol.Type));
    }

    private static MethodSymbolInfo CreateMethodSymbolInfo(IMethodSymbol methodSymbol)
    {
        IReadOnlyCollection<ParameterSymbolInfo> parameterInfos =
            methodSymbol.Parameters.Select(CreateParameterSymbolInfo).ToArray();

        return new MethodSymbolInfo(methodSymbol.Name, methodSymbol.DeclaredAccessibility,
            RequiresReflection(methodSymbol), parameterInfos);
    }

    private static bool RequiresReflection(IFieldSymbol fieldSymbol)
    {
        bool isWritable = !fieldSymbol.IsReadOnly;
        return RequiresReflection(fieldSymbol.DeclaredAccessibility, isWritable);
    }

    private static bool RequiresReflection(IMethodSymbol methodSymbol)
    {
        return !methodSymbol.DeclaredAccessibility.IsPublicOrInternal();
    }

    private static bool RequiresReflection(IPropertySymbol propertySymbol)
    {
        bool isWritable = false;

        if (propertySymbol.SetMethod != null)
        {
            isWritable = propertySymbol.SetMethod.DeclaredAccessibility.IsPublicOrInternal() &&
                         !propertySymbol.SetMethod.IsInitOnly;
        }

        return RequiresReflection(propertySymbol.DeclaredAccessibility, isWritable);
    }

    private static bool RequiresReflection(Accessibility declaredAccessibility, bool isWritable)
    {
        return !declaredAccessibility.IsPublicOrInternal() || !isWritable;
    }

    private static ParameterSymbolInfo CreateParameterSymbolInfo(IParameterSymbol parameterSymbol)
    {
        return new ParameterSymbolInfo(
            parameterSymbol.Name,
            CodeTypeExtractor.GetTypeForCodeGeneration(parameterSymbol.Type),
            parameterSymbol.NullableAnnotation == NullableAnnotation.Annotated,
            GetDefaultValueAsCode(parameterSymbol));
    }

    private static string? GetDefaultValueAsCode(IParameterSymbol parameterSymbol)
    {
        if (!parameterSymbol.HasExplicitDefaultValue)
        {
            return null;
        }

        if (parameterSymbol.ExplicitDefaultValue == null && parameterSymbol.Type.IsReferenceType)
        {
            return "null";
        }

        if (parameterSymbol.ExplicitDefaultValue == null && parameterSymbol.Type.IsValueType)
        {
            return "default";
        }

        if (parameterSymbol.ExplicitDefaultValue == null &&
            parameterSymbol.Type is { IsReferenceType: false, IsValueType: false }) // unconstrained type parameter
        {
            return "default";
        }

        return parameterSymbol.ExplicitDefaultValue! switch
        {
            string s => $"\"{s}\"",
            { } o => o.ToString(),
        };
    }
}