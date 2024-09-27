using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class TypeData
{
    internal TypeData(
        INamedTypeSymbol type,
        GenericInfo? genericInfo,
        AttributeDataExtended attributeDataExtended,
        IReadOnlyCollection<string> usingStatements)
    {
        Type = type;
        GenericInfo = genericInfo;
        AttributeDataExtended = attributeDataExtended;
        UsingStatements = usingStatements;
    }

    internal INamedTypeSymbol Type { get; }
    internal GenericInfo? GenericInfo { get; }
    internal AttributeDataExtended AttributeDataExtended { get; }
    internal IReadOnlyCollection<string> UsingStatements { get; }
}