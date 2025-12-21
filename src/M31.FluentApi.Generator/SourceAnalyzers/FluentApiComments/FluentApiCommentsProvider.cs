using System.Composition;
using M31.FluentApi.Generator.CodeGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Generator.SourceAnalyzers.FluentApiComments;

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

    private Task<Document> AddFluentSummaryAsync(
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
            return Task.FromResult(document);
        }

        FluentApiInfoGroup? group = classInfoResult.ClassInfo.AdditionalInfo.FluentApiInfoGroups.FirstOrDefault(
            g => g.FluentApiInfos.Any(i => i.SymbolInfo.Name == memberSymbol.Name));

        if (group == null)
        {
            return Task.FromResult(document);
        }

        if (group.IsCompoundGroup && group.FluentApiInfos.First().SymbolInfo.Name != memberSymbol.Name)
        {
            memberSymbol = group.FluentApiInfos.First().AdditionalInfo.Symbol;
            MemberDeclarationSyntax? firstMemberSyntax =
                memberSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() as MemberDeclarationSyntax;
            if (firstMemberSyntax == null)
            {
                return Task.FromResult(document);
            }

            memberSyntax = firstMemberSyntax;
        }

        Dictionary<FluentApiInfoGroup, BuilderMethods> groupToMethods =
            CodeGenerator.GenerateBuilderMethods(classInfoResult.ClassInfo, cancellationToken);
        BuilderMethods builderMethods = groupToMethods[group];
        List<string> commentsTemplate = MethodsToCommentsTemplate.CreateCommentsTemplate(builderMethods);

        if (commentsTemplate.Count <= 2)
        {
            return Task.FromResult(document);
        }

        SyntaxTriviaList leadingTrivia = memberSyntax.GetLeadingTrivia();
        string padding = GetPadding(leadingTrivia);
        string nl = classInfoResult.ClassInfo.NewLineString;

        commentsTemplate[0] = $"{commentsTemplate[0]}{nl}";
        for (int i = 1; i < commentsTemplate.Count - 1; i++)
        {
            commentsTemplate[i] = $"{padding}{commentsTemplate[i]}{nl}";
        }

        commentsTemplate[commentsTemplate.Count - 1] =
            $"{padding}{commentsTemplate[commentsTemplate.Count - 1]}{nl}{padding}";

        SyntaxTriviaList xmlComment =
            SyntaxFactory.TriviaList(commentsTemplate.Select(SyntaxFactory.Comment).ToArray());

        MemberDeclarationSyntax newMemberSyntax = memberSyntax.WithLeadingTrivia(leadingTrivia.AddRange(xmlComment));

        SyntaxNode newRoot = root.ReplaceNode(memberSyntax, newMemberSyntax);
        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }

    private static string GetPadding(SyntaxTriviaList leadingTrivia)
    {
        const string fallback = "    ";
        SyntaxTrivia? first = leadingTrivia.FirstOrDefault();
        if (first == null || !first.Value.IsKind(SyntaxKind.WhitespaceTrivia))
        {
            return fallback;
        }

        return first.Value.ToString();
    }
}