using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal abstract class FluentApiSymbolInfo
{
    internal FluentApiSymbolInfo(
        string name,
        string declaringClassNameWithTypeParameters,
        Accessibility accessibility,
        Comments comments)
    {
        Name = name;
        NameInCamelCase = Name.TrimStart('_').FirstCharToLower();
        NameInPascalCase = Name.TrimStart('_').FirstCharToUpper();
        DeclaringClassNameWithTypeParameters = declaringClassNameWithTypeParameters;
        Accessibility = accessibility;
        Comments = comments;
    }

    internal string Name { get; }
    internal string NameInCamelCase { get; }
    internal string NameInPascalCase { get; }
    internal string DeclaringClassNameWithTypeParameters { get; }
    internal Accessibility Accessibility { get; }
    internal Comments Comments { get; }

    protected bool Equals(FluentApiSymbolInfo other)
    {
        return Name == other.Name &&
               DeclaringClassNameWithTypeParameters == other.DeclaringClassNameWithTypeParameters &&
               Accessibility == other.Accessibility &&
               Comments.Equals(other.Comments);
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
        return new HashCode()
            .Add(Name, DeclaringClassNameWithTypeParameters, Accessibility, Comments);
    }
}