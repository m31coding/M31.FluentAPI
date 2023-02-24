using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Generator.Commons;

internal static class SyntaxNodeExtensions
{
    internal static bool IsTypeDeclarationOfInterest(
        this SyntaxNode? syntaxNode, out TypeDeclarationSyntax typeDeclaration)
    {
        if (syntaxNode.IsTypeDeclarationOfInterest())
        {
            typeDeclaration = (TypeDeclarationSyntax)syntaxNode!;
            return true;
        }

        typeDeclaration = null!;
        return false;
    }

    internal static bool IsTypeDeclarationOfInterest(this SyntaxNode? syntaxNode)
    {
        return syntaxNode is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax;
    }
}