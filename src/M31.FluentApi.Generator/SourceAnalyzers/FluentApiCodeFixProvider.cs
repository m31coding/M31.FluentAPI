using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceAnalyzers;

[ExportCodeFixProvider(LanguageNames.CSharp)]
internal class FluentApiCodeFixProvider : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds { get; }
        = ImmutableArray.Create(
            MissingSetAccessor.Descriptor.Id,
            InvalidFluentMethodReturnType.Descriptor.Id);

    public override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        foreach (Diagnostic diagnostic in context.Diagnostics)
        {
            if (diagnostic.Id == MissingSetAccessor.Descriptor.Id)
            {
                FixCode(context, diagnostic, AddPrivateSetAsync);
            }

            if (diagnostic.Id == InvalidFluentMethodReturnType.Descriptor.Id)
            {
                FixCode(context, diagnostic, ChangeReturnTypeToVoid);
            }
        }

        return Task.CompletedTask;
    }

    private static void FixCode(CodeFixContext context, Diagnostic diagnostic,
        Func<CodeFixContext, Diagnostic, CancellationToken, Task<Document>> fixCode)
    {
        string title = diagnostic.Descriptor.Title.ToString();
        CodeAction action = CodeAction.Create(title,
            token => fixCode(context, diagnostic, token),
            title);
        context.RegisterCodeFix(action, diagnostic);
    }

    private static async Task<Document> AddPrivateSetAsync(
        CodeFixContext context,
        Diagnostic diagnostic,
        CancellationToken cancellationToken)
    {
        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(cancellationToken);

        if (root is null)
        {
            return context.Document;
        }

        PropertyDeclarationSyntax propertyDeclarationSyntax =
            FindSyntaxNodeOfType<PropertyDeclarationSyntax>(diagnostic, root);

        AccessorListSyntax? accessorList = propertyDeclarationSyntax.AccessorList;

        if (accessorList == null)
        {
            return context.Document;
        }

        AccessorDeclarationSyntax accessorDeclarationSyntax =
            SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

        if (propertyDeclarationSyntax.Modifiers.Any(m => m.ToString() == "public"))
        {
            accessorDeclarationSyntax =
                accessorDeclarationSyntax.AddModifiers(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
        }

        AccessorListSyntax newAccessorList = accessorList.AddAccessors(accessorDeclarationSyntax);

        SyntaxNode? newRoot = root.ReplaceNode(accessorList, newAccessorList);
        return context.Document.WithSyntaxRoot(newRoot);
    }

    private static async Task<Document> ChangeReturnTypeToVoid(
        CodeFixContext context,
        Diagnostic diagnostic,
        CancellationToken cancellationToken)
    {
        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(cancellationToken);

        if (root is null)
        {
            return context.Document;
        }

        MethodDeclarationSyntax methodDeclarationSyntax =
            FindSyntaxNodeOfType<MethodDeclarationSyntax>(diagnostic, root);

        TypeSyntax typeSyntax = methodDeclarationSyntax.ReturnType;
        TypeSyntax newTypeSyntax = SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.VoidKeyword));

        SyntaxNode? newRoot = root.ReplaceNode(typeSyntax, newTypeSyntax);
        return context.Document.WithSyntaxRoot(newRoot);
    }

    private static TSyntaxNodeType FindSyntaxNodeOfType<TSyntaxNodeType>(
        Diagnostic diagnostic,
        SyntaxNode root)
        where TSyntaxNodeType : SyntaxNode
    {
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        TSyntaxNodeType? node = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf()
            .OfType<TSyntaxNodeType>().FirstOrDefault();

        if (node == null)
        {
            throw new ArgumentException(
                $"Unable to find syntax node of type {typeof(TSyntaxNodeType)} " +
                $"for diagnostic {diagnostic.Descriptor.Id}.");
        }

        return node;
    }
}