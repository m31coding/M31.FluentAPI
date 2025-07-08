using System.Text.RegularExpressions;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators.DocumentationComments;

internal class Comments
{
    private static readonly Regex commentRegex = new Regex(@"<(?<tag>fluent\w+)(\s+(?<attrs>[^>]+))?>\s*(?<content>.*?)\s*</\k<tag>>", RegexOptions.Compiled | RegexOptions.Singleline);
    private static readonly Regex attributeRegex = new Regex(@"(?<key>\w+)\s*=\s*""(?<value>[^""]*)""", RegexOptions.Compiled | RegexOptions.Singleline);

    private Comments(IReadOnlyList<Comment> comments)
    {
        List = comments;
    }

    public IReadOnlyList<Comment> List { get; }

    internal static Comments Parse(string? commentXml)
    {
        if (commentXml == null)
        {
            return new Comments(Array.Empty<Comment>());
        }

        List<Comment> comments = new List<Comment>();
        MatchCollection matches = commentRegex.Matches(commentXml);

        foreach (Match match in matches)
        {
            string tag = match.Groups["tag"].Value;
            string attributes = match.Groups["attrs"].Value;
            string content = match.Groups["content"].Value;
            Comment comment = new Comment(tag, ParseCommentAttributes(attributes), content);
            comments.Add(comment);
        }

        return new Comments(comments);
    }

    private static IReadOnlyList<CommentAttribute> ParseCommentAttributes(string commentAttributes)
    {
        MatchCollection matches = attributeRegex.Matches(commentAttributes);
        return matches.Cast<Match>().Select(m => new CommentAttribute(m.Groups["key"].Value, m.Groups["value"].Value)).ToArray();
    }

    protected bool Equals(Comments other)
    {
        return List.SequenceEqual(other.List);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Comments)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().AddSequence(List);
    }
}
