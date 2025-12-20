using Microsoft.CodeAnalysis.CSharp;

namespace M31.FluentApi.Generator.Commons;

internal static class CSharpKeywords
{
    internal static bool IsCSharpKeyword(string name)
    {
        return SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None;
    }
}