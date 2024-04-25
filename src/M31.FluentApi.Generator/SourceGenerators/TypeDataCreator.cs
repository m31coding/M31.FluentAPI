using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class TypeDataCreator
{
    private readonly ClassInfoReport report;

    internal TypeDataCreator(ClassInfoReport report)
    {
        this.report = report;
    }

    internal TypeData? GetTypeData(SemanticModel semanticModel, TypeDeclarationSyntax typeDeclaration)
    {
        INamedTypeSymbol? type = semanticModel.GetDeclaredSymbol(typeDeclaration);

        if (type == null)
        {
            report.ReportError($"Unable to get the type symbol for the class declaration syntax " +
                               $"{typeDeclaration.ToString()}.");
            return null;
        }

        GenericInfo? genericInfo = GetGenericInfo(type);

        AttributeDataExtended[] attributeData = type.GetAttributes().Select(AttributeDataExtended.Create)
            .OfType<AttributeDataExtended>().Where(a => a.FullName == Attributes.FullNames.FluentApiAttribute)
            .ToArray();

        if (attributeData.Length == 0)
        {
            report.ReportError($"Type symbol {type.Name} has no fluent API attribute.");
            return null;
        }

        if (attributeData.Length > 1)
        {
            foreach (AttributeDataExtended attributeDataExtended in attributeData.Skip(1))
            {
                report.ReportDiagnostic(DuplicateAttribute.CreateDiagnostic(attributeDataExtended));
            }
        }

        IReadOnlyCollection<string>? usingStatements = GetUsingStatements(typeDeclaration);

        if (usingStatements == null)
        {
            return null;
        }

        return new TypeData(type, genericInfo, attributeData[0], usingStatements);
    }

    private IReadOnlyCollection<string>? GetUsingStatements(SyntaxNode syntaxNode)
    {
        if (syntaxNode is CompilationUnitSyntax compilationUnitSyntax)
        {
            return compilationUnitSyntax.Usings.Select(u => u.ToString()).ToArray();
        }

        if (syntaxNode.Parent == null)
        {
            report.ReportError("Compilation unit syntax not found.");
            return null;
        }

        return GetUsingStatements(syntaxNode.Parent);
    }

    private GenericInfo? GetGenericInfo(INamedTypeSymbol type)
    {
        if (!type.IsGenericType)
        {
            return null;
        }

        GenericTypeParameter[] parameters = type.TypeParameters.Select(GenericTypeParameter.Create).ToArray();
        return new GenericInfo(parameters);
    }
}