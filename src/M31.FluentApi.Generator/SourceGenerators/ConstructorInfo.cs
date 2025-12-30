using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class ConstructorInfo
{
    internal ConstructorInfo(IReadOnlyCollection<ParameterSymbolInfo> parameterInfos, bool constructorIsNonPublic)
    {
        ParameterInfos = parameterInfos;
        ConstructorIsNonPublic = constructorIsNonPublic;
    }

    internal IReadOnlyCollection<ParameterSymbolInfo> ParameterInfos { get; }
    internal bool ConstructorIsNonPublic { get; }
    internal int NumberOfParameters => ParameterInfos.Count;

    protected bool Equals(ConstructorInfo other)
    {
        return ParameterInfos.SequenceEqual(other.ParameterInfos) &&
               ConstructorIsNonPublic == other.ConstructorIsNonPublic;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ConstructorInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .AddSequence(ParameterInfos)
            .Add(ConstructorIsNonPublic);
    }
}