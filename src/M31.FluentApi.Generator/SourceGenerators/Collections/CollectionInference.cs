using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.CodeTypeExtractor;

namespace M31.FluentApi.Generator.SourceGenerators.Collections;

internal class CollectionInference
{
    private static readonly Dictionary<string, GeneratedCollection> genericTypeToCollection =
        new Dictionary<string, GeneratedCollection>()
        {
            { "System.Collections.Generic.IEnumerable<T>", GeneratedCollection.Array },
            { "System.Collections.Generic.IReadOnlyList<T>", GeneratedCollection.Array },
            { "System.Collections.Generic.IReadOnlyCollection<T>", GeneratedCollection.Array },
            { "System.Collections.Generic.List<T>", GeneratedCollection.List },
            { "System.Collections.Generic.IList<T>", GeneratedCollection.List },
            { "System.Collections.Generic.ICollection<T>", GeneratedCollection.List },
            { "System.Collections.Generic.HashSet<T>", GeneratedCollection.HashSet },
            { "System.Collections.Generic.ISet<T>", GeneratedCollection.HashSet },
            { "System.Collections.Generic.IReadOnlySet<T>", GeneratedCollection.HashSet },
        };

    private static readonly Dictionary<string, GeneratedCollection> nonGenericTypeToCollection =
        new Dictionary<string, GeneratedCollection>()
        {
            { "System.Array", GeneratedCollection.Array },
            { "System.Collections.IEnumerable", GeneratedCollection.Array },
            { "System.Collections.IList", GeneratedCollection.List },
            { "System.Collections.ICollection", GeneratedCollection.List },
        };

    internal static CollectionType? InferCollectionType(ITypeSymbol typeSymbol)
    {
        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            return new CollectionType(
                GeneratedCollection.Array,
                GetTypeForCodeGeneration(arrayTypeSymbol.ElementType),
                arrayTypeSymbol.ElementType);
        }

        if (typeSymbol is INamedTypeSymbol { IsGenericType: true, TypeArguments.Length: 1 } genericNamedTypeSymbol)
        {
            string baseType = genericNamedTypeSymbol.ConstructedFrom.ToString();
            ITypeSymbol genericArgument = genericNamedTypeSymbol.TypeArguments[0];
            string genericTypeArgument = GetTypeForCodeGeneration(genericArgument);

            if (!genericTypeToCollection.TryGetValue(baseType, out GeneratedCollection collection))
            {
                return null;
            }

            return new CollectionType(collection, genericTypeArgument, genericArgument);
        }

        if (typeSymbol is INamedTypeSymbol { IsGenericType: false } nonGenericNamedTypeSymbol)
        {
            if (!nonGenericTypeToCollection.TryGetValue(
                    nonGenericNamedTypeSymbol.ToString(),
                    out GeneratedCollection collection))
            {
                return null;
            }

            return new CollectionType(collection, "object", null);
        }

        return null;
    }
}