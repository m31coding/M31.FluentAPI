using System.Composition;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        SyntaxNode node = root.FindNode(context.Span);

        if (node is not MemberDeclarationSyntax memberSyntax)
        {
            return;
        }

        if (!memberSyntax.Parent.IsClassStructOrRecordSyntax(out TypeDeclarationSyntax typeSyntax))
        {
            return;
        }

        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (!typeSyntax.AttributeLists
                .SelectMany(list => list.Attributes)
                .Any(attr => attr.IsFluentApiAttributeSyntax()))
        {
            return;
        }

        SemanticModel? semanticModel = await document.GetSemanticModelAsync(context.CancellationToken);
        if (semanticModel == null)
        {
            return;
        }

        ISymbol? memberSymbol = semanticModel.GetDeclaredSymbol(memberSyntax, context.CancellationToken);
        if (memberSymbol == null)
        {
            return;
        }

        if (context.CancellationToken.IsCancellationRequested)
        {
            return;
        }

        AttributeDataExtended[] attributeDataExtended =
            memberSymbol.GetAttributes()
                .Select(AttributeDataExtended.Create)
                .OfType<AttributeDataExtended>()
                .ToArray();

        if (!attributeDataExtended.Any(a => Attributes.IsMainAttribute(a.FullName)))
        {
            return;
        }

        CodeAction action = CodeAction.Create("Create fluent API documentation comments",
            c => AddFluentSummaryAsync(
                memberSyntax, memberSymbol, context.Document, root, semanticModel, typeSyntax, c),
            nameof(FluentApiCommentsProvider));

        context.RegisterRefactoring(action);
    }

    private async Task<Document> AddFluentSummaryAsync(
        MemberDeclarationSyntax memberSyntax,
        ISymbol memberSymbol,
        Document document,
        SyntaxNode root,
        SemanticModel semanticModel,
        TypeDeclarationSyntax typeSyntax,
        CancellationToken cancellationToken)
    {
        ClassInfoResult classInfoResult =
            ClassInfoFactory.CreateFluentApiClassInfo(
                semanticModel,
                typeSyntax,
                SourceGenerator.GeneratorConfig,
                cancellationToken);

        if (classInfoResult.ClassInfo == null)
        {
            return document;
        }

        // todo: handle groups.
        FluentApiInfo? info =
            classInfoResult.ClassInfo.FluentApiInfos.FirstOrDefault(i => i.SymbolInfo.Name == memberSymbol.Name);

        if (info == null)
        {
            return document;
        }

        SyntaxTriviaList leadingTrivia = memberSyntax.GetLeadingTrivia();
        string padding = GetPadding(leadingTrivia);
        string nl = classInfoResult.ClassInfo.NewLineString;

        var xmlComment = SyntaxFactory.TriviaList(
            SyntaxFactory.Comment($"/// <fluentSummary>{nl}"),
            SyntaxFactory.Comment($"{padding}/// ...{nl}"),
            SyntaxFactory.Comment($"{padding}/// </fluentSummary>{nl}{padding}"));

        MemberDeclarationSyntax newMemberSyntax = memberSyntax.WithLeadingTrivia(leadingTrivia.AddRange(xmlComment));

        SyntaxNode newRoot = root.ReplaceNode(memberSyntax, newMemberSyntax);
        return document.WithSyntaxRoot(newRoot);
    }

    private static string GetPadding(SyntaxTriviaList leadingTrivia)
    {
        const string fallback = "    ";
        SyntaxTrivia? first = leadingTrivia.FirstOrDefault();
        if (first == null || first.Value.Kind() != SyntaxKind.WhitespaceTrivia)
        {
            return fallback;
        }

        return first.Value.ToString();
    }
}