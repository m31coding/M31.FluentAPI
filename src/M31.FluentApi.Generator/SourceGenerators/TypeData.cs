using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class TypeData
{
    internal TypeData(INamedTypeSymbol type, AttributeDataExtended attributeData,
        IReadOnlyCollection<string> usingStatements)
    {
        Type = type;
        AttributeData = attributeData;
        UsingStatements = usingStatements;
    }

    internal INamedTypeSymbol Type { get; }
    internal AttributeDataExtended AttributeData { get; }
    internal IReadOnlyCollection<string> UsingStatements { get; }
}