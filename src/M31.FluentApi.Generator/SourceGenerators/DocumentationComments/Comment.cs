using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators.DocumentationComments;
internal class Comment
{
    internal string Tag { get; }
    internal IReadOnlyList<CommentAttribute> Attributes { get; }
    internal string Content { get; }

    internal Comment(string tag, IReadOnlyList<CommentAttribute> attributes, string content)
    {
        Tag = tag;
        Attributes = attributes;
        Content = content;
    }

    protected bool Equals(Comment other)
    {
        return Tag == other.Tag &&
            Attributes.SequenceEqual(other.Attributes) &&
            Content == other.Content;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Comment)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(Tag)
            .AddSequence(Attributes)
            .Add(Content);
    }
}
