using System.Composition;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace M31.FluentApi.Generator.SourceAnalyzers;

[ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(FluentApiCommentsProvider))]
[Shared]
internal class FluentApiCommentsProvider : CodeRefactoringProvider
{
    public override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
    {
        Document document = context.Document;

        SyntaxNode? root = await document.GetSyntaxRootAsync(context.CancellationToken);
        if (root == null)
        {
            return;
        }

        // SemanticModel? semanticModel = await document.GetSemanticModelAsync(context.CancellationToken);
        // if (semanticModel == null)
        // {
        //     return;
        // }

        SyntaxNode node = root.FindNode(context.Span);

        // Check for property, field, or method declaration
        if (node is not MemberDeclarationSyntax memberSyntax)
        {
            return;
        }

        if (!memberSyntax.Parent.IsClassStructOrRecordSyntax(out TypeDeclarationSyntax typeSyntax))
        {
            return;
        }

        if (!typeSyntax.AttributeLists
                .SelectMany(list => list.Attributes)
                .Any(attr => attr.IsFluentApiAttributeSyntax()))
        {
            return;
        }

        // // Only offer refactoring if member has [FluentMember] or no comment yet
        // var memberSymbol = semanticModel.GetDeclaredSymbol(memberSyntax, context.CancellationToken);
        // if (memberSymbol == null)
        //     return;
        //
        // bool hasFluentMember = memberSymbol.GetAttributes().Any(attr =>
        //     attr.AttributeClass?.Name == "FluentMemberAttribute" ||
        //     attr.AttributeClass?.ToDisplayString() == "FluentMemberAttribute");
        //
        // if (!hasFluentMember)
        //     return;

        CodeAction action = CodeAction.Create("Add <fluentSummary> doc comment b",
            c => AddFluentSummaryAsync(context.Document, null!, c),
            nameof(FluentApiCommentsProvider));

        context.RegisterRefactoring(action);
    }

    private async Task<Document> AddFluentSummaryAsync(Document document, MethodDeclarationSyntax methodDecl,
        CancellationToken cancellationToken)
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