using System.Text.RegularExpressions;

namespace M31.FluentApi.Generator.SourceGenerators.DocumentationComments;

internal class Comment
{
    private static readonly Regex attributeRegex = new Regex(@"(?<key>\w+)\s*=\s*""(?<value>[^""]*)""", RegexOptions.Compiled | RegexOptions.Singleline);

    internal string Tag { get; }
    internal IReadOnlyCollection<CommentAttribute> Attributes { get; }
    internal string Content { get; }

    internal static IReadOnlyCollection<CommentAttribute> ParseCommentAttributes(string commentAttributes)
    {
        MatchCollection matches = attributeRegex.Matches(commentAttributes);
        return matches.Cast<Match>().Select(m => new CommentAttribute(m.Groups["key"].Value, m.Groups["value"].Value)).ToArray();
    }
}
