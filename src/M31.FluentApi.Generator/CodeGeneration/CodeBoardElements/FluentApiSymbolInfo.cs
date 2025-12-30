using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using M31.FluentApi.Generator.Commons;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal abstract class FluentApiSymbolInfo
{
    internal FluentApiSymbolInfo(
        string name,
        string declaringClassName,
        string declaringClassNameWithTypeParameters,
        Accessibility accessibility,
        bool publiclyWritable,
        Comments comments)
    {
        Name = name;
        NameInCamelCase = Name.TrimStart('_').FirstCharToLower();
        NameInPascalCase = Name.TrimStart('_').FirstCharToUpper();
        DeclaringClassName = declaringClassName;
        DeclaringClassNameWithTypeParameters = declaringClassNameWithTypeParameters;
        Accessibility = accessibility;
        PubliclyWritable = publiclyWritable;
        Comments = comments;
    }

    internal string Name { get; }
    internal string NameInCamelCase { get; }
    internal string NameInPascalCase { get; }
    internal string DeclaringClassName { get; }
    internal string DeclaringClassNameWithTypeParameters { get; }
    internal Accessibility Accessibility { get; }
    internal bool PubliclyWritable { get; }
    internal Comments Comments { get; }

    protected bool Equals(FluentApiSymbolInfo other)
    {
        return Name == other.Name &&
               DeclaringClassName == other.DeclaringClassName &&
               DeclaringClassNameWithTypeParameters == other.DeclaringClassNameWithTypeParameters &&
               Accessibility == other.Accessibility &&
               PubliclyWritable == other.PubliclyWritable &&
               Comments.Equals(other.Comments);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FluentApiSymbolInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(Name)
            .Add(DeclaringClassName, DeclaringClassNameWithTypeParameters)
            .Add(Accessibility, PubliclyWritable, Comments);
    }
}