using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Generator.Commons;

internal static class SyntaxNodeExtensions
{
    internal static bool IsClassStructOrRecordSyntax(
        this SyntaxNode? syntaxNode, out TypeDeclarationSyntax typeDeclaration)
    {
        if (syntaxNode.IsClassStructOrRecordSyntax())
        {
            typeDeclaration = (TypeDeclarationSyntax)syntaxNode!;
            return true;
        }

        typeDeclaration = null!;
        return false;
    }

    internal static bool IsClassStructOrRecordSyntax(this SyntaxNode? syntaxNode)
    {
        return syntaxNode is ClassDeclarationSyntax or StructDeclarationSyntax or RecordDeclarationSyntax;
    }

    internal static bool IsFluentApiAttributeSyntax(this AttributeSyntax attributeSyntax)
    {
        string? name = ExtractName(attributeSyntax.Name);

        // Note that we drop alias support for better performance.
        return name is "FluentApi" or "FluentApiAttribute";

        string? ExtractName(NameSyntax nameSyntax)
        {
            return nameSyntax switch
            {
                SimpleNameSyntax simpleNameSyntax => simpleNameSyntax.Identifier.Text, // without namespace
                QualifiedNameSyntax qualifiedNameSyntax => qualifiedNameSyntax.Right.Identifier.Text, // fully qualified
                _ => null
            };
        }
    }
}