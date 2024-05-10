using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class TypeData
{
    internal TypeData(
        INamedTypeSymbol type,
        GenericInfo? genericInfo,
        AttributeDataExtended attributeData,
        IReadOnlyCollection<string> usingStatements)
    {
        Type = type;
        GenericInfo = genericInfo;
        AttributeData = attributeData;
        UsingStatements = usingStatements;
    }

    internal INamedTypeSymbol Type { get; }
    internal GenericInfo? GenericInfo { get; }
    internal AttributeDataExtended AttributeData { get; }
    internal IReadOnlyCollection<string> UsingStatements { get; }
}