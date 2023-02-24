using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace M31.FluentApi.Tests.Components.Helpers;

internal class TypeSymbolProvider
{
    private readonly Dictionary<string, ITypeSymbol> propertyNameToTypeSymbol;

    private TypeSymbolProvider()
    {
        propertyNameToTypeSymbol = new Dictionary<string, ITypeSymbol>();
    }

    internal static TypeSymbolProvider Create(string code)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        CSharpCompilation compilation = CSharpCompilation.Create("CollectionAssembly")
            .AddReferences(
                MetadataReference.CreateFromFile(typeof(string).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(List<string>).Assembly.Location))
            .AddSyntaxTrees(tree);
        SemanticModel model = compilation.GetSemanticModel(tree);
        TypeSymbolProvider typeSymbolProvider = new TypeSymbolProvider();
        typeSymbolProvider.FindPropertyTypeSymbols(tree.GetRoot(), model);
        return typeSymbolProvider;
    }

    internal ITypeSymbol GetTypeSymbol(string propertyName)
    {
        return propertyNameToTypeSymbol[propertyName];
    }

    private void FindPropertyTypeSymbols(SyntaxNode tree, SemanticModel semanticModel)
    {
        ISymbol? symbol = semanticModel.GetDeclaredSymbol(tree);

        if (symbol is IPropertySymbol propertySymbol)
        {
            propertyNameToTypeSymbol[propertySymbol.Name] = propertySymbol.Type;
        }

        foreach (SyntaxNode child in tree.ChildNodes())
        {
            FindPropertyTypeSymbols(child, semanticModel);
        }
    }
}