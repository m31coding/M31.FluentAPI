using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ParameterIdentity
{
    private ParameterIdentity(string? nonGenericParameterType, int? genericTypeParameterPosition, bool hasModifier)
    {
        NonGenericParameterType = nonGenericParameterType;
        GenericTypeParameterPosition = genericTypeParameterPosition;
        HasModifier = hasModifier;
    }

    internal static ParameterIdentity Create(
        string parameterType,
        int? genericTypeParameterPosition,
        ParameterKinds parameterKinds)
    {
        bool isGeneric = genericTypeParameterPosition.HasValue;
        string? nonGenericParameterType = isGeneric ? null : parameterType;

        bool hasModifier = parameterKinds.HasFlag(ParameterKinds.In) ||
                           parameterKinds.HasFlag(ParameterKinds.Out) ||
                           parameterKinds.HasFlag(ParameterKinds.Ref);

        return new ParameterIdentity(nonGenericParameterType, genericTypeParameterPosition, hasModifier);
    }

    internal string? NonGenericParameterType { get; }
    internal int? GenericTypeParameterPosition { get; }
    internal bool HasModifier { get; }

    protected bool Equals(ParameterIdentity other)
    {
        return NonGenericParameterType == other.NonGenericParameterType &&
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
        return new HashCode().Add(NonGenericParameterType, GenericTypeParameterPosition, HasModifier);
    }

    public override string ToString()
    {
        string typeString = GenericTypeParameterPosition.HasValue
            ? $"g{GenericTypeParameterPosition.Value}"
            : NonGenericParameterType!;

        return HasModifier ? $"mod-{typeString}" : typeString;
    }
}