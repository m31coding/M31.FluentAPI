using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ParameterSymbolInfo
{
    internal ParameterSymbolInfo(string parameterName, string typeForCodeGeneration, bool isNullable)
    {
        ParameterName = parameterName;
        TypeForCodeGeneration = typeForCodeGeneration;
        IsNullable = isNullable;
    }

    internal string ParameterName { get; }
    internal string TypeForCodeGeneration { get; }
    internal bool IsNullable { get; }

    protected bool Equals(ParameterSymbolInfo other)
    {
        return ParameterName == other.ParameterName &&
               TypeForCodeGeneration == other.TypeForCodeGeneration &&
               IsNullable == other.IsNullable;
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
        return new HashCode().Add(ParameterName, TypeForCodeGeneration, IsNullable);
    }
}