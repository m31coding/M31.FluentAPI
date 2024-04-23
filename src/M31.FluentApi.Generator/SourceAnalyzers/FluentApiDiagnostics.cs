// The parameter spans multiple lines
#pragma warning disable SA1118

using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace M31.FluentApi.Generator.SourceAnalyzers;

internal static class FluentApiDiagnostics
{
    internal static readonly DiagnosticDescriptor[] AllDescriptors = new DiagnosticDescriptor[]
    {
        MissingSetAccessor.Descriptor,
        UnsupportedFluentCollectionType.Descriptor,
        UnsupportedStaticMember.Descriptor,
        DuplicateAttribute.Descriptor,
        OrthogonalAttributeMisused.Descriptor,
        DuplicateMainAttribute.Descriptor,
        UnsupportedPartialType.Descriptor,
        InvalidFluentPredicateType.Descriptor,
        InvalidFluentNullableType.Descriptor,
        FluentNullableTypeWithoutNullableAnnotation.Descriptor,
        MissingDefaultConstructor.Descriptor,
        CodeGenerationException.Descriptor,
        GenericException.Descriptor,
        OrthogonalAttributeMisusedWithCompound.Descriptor,
        DuplicateFluentApiMethod.Descriptor,
        InvalidFluentMethodReturnType.Descriptor,
        CodeGenerationError.Descriptor,
        ConflictingControlAttributes.Descriptor,
        MissingBuilderStep.Descriptor,
    };

    internal static class MissingSetAccessor
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA001",
            title: "Fluent member property must have a set accessor",
            messageFormat: "The fluent member property '{0}' must have a set accessor",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(ISymbol symbol)
        {
            return Diagnostic.Create(Descriptor, symbol.Locations.FirstOrDefault(), symbol.Name);
        }
    }

    internal static class UnsupportedFluentCollectionType
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA002",
            title: "Fluent collection type is not supported",
            messageFormat: "Type '{0}' can not be used for a fluent collection. " +
                           "Change the type or consider implementing a custom method " +
                           "and use the FluentMethod attribute.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            helpLinkUri: "https://github.com/m31coding/M31.FluentAPI/tree/main/src/M31.FluentApi.Generator/" +
                         "SourceGenerators/Collections/CollectionInference.cs",
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(TypeSyntax actualType)
        {
            return Diagnostic.Create(Descriptor, actualType.GetLocation(), actualType.ToString());
        }
    }

    internal static class UnsupportedStaticMember
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA003",
            title: "Static members can not be used for the fluent API",
            messageFormat: "The static member '{0}' can not be used for the fluent API. " +
                           "Remove the static keyword.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(ISymbol symbol)
        {
            return Diagnostic.Create(Descriptor, symbol.Locations.FirstOrDefault(), symbol.Name);
        }
    }

    internal static class DuplicateAttribute
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA004",
            title: "Duplicate attribute",
            messageFormat: "The attribute '{0}' can only be applied once",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location, attributeData.ShortName);
        }
    }

    internal static class OrthogonalAttributeMisused
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA005",
            title: "Misused orthogonal attribute",
            messageFormat: "The attribute '{0}' can only be used in combination with FluentMember, FluentPredicate, " +
                           "or FluentCollection",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location, attributeData.ShortName);
        }
    }

    internal static class DuplicateMainAttribute
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA006",
            title: "Several main attributes",
            messageFormat: "Use only one of FluentMember, FluentPredicate, FluentCollection and FluentMethod",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location);
        }
    }

    internal static class UnsupportedPartialType
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA007",
            title: "Partial types are not supported",
            messageFormat: "Partial types are not supported. Remove partial keyword from type '{0}'.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(SyntaxToken partialKeyword, string typeName)
        {
            return Diagnostic.Create(Descriptor, partialKeyword.GetLocation(), typeName);
        }
    }

    internal static class InvalidFluentPredicateType
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA008",
            title: "Invalid fluent predicate type",
            messageFormat: "Type '{0}' can not be used for a fluent predicate. " +
                           "Change the type to 'bool'.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(TypeSyntax actualType)
        {
            return Diagnostic.Create(Descriptor, actualType.GetLocation(), actualType.ToString());
        }
    }

    internal static class InvalidFluentNullableType
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA009",
            title: "Invalid fluent nullable type",
            messageFormat: "Type '{0}' can not be used with fluent nullable. " +
                           "Change the type to a nullable type.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(TypeSyntax actualType)
        {
            return Diagnostic.Create(Descriptor, actualType.GetLocation(), actualType.ToString());
        }
    }

    internal static class FluentNullableTypeWithoutNullableAnnotation
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA010",
            title: "Fluent nullable type without nullable annotation",
            messageFormat: "Type '{0}' is not nullable. " +
                           "Consider appending the nullable annotation '?' to the type.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(TypeSyntax actualType)
        {
            return Diagnostic.Create(Descriptor, actualType.GetLocation(), actualType.ToString());
        }
    }

    internal static class MissingDefaultConstructor
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA011",
            title: "Default constructor is missing",
            messageFormat: "The fluent API requires a default constructor. " +
                           "Add a default constructor to type '{0}'.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(INamedTypeSymbol symbol)
        {
            return Diagnostic.Create(Descriptor, symbol.Locations[0], symbol.Name);
        }
    }

    /// <summary>
    /// Diagnostic used for <see cref="GenerationException"/>s.
    /// </summary>
    internal static class CodeGenerationException
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA012",
            title: "Code generation exception",
            messageFormat: "{0}",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(GenerationException exception)
        {
            return Diagnostic.Create(Descriptor, null, exception.Message);
        }
    }

    /// <summary>
    /// Diagnostic used for caught exceptions other than <see cref="GenerationException"/>s.
    /// </summary>
    internal static class GenericException
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA013",
            title: "Exception",
            messageFormat: "Code generation has thrown an exception of type {0} with message: {1}",
            category: "M31.Bug",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(Exception exception)
        {
            return Diagnostic.Create(Descriptor, null, exception.GetType().ToString(), exception.Message);
        }
    }

    internal static class OrthogonalAttributeMisusedWithCompound
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA014",
            title: "Orthogonal attributes can not be used in compounds",
            messageFormat: "The attribute '{0}' can not be used in a compound",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location, attributeData.ShortName);
        }
    }

    internal static class DuplicateFluentApiMethod
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA015",
            title: "Duplicate fluent API method",
            messageFormat: "A fluent API method with name '{0}' and the same parameters already exists",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(string duplicateMethodName, AttributeData[] attributeData)
        {
            Location[] locations = attributeData.Select(a => a.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None).ToArray();
            return Diagnostic.Create(Descriptor, locations.FirstOrDefault(), locations.Skip(1), duplicateMethodName);
        }
    }

    internal static class InvalidFluentMethodReturnType
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA016",
            title: "Invalid fluent method return type",
            messageFormat: "Type '{0}' can not be used as a fluent method return type. " +
                           "Change the return type to 'void'.",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(TypeSyntax actualType)
        {
            return Diagnostic.Create(Descriptor, actualType.GetLocation(), actualType.ToString());
        }
    }

    internal static class CodeGenerationError
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA017",
            title: "Code generation error",
            messageFormat: "{0}",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(string error)
        {
            return Diagnostic.Create(Descriptor, null, error);
        }
    }

    internal static class ConflictingControlAttributes
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA019",
            title: "Conflicting control attributes",
            messageFormat: "Control attributes are in conflict with each other",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location);
        }
    }

    internal static class MissingBuilderStep
    {
        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            id: "M31FA020",
            title: "Missing builder step",
            messageFormat: "Builder step {0} can not be found",
            category: "M31.Usage",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        internal static Diagnostic CreateDiagnostic(AttributeDataExtended attributeData, int missingBuilderStep)
        {
            Location location = attributeData.AttributeData.ApplicationSyntaxReference?
                .GetSyntax().GetLocation() ?? Location.None;
            return Diagnostic.Create(Descriptor, location, missingBuilderStep);
        }
    }
}