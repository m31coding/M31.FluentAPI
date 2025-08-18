using System;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Tests.Helpers;

internal static class SyntaxExtensions
{
    internal static TypeDeclarationSyntax? GetFluentApiTypeDeclaration(this SyntaxTree syntaxTree)
    {
        SyntaxNode root = syntaxTree.GetRoot();
        TypeDeclarationSyntax? typeDeclaration =
            (TypeDeclarationSyntax?)root.Find(n => n.IsClassStructOrRecordSyntax());
        return typeDeclaration;
    }

    internal static SyntaxNode? Find(this SyntaxNode tree, Predicate<SyntaxNode> predicate)
    {
        if (predicate(tree))
        {
            return tree;
        }

        foreach (SyntaxNode child in tree.ChildNodes())
        {
            SyntaxNode? found = Find(child, predicate);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}