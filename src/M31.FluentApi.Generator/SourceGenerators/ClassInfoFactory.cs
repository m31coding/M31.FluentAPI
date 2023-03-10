using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class ClassInfoFactory
{
    private readonly Dictionary<FluentApiInfo, FluentApiAdditionalInfo> additionalInfo;
    private readonly ClassInfoReport report;

    private ClassInfoFactory()
    {
        additionalInfo = new Dictionary<FluentApiInfo, FluentApiAdditionalInfo>();
        report = new ClassInfoReport();
    }

    internal static ClassInfoResult CreateFluentApiClassInfo(
        SemanticModel semanticModel, TypeDeclarationSyntax typeDeclaration, CancellationToken cancellationToken)
    {
        return new ClassInfoFactory().CreateFluentApiClassInfoInternal(semanticModel, typeDeclaration,
            cancellationToken);
    }

    private ClassInfoResult CreateFluentApiClassInfoInternal(
        SemanticModel semanticModel, TypeDeclarationSyntax typeDeclaration, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new ClassInfoResult(report);
        }

        TypeDataCreator typeDataCreator = new TypeDataCreator(report);
        TypeData? typeData = typeDataCreator.GetTypeData(semanticModel, typeDeclaration);

        if (typeData == null || cancellationToken.IsCancellationRequested)
        {
            return new ClassInfoResult(report);
        }

        SyntaxKind syntaxKind = typeDeclaration.Kind();
        bool isStruct = syntaxKind is SyntaxKind.StructDeclaration or SyntaxKind.RecordStructDeclaration;

        FluentApiClassInfo? classInfo = CreateFluentApiClassInfo(
            typeData.Type, typeData.AttributeData, typeData.UsingStatements, isStruct, cancellationToken);

        if (classInfo != null)
        {
            ClassInfoAnalyzer analyzer = new ClassInfoAnalyzer(semanticModel, report);
            analyzer.Analyze(classInfo, cancellationToken);
        }

        return new ClassInfoResult(classInfo, report);
    }

    private FluentApiClassInfo? CreateFluentApiClassInfo(
        INamedTypeSymbol type,
        AttributeDataExtended attributeDataExtended,
        IReadOnlyCollection<string> usingStatements,
        bool isStruct,
        CancellationToken cancellationToken)
    {
        string className = type.Name;
        string? @namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToString();
        bool isInternal = type.DeclaredAccessibility == Accessibility.Internal;
        bool hasPrivateConstructor = HasPrivateConstructor(type);
        string builderClassNameTemplate = attributeDataExtended.AttributeData.GetConstructorArguments<string>();
        string builderClassName = NameCreator.CreateName(builderClassNameTemplate, className);

        List<FluentApiInfo> infos = new List<FluentApiInfo>();

        foreach (var member in type.GetMembers().Where(m => m.CanBeReferencedByName && m.Name != string.Empty))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            FluentApiInfo? fluentApiInfo = TryCreateFluentApiInfo(member);

            if (fluentApiInfo != null)
            {
                infos.Add(fluentApiInfo);
            }
        }

        return new FluentApiClassInfo(
            className,
            @namespace,
            isStruct,
            isInternal,
            hasPrivateConstructor,
            builderClassName,
            infos,
            usingStatements,
            new FluentApiClassAdditionalInfo(additionalInfo));
    }

    private bool HasPrivateConstructor(INamedTypeSymbol type)
    {
        IMethodSymbol[] defaultInstanceConstructors =
            type.InstanceConstructors.Where(c => c.Parameters.Length == 0).ToArray();

        if (defaultInstanceConstructors.Length == 0)
        {
            return false;
        }

        return !defaultInstanceConstructors.Any(c => c.DeclaredAccessibility == Accessibility.Public);
    }

    private FluentApiInfo? TryCreateFluentApiInfo(ISymbol symbol)
    {
        AttributeDataExtractor extractor = new AttributeDataExtractor(report);
        FluentApiAttributeData? attributeData = extractor.GetAttributeData(symbol);

        if (attributeData == null)
        {
            return null;
        }

        if (symbol.IsStatic)
        {
            report.ReportDiagnostic(UnsupportedStaticMember.CreateDiagnostic(symbol));
            return null;
        }

        FluentApiInfo result = FluentApiInfo.Create(
            symbol,
            attributeData,
            out FluentApiAdditionalInfo fluentApiAdditionalInfo);

        additionalInfo.Add(result, fluentApiAdditionalInfo);
        return result;
    }
}