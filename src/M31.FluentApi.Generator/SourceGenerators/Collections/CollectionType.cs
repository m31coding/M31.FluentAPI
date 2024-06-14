using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.Collections;

internal class CollectionType
{
    internal CollectionType(GeneratedCollection collection, string genericTypeArgument, ITypeSymbol? genericTypeSymbol)
    {
        Collection = collection;
        GenericTypeArgument = genericTypeArgument;
        GenericTypeSymbol = genericTypeSymbol;
    }

    internal GeneratedCollection Collection { get; }
    internal string GenericTypeArgument { get; }
    internal ITypeSymbol? GenericTypeSymbol { get; }

    protected bool Equals(CollectionType other)
    {
        // Exclude GenericTypeSymbol from the comparison, it is unstable additional information.
        return Collection == other.Collection && GenericTypeArgument == other.GenericTypeArgument;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CollectionType)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(Collection, GenericTypeArgument);
    }
}