using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace M31.FluentApi.Generator.SourceAnalyzers;

[ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(AddFluentSummaryRefactoringProvider)), Shared]
public class AddFluentSummaryRefactoringProvider : CodeRefactoringProvider
{
    public override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
    {
        //var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        //if (root == null)
        //    return;

        //var span = context.Span;
        //var token = root.FindToken(span.Start);

        //// Check if the user is on a method identifier
        //var methodDeclaration = token.Parent?.AncestorsAndSelf().OfType<MethodDeclarationSyntax>().FirstOrDefault();
        //if (methodDeclaration == null)
        //    return;

        //// Only offer if no existing doc comment
        //if (methodDeclaration.GetLeadingTrivia().Any(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia)))
        //    return;

        // Register code action
        var action = CodeAction.Create("Add <fluentSummary> doc comment",
            c => AddFluentSummaryAsync(context.Document, null!, c),
            nameof(AddFluentSummaryRefactoringProvider));

        context.RegisterRefactoring(action);
    }

    private async Task<Document> AddFluentSummaryAsync(Document document, MethodDeclarationSyntax methodDecl, CancellationToken cancellationToken)
    {
        return document;
        var leadingTrivia = methodDecl.GetLeadingTrivia();

        var xmlComment = SyntaxFactory.TriviaList(
            SyntaxFactory.Comment("/// <fluentSummary>"),
            SyntaxFactory.CarriageReturnLineFeed,
            SyntaxFactory.Comment("/// ..."),
            SyntaxFactory.CarriageReturnLineFeed,
            SyntaxFactory.Comment("/// </fluentSummary>"),
            SyntaxFactory.CarriageReturnLineFeed
        );

        var newMethod = methodDecl.WithLeadingTrivia(xmlComment.AddRange(leadingTrivia));

        var root = await document.GetSyntaxRootAsync(cancellationToken);
        if (root == null)
            return document;

        var newRoot = root.ReplaceNode(methodDecl, newMethod);
        return document.WithSyntaxRoot(newRoot);
    }
}
