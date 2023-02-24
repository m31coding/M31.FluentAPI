using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.Commons;

internal static class DeclaredAccessibilityExtensions
{
    internal static bool IsPublicOrInternal(this Accessibility accessibility)
    {
        return accessibility is Accessibility.Public or Accessibility.Internal;
    }
}