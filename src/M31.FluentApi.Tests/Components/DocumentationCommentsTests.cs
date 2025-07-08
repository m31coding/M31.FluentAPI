using M31.FluentApi.Generator.SourceGenerators.DocumentationComments;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Xunit;

namespace M31.FluentApi.Tests.Components;

public class DocumentationCommentsTests
{

    public string Property1 { get; private set; }

    [Fact]
    public void CanParseCommentsTest1()
    {
       string sourceCode = @"
            namespace M31.FluentApi.Tests.Components
            {
                public class DocumentationCommentsTests
                {
                    /// <fluentSummary>
                    /// Sets Property1.
                    /// </fluentSummary>
                    public string Property1 { get; private set; }
                }
            }";

        IPropertySymbol propertySymbol = GetPropertySymbol(sourceCode, "Property1");
        string commentXml = propertySymbol.GetDocumentationCommentXml()!;
        Comments comments = Comments.Parse(commentXml);
        Assert.Equal(1, comments.List.Count);
        Comment comment = comments.List[0];
        Assert.Equal("fluentSummary", comment.Tag);
        Assert.Equal(0, comment.Attributes.Count);
        Assert.Equal("Sets Property1.", comment.Content);
    }

    private static IPropertySymbol GetPropertySymbol(string code, string propertyName)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
        CSharpCompilation compilation = CSharpCompilation.Create(
            "TestCompilation",
            syntaxTrees: new[] { tree });

        SemanticModel semanticModel = compilation.GetSemanticModel(tree);
        SyntaxNode root = tree.GetRoot();

        PropertyDeclarationSyntax propertyDecl = root
            .DescendantNodes()
            .OfType<PropertyDeclarationSyntax>()
            .First(p => p.Identifier.Text == propertyName);

        return semanticModel.GetDeclaredSymbol(propertyDecl)!;
    }
}