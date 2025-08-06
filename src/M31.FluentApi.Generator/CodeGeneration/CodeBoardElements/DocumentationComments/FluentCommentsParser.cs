using System.Text.RegularExpressions;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

internal static class FluentCommentsParser
{
    private static readonly Regex commentRegex =
        new Regex(@"<(?<tag>fluent\w+)(\s+(?<attrs>[^>]+))?>\s*(?<content>.*?)\s*</\k<tag>>",
            RegexOptions.Compiled | RegexOptions.Singleline);

    private static readonly Regex attributeRegex = new Regex(@"(?<key>\w+)\s*=\s*""(?<value>[^""]*)""",
        RegexOptions.Compiled | RegexOptions.Singleline);

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
        return matches.Cast<Match>().Select(m => new CommentAttribute(m.Groups["key"].Value, m.Groups["value"].Value))
            .ToArray();
    }
}