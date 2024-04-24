using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class GenericInfo
{
    internal GenericInfo(IReadOnlyCollection<string> parameters)
    {
        Parameters = parameters;
    }

    internal IReadOnlyCollection<string> Parameters { get; }

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
        return new HashCode()
            .AddSequence(Parameters);
    }
}