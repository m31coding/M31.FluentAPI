using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.DocumentationComments;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class MethodSymbolInfo : FluentApiSymbolInfo
{
    internal MethodSymbolInfo(
        string name,
        string declaringClassNameWithTypeParameters,
        Accessibility accessibility,
        bool requiresReflection,
        GenericInfo? genericInfo,
        IReadOnlyCollection<ParameterSymbolInfo> parameterInfos,
        string returnType,
        Comments comments)
        : base(name, declaringClassNameWithTypeParameters, accessibility, requiresReflection, comments)
    {
        GenericInfo = genericInfo;
        ParameterInfos = parameterInfos;
        ReturnType = returnType;
    }

    internal GenericInfo? GenericInfo { get; }
    internal IReadOnlyCollection<ParameterSymbolInfo> ParameterInfos { get; }
    internal string ReturnType { get; }

    protected bool Equals(MethodSymbolInfo other)
    {
        return base.Equals(other) &&
               Equals(GenericInfo, other.GenericInfo) &&
               ParameterInfos.SequenceEqual(other.ParameterInfos) &&
               ReturnType == other.ReturnType;
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
        return new HashCode()
            .Add(base.GetHashCode())
            .Add(GenericInfo)
            .AddSequence(ParameterInfos)
            .Add(ReturnType);
    }
}