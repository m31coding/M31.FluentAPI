using System;
using System.Text.RegularExpressions;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;

internal static class SourceExtensions
{
    internal static string SelectSpan(this string source, string span)
    {
        MatchCollection matches = Regex.Matches(source, Regex.Escape(span));
        if (matches.Count != 1)
        {
            throw new InvalidOperationException(
                $"Span '{span}' not found or found multiple times in the source code.");
        }

        string replacement = $"[|{span}|]";
        return ReplaceMatch(source, matches[0], replacement);

        static string ReplaceMatch(string source, Match match, string replacement)
        {
            return source[..match.Index] +
                   replacement +
                   source[(match.Index + match.Length)..];
        }
    }
}