namespace M31.FluentApi.Generator.SourceGenerators.DocumentationComments;

internal class CommentAttribute
{
    internal CommentAttribute(string key, string value)
    {
        Key = key;
        Value = value;
    }

    internal string Key { get; }
    internal string Value { get; }

    // todo: equality
}
