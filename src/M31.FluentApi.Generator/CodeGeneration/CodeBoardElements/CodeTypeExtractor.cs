using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal static class CodeTypeExtractor
{
    internal static string GetTypeForCodeGeneration(ITypeSymbol typeSymbol)
    {
        SymbolDisplayMiscellaneousOptions miscellaneousOptions =
            SymbolDisplayMiscellaneousOptions.UseSpecialTypes |
            SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier;

        SymbolDisplayFormat format =
            new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
                miscellaneousOptions: miscellaneousOptions);

        return typeSymbol.ToDisplayString(format);
    }
}