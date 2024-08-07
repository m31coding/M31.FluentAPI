using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ParameterSymbolInfo
{
    internal ParameterSymbolInfo(
        string parameterName,
        string typeForCodeGeneration,
        bool isNullable,
        bool isReferenceType,
        string? defaultValue,
        int? genericTypeParameterPosition,
        ParameterKinds parameterKinds)
    {
        ParameterName = parameterName;
        TypeForCodeGeneration = typeForCodeGeneration;
        IsNullable = isNullable;
        IsReferenceType = isReferenceType;
        DefaultValue = defaultValue;
        GenericTypeParameterPosition = genericTypeParameterPosition;
        ParameterKinds = parameterKinds;
    }

    internal string ParameterName { get; }
    internal string TypeForCodeGeneration { get; }
    internal bool IsNullable { get; }
    internal bool IsReferenceType { get; }
    internal string? DefaultValue { get; }
    internal int? GenericTypeParameterPosition { get; }
    internal ParameterKinds ParameterKinds { get; }
    internal bool IsGenericParameter => GenericTypeParameterPosition.HasValue;

    protected bool Equals(ParameterSymbolInfo other)
    {
        return ParameterName == other.ParameterName &&
               TypeForCodeGeneration == other.TypeForCodeGeneration &&
               IsNullable == other.IsNullable &&
               IsReferenceType == other.IsReferenceType &&
               DefaultValue == other.DefaultValue &&
               ParameterKinds == other.ParameterKinds;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ParameterSymbolInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(ParameterName, TypeForCodeGeneration, IsNullable, IsReferenceType)
            .Add(DefaultValue, ParameterKinds);
    }
}