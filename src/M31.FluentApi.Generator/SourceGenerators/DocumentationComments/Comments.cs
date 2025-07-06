using System.Text.RegularExpressions;

namespace M31.FluentApi.Generator.SourceGenerators.DocumentationComments;

internal class Comments
{
    private static readonly Regex commentRegex = new Regex(@"<(?<tag>fluent\w+)\s+(?<attrs>[^>]+)>\s*(?<content>.*?)\s*</\k<tag>>", RegexOptions.Compiled | RegexOptions.Singleline);
}
