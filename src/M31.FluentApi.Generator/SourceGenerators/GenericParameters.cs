using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class GenericParameters : IEquatable<GenericParameters>
{
    public GenericParameters(IReadOnlyCollection<string> parameters)
    {
        Parameters = parameters;
    }

    internal IReadOnlyCollection<string> Parameters { get; }

    public bool Equals(GenericParameters? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Parameters.SequenceEqual(other.Parameters);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GenericParameters)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .AddSequence(Parameters);
    }
}