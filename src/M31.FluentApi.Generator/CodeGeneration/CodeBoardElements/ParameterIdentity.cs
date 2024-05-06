using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ParameterIdentity
{
    private ParameterIdentity(string parameterType, int? genericTypeParameterPosition, bool hasModifier)
    {
        ParameterType = parameterType;
        GenericTypeParameterPosition = genericTypeParameterPosition;
        HasModifier = hasModifier;
    }

    internal static ParameterIdentity Create(
        string parameterType,
        int? genericTypeParameterPosition,
        ParameterKinds parameterKinds)
    {
        bool hasModifier = parameterKinds.HasFlag(ParameterKinds.In) ||
                           parameterKinds.HasFlag(ParameterKinds.Out) ||
                           parameterKinds.HasFlag(ParameterKinds.Ref);

        return new ParameterIdentity(parameterType, genericTypeParameterPosition, hasModifier);
    }

    internal string ParameterType { get; }
    internal int? GenericTypeParameterPosition { get; }
    internal bool HasModifier { get; }

    protected bool Equals(ParameterIdentity other)
    {
        return ParameterType == other.ParameterType &&
               GenericTypeParameterPosition == other.GenericTypeParameterPosition &&
               HasModifier == other.HasModifier;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ParameterIdentity)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(ParameterType, GenericTypeParameterPosition, HasModifier);
    }

    public override string ToString()
    {
        string typeString = GenericTypeParameterPosition.HasValue
            ? $"g{GenericTypeParameterPosition.Value}"
            : ParameterType;

        return HasModifier ? $"mod-{typeString}" : typeString;
    }
}