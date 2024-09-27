using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal abstract class FluentApiSymbolInfo
{
    internal FluentApiSymbolInfo(
        string name,
        string declaringClassNameWithTypeParameters,
        Accessibility accessibility,
        bool requiresReflection)
    {
        Name = name;
        NameInCamelCase = Name.TrimStart('_').FirstCharToLower();
        DeclaringClassNameWithTypeParameters = declaringClassNameWithTypeParameters;
        Accessibility = accessibility;
        RequiresReflection = requiresReflection;
    }

    internal string Name { get; }
    internal string NameInCamelCase { get; }
    internal string DeclaringClassNameWithTypeParameters { get; }
    internal Accessibility Accessibility { get; }
    internal bool RequiresReflection { get; }

    protected bool Equals(FluentApiSymbolInfo other)
    {
        return Name == other.Name &&
               DeclaringClassNameWithTypeParameters == other.DeclaringClassNameWithTypeParameters &&
               Accessibility == other.Accessibility &&
               RequiresReflection == other.RequiresReflection;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FluentApiSymbolInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(Name, DeclaringClassNameWithTypeParameters, Accessibility, RequiresReflection);
    }
}