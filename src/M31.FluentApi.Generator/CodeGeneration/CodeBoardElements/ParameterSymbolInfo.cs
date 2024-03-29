using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ParameterSymbolInfo
{
    internal ParameterSymbolInfo(
        string parameterName,
        string typeForCodeGeneration,
        bool isNullable,
        string? defaultValue)
    {
        ParameterName = parameterName;
        TypeForCodeGeneration = typeForCodeGeneration;
        IsNullable = isNullable;
        DefaultValue = defaultValue;
    }

    internal string ParameterName { get; }
    internal string TypeForCodeGeneration { get; }
    internal bool IsNullable { get; }
    internal string? DefaultValue { get; }

    protected bool Equals(ParameterSymbolInfo other)
    {
        return ParameterName == other.ParameterName &&
               TypeForCodeGeneration == other.TypeForCodeGeneration &&
               IsNullable == other.IsNullable &&
               DefaultValue == other.DefaultValue;
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
        return new HashCode().Add(ParameterName, TypeForCodeGeneration, IsNullable, DefaultValue);
    }
}