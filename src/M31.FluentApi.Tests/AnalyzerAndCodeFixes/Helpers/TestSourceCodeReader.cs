using System.IO;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;

internal static class TestSourceCodeReader
{
    internal static SourceWithFix ReadSource(string testClassFolder, string @class)
    {
        string source = ReadTestClassCode(testClassFolder, $"{@class}.cs");
        string fixedSource = TryReadTestClassCode(testClassFolder, $"{@class}.fixed.txt") ?? source;
        return new SourceWithFix(source, fixedSource);
    }

    private static string ReadTestClassCode(string testClassFolder, string file)
    {
        return File.ReadAllText(GetFilePath(testClassFolder, file));
    }

    private static string? TryReadTestClassCode(string testClassFolder, string file)
    {
        string filePath = GetFilePath(testClassFolder, file);
        return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
    }

    private static string GetFilePath(string testClassFolder, string file)
    {
        return Path.Join("..", "..", "..", "AnalyzerAndCodeFixes", "TestClasses", testClassFolder, file);
    }
}