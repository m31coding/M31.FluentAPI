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
            typeData,
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
        TypeData typeData,
        bool isStruct,
        string newLineString,
        CancellationToken cancellationToken)
    {
        string className = typeData.Type.Name;
        string? @namespace = typeData.Type.ContainingNamespace.IsGlobalNamespace
            ? null
            : typeData.Type.ContainingNamespace.ToString();
        bool isInternal = typeData.Type.DeclaredAccessibility == Accessibility.Internal;
        ConstructorInfo? constructorInfo = TryGetConstructorInfo(typeData.Type);
        FluentApiAttributeInfo fluentApiAttributeInfo =
            FluentApiAttributeInfo.Create(typeData.AttributeDataExtended.AttributeData, className);

        List<FluentApiInfo> infos = new List<FluentApiInfo>();
        (ITypeSymbol declaringType, ISymbol[] members)[] allMembers =
            GetMembersOfTypeAndBaseTypes(typeData.Type).ToArray();

        foreach ((ITypeSymbol declaringType, ISymbol[] members) in allMembers)
        {
            if (declaringType is not INamedTypeSymbol namedTypeSymbol)
            {
                throw new GenerationException($"The type {declaringType.Name} is not a named type symbol.");
            }

            GenericInfo? genericInfo = GenericInfo.TryCreate(namedTypeSymbol);
            string declaringClassNameWithGenericParameters =
                AugmentTypeNameWithGenericParameters(namedTypeSymbol.Name, genericInfo);

            foreach (ISymbol member in members)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                FluentApiInfo? fluentApiInfo = TryCreateFluentApiInfo(member, declaringClassNameWithGenericParameters);

                if (fluentApiInfo != null)
                {
                    infos.Add(fluentApiInfo);
                }
            }
        }

        IReadOnlyCollection<FluentApiInfoGroup> groups = FluentApiInfoGroupCreator.CreateGroups(infos, report);

        return new FluentApiClassInfo(
            className,
            @namespace,
            typeData.GenericInfo,
            isStruct,
            isInternal,
            constructorInfo!,
            fluentApiAttributeInfo.BuilderClassName,
            newLineString,
            infos,
            typeData.UsingStatements,
            new FluentApiClassAdditionalInfo(groups));
    }

    private static List<(ITypeSymbol declaringType, ISymbol[] members)> GetMembersOfTypeAndBaseTypes(
        ITypeSymbol typeSymbol)
    {
        List<(ITypeSymbol declaringType, ISymbol[] members)> result =
            new List<(ITypeSymbol declaringType, ISymbol[] members)>();

        GetMembers(typeSymbol);
        return result;

        void GetMembers(ITypeSymbol currentTypeSymbol)
        {
            ISymbol[] members = currentTypeSymbol.GetMembers()
                .Where(m => m.CanBeReferencedByName && m.Name != string.Empty).ToArray();

            result.Add((currentTypeSymbol, members));

            if (currentTypeSymbol.BaseType == null ||
                currentTypeSymbol.BaseType.SpecialType == SpecialType.System_Object)
            {
                return;
            }

            GetMembers(currentTypeSymbol.BaseType);
        }
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

        IGrouping<int, IMethodSymbol>? constructorsWithFewestParameters =
            constructorsGroupedByNumberOfParameters.FirstOrDefault();

        if (constructorsWithFewestParameters == null)
        {
            throw new GenerationException(
                $"The type {type.Name} has neither a default constructor nor explicitly declared constructors.");
        }

        IMethodSymbol[] constructors = constructorsWithFewestParameters.ToArray();

        if (constructors.Length != 1)
        {
            int nofParameters = constructorsWithFewestParameters.Key;

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

    private FluentApiInfo? TryCreateFluentApiInfo(ISymbol symbol, string declaringClassNameWithTypeParameters)
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
        return fluentApiInfoCreator.Create(symbol, attributeData, declaringClassNameWithTypeParameters);
    }

    public static string AugmentTypeNameWithGenericParameters(string typeName, GenericInfo? genericInfo)
    {
        string parameterListInAngleBrackets = genericInfo?.ParameterListInAngleBrackets ?? string.Empty;
        return $"{typeName}{parameterListInAngleBrackets}";
    }
}