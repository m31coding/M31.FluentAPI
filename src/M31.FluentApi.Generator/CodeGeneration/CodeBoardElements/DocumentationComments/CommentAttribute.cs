using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;
internal class CommentAttribute
{
    internal CommentAttribute(string key, string value)
    {
        Key = key;
        Value = value;
    }

    internal string Key { get; }
    internal string Value { get; }

    protected bool Equals(CommentAttribute other)
    {
        return Key == other.Key &&
            Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CommentAttribute)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().Add(Key, Value);
    }

    public override string ToString()
    {
        return @$"{Key}=""{Value}""";
    }
}
