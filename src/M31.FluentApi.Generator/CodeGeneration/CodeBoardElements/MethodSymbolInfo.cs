using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class MethodSymbolInfo : FluentApiSymbolInfo
{
    internal MethodSymbolInfo(
        string name,
        Accessibility accessibility,
        bool requiresReflection,
        IReadOnlyCollection<ParameterSymbolInfo> parameterInfos)
        : base(name, accessibility, requiresReflection)
    {
        ParameterInfos = parameterInfos;
    }

    internal IReadOnlyCollection<ParameterSymbolInfo> ParameterInfos { get; }

    protected bool Equals(MethodSymbolInfo other)
    {
        return base.Equals(other) && ParameterInfos.SequenceEqual(other.ParameterInfos);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MethodSymbolInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(base.GetHashCode()).AddSequence(ParameterInfos);
    }
}