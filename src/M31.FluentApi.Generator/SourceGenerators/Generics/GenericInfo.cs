using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.Generics;

internal class GenericInfo
{
    private GenericInfo(IReadOnlyCollection<GenericTypeParameter> parameters)
    {
        Parameters = parameters;
    }

    internal static GenericInfo Create(IEnumerable<ITypeParameterSymbol> typeParameters)
    {
        GenericTypeParameter[] parameters = typeParameters.Select(GenericTypeParameter.Create).ToArray();
        return new GenericInfo(parameters);
    }

    internal IReadOnlyCollection<GenericTypeParameter> Parameters { get; }

    internal IEnumerable<string> ParameterStrings => Parameters.Select(p => p.ParameterString);

    protected bool Equals(GenericInfo other)
    {
        return Parameters.SequenceEqual(other.Parameters);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GenericInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().AddSequence(Parameters);
    }
}