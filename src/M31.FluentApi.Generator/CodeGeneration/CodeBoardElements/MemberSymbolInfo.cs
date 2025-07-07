using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.Collections;
using M31.FluentApi.Generator.SourceGenerators.DocumentationComments;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class MemberSymbolInfo : FluentApiSymbolInfo
{
    internal MemberSymbolInfo(
        string name,
        string type,
        string declaringClassNameWithTypeParameters,
        Accessibility accessibility,
        bool requiresReflection,
        string typeForCodeGeneration,
        bool isNullable,
        bool isProperty,
        CollectionType? collectionType,
        Comments comments)
        : base(name, declaringClassNameWithTypeParameters, accessibility, requiresReflection, comments)
    {
        Type = type;
        TypeForCodeGeneration = typeForCodeGeneration;
        IsNullable = isNullable;
        IsProperty = isProperty;
        CollectionType = collectionType;
    }

    internal string Type { get; }
    internal string TypeForCodeGeneration { get; }
    internal bool IsNullable { get; }
    internal bool IsProperty { get; }
    internal CollectionType? CollectionType { get; }

    protected bool Equals(MemberSymbolInfo other)
    {
        return base.Equals(other) && Type == other.Type && TypeForCodeGeneration == other.TypeForCodeGeneration &&
               IsNullable == other.IsNullable && IsProperty == other.IsProperty &&
               Equals(CollectionType, other.CollectionType);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MemberSymbolInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(base.GetHashCode(), Type, TypeForCodeGeneration)
            .Add(IsNullable, IsProperty)
            .Add(CollectionType);
    }
}