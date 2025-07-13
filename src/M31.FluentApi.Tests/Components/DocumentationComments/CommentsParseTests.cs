using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Xunit;

namespace M31.FluentApi.Tests.Components.DocumentationComments;

public class CommentsParseTests
{
    [Fact]
    public void CanParseSingleTag()
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

    [Fact]
    public void CanParseMultipleTagsAndAttributes()
    {
        string sourceCode = @"
            namespace M31.FluentApi.Tests.Components
            {
                public class DocumentationCommentsTests
                {
                    /// <fluentSummary method=""WithProperty1"">
                    /// Sets Property1.
                    /// </fluentSummary>
                    /// <fluentParam method=""WithProperty1"" name=""property1"">
                    /// The new value of Property1.
                    /// </fluentParam>
                    public string Property1 { get; private set; }
                }
            }";

        IPropertySymbol propertySymbol = GetPropertySymbol(sourceCode, "Property1");
        string commentXml = propertySymbol.GetDocumentationCommentXml()!;
        Comments comments = Comments.Parse(commentXml);
        Assert.Equal(2, comments.List.Count);

        Comment comment1 = comments.List[0];
        Assert.Equal("fluentSummary", comment1.Tag);
        Assert.Equal(1, comment1.Attributes.Count);
        Assert.Equal("method", comment1.Attributes[0].Key);
        Assert.Equal("WithProperty1", comment1.Attributes[0].Value);
        Assert.Equal("Sets Property1.", comment1.Content);

        Comment comment2 = comments.List[1];
        Assert.Equal("fluentParam", comment2.Tag);
        Assert.Equal(2, comment2.Attributes.Count);
        Assert.Equal("method", comment2.Attributes[0].Key);
        Assert.Equal("WithProperty1", comment2.Attributes[0].Value);
        Assert.Equal("name", comment2.Attributes[1].Key);
        Assert.Equal("property1", comment2.Attributes[1].Value);
        Assert.Equal("The new value of Property1.", comment2.Content);
    }

    [Fact]
    public void CanIgnoreNonFluentTagsInParsing()
    {
        string sourceCode = @"
            namespace M31.FluentApi.Tests.Components
            {
                public class DocumentationCommentsTests
                {
                    /// <Summary>
                    /// Gets or sets Property1.
                    /// </Summary>
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