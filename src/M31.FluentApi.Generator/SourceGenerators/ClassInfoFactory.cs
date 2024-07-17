using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using M31.FluentApi.Generator.SourceGenerators.Generics;
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
            typeData.GenericInfo,
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
        GenericInfo? genericInfo,
        AttributeDataExtended attributeDataExtended,
        IReadOnlyCollection<string> usingStatements,
        bool isStruct,
        string newLineString,
        CancellationToken cancellationToken)
    {
        string className = type.Name;
        string? @namespace = type.ContainingNamespace.IsGlobalNamespace ? null : type.ContainingNamespace.ToString();
        bool isInternal = type.DeclaredAccessibility == Accessibility.Internal;
        ConstructorInfo? constructorInfo = TryGetConstructorInfo(type);
        FluentApiAttributeInfo fluentApiAttributeInfo =
            FluentApiAttributeInfo.Create(attributeDataExtended.AttributeData, className);

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
            genericInfo,
            isStruct,
            isInternal,
            constructorInfo!,
            fluentApiAttributeInfo.BuilderClassName,
            newLineString,
            infos,
            usingStatements,
            new FluentApiClassAdditionalInfo(groups));
    }

    private ConstructorInfo? TryGetConstructorInfo(INamedTypeSymbol type)
    {
        /* Look for the default constructor. If it is not present, take the constructor
           with the fewest parameters that is explicitly declared. */

#pragma warning disable RS1024
        IGrouping<int, IMethodSymbol>[] constructorsGroupedByNumberOfParameters =
            type.InstanceConstructors
                .Where(c => c.Parameters.Length == 0 || !c.IsImplicitlyDeclared)
                .GroupBy(c => c.Parameters.Length)
                .OrderBy(g => g.Key)
                .ToArray();
#pragma warning restore RS1024

        IGrouping<int, IMethodSymbol>? constructorsWithLeastAmountOfParameters =
            constructorsGroupedByNumberOfParameters.FirstOrDefault();

        if (constructorsWithLeastAmountOfParameters == null)
        {
            throw new GenerationException(
                $"The type {type.Name} has neither a default constructor nor explicitly declared constructors.");
        }

        IMethodSymbol[] constructors = constructorsWithLeastAmountOfParameters.ToArray();

        if (constructors.Length != 1)
        {
            int nofParameters = constructorsWithLeastAmountOfParameters.Key;

            foreach (IMethodSymbol constructor in constructors)
            {
                report.ReportDiagnostic(AmbiguousConstructors.CreateDiagnostic(constructor, nofParameters));
            }

            return null;
        }

        return new ConstructorInfo(
            constructors[0].Parameters.Length,
            constructors[0].DeclaredAccessibility != Accessibility.Public);
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

        FluentApiInfoCreator fluentApiInfoCreator = new FluentApiInfoCreator(report);
        return fluentApiInfoCreator.Create(symbol, attributeData);
    }
}