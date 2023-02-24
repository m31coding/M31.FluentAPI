using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal static class SymbolExtensions
{
    private static readonly SymbolDisplayFormat defaultFormat =
        new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

    internal static bool HasAttribute(this ISymbol symbol, string fullyQualifiedAttributeName)
    {
        return symbol.GetAttributes()
            .Any(a => a?.AttributeClass?.ToDefaultDisplayString() == fullyQualifiedAttributeName);
    }

    internal static bool HasAnyAttribute(this ISymbol symbol, params string[] fullyQualifiedAttributeNames)
    {
        return symbol.GetAttributes()
            .Any(a => fullyQualifiedAttributeNames.Any(n => a?.AttributeClass?.ToDefaultDisplayString() == n));
    }

    internal static string ToDefaultDisplayString(this ISymbol symbol)
    {
        return symbol.ToDisplayString(defaultFormat);
    }
}