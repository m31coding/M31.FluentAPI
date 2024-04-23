using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

/// <summary>
/// Creates and analyzes a <see cref="FluentApiClassInfo"/> instance.
/// </summary>
internal class ClassInfoFactory
{
    private readonly ClassInfoReport report;

    private ClassInfoFactory()
    {
        report = new ClassInfoReport();
    }

    internal static ClassInfoResult CreateFluentApiClassInfo(
        SemanticModel semanticModel,
        TypeDeclarationSyntax typeDeclaration,
        SourceGeneratorConfig generatorConfig,
        CancellationToken cancellationToken)
    {
        return new ClassInfoFactory().CreateFluentApiClassInfoInternal(
            semanticModel,
            typeDeclaration,
            generatorConfig,
            cancellationToken);
    }

    private ClassInfoResult CreateFluentApiClassInfoInternal(
        SemanticModel semanticModel,
        TypeDeclarationSyntax typeDeclaration,
        SourceGeneratorConfig generatorConfig,
        CancellationToken cancellationToken)
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
            typeData.Type,
            typeData.GenericsInfo,
            typeData.AttributeData,
            typeData.UsingStatements,
            isStruct,
            generatorConfig.NewLineString,
            cancellationToken);

        if (classInfo != null)
        {
            ClassInfoAnalyzer analyzer = new ClassInfoAnalyzer(semanticModel, report);
            analyzer.Analyze(classInfo, cancellationToken);
        }

        return new ClassInfoResult(classInfo, report);
    }

    private FluentApiClassInfo? CreateFluentApiClassInfo(
        INamedTypeSymbol type,
        GenericsInfo? genericsInfo,
        AttributeDataExtended attributeDataExtended,
        IReadOnlyCollection<string> usingStatements,
        bool isStruct,
        string newLineString,
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

        IReadOnlyCollection<FluentApiInfoGroup> groups = FluentApiInfoGroupCreator.CreateGroups(infos, report);

        return new FluentApiClassInfo(
            className,
            @namespace,
            genericsInfo,
            isStruct,
            isInternal,
            hasPrivateConstructor,
            builderClassName,
            newLineString,
            infos,
            usingStatements,
            new FluentApiClassAdditionalInfo(groups));
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

        return FluentApiInfo.Create(symbol, attributeData);
    }
}