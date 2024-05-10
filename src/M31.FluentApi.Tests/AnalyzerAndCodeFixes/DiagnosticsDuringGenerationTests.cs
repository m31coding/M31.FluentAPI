using System.Collections.Immutable;
using System.Linq;
using M31.FluentApi.Generator.SourceAnalyzers;
using M31.FluentApi.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;
using static M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.TestSourceCodeReader;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes;

public class DiagnosticsDuringGenerationTests
{
    [Fact]
    public void CanDetectDuplicateMethod1()
    {
        DuplicateMethodTest("DuplicateMethodClass1", "WithName", (11, 6), (14, 6));
    }

    [Fact]
    public void CanDetectDuplicateMethod2()
    {
        DuplicateMethodTest("DuplicateMethodClass2", "WithFriend", (12, 6), (15, 6));
    }

    [Fact]
    public void CanDetectDuplicateMethod3()
    {
        DuplicateMethodTest("DuplicateMethodClass3", "WithName", (14, 6), (20, 6));
    }

    [Fact]
    public void CanDetectDuplicateMethod4()
    {
        DuplicateMethodTest("DuplicateMethodClass4", "Method1", (11, 6), (16, 6), (21, 6));
    }

    [Fact]
    public void CanDetectDuplicateMethod5()
    {
        DuplicateMethodTest("DuplicateMethodClass5", "Method1", (11, 6), (16, 6));
    }

    private void DuplicateMethodTest(
        string testClassFolder,
        string duplicateMethodName,
        params (int line, int column)[] locations)
    {
        (string source, _) = ReadSource(testClassFolder, "Student");
        ImmutableArray<Diagnostic> diagnostics = ManualGenerator.RunGeneratorsAndGetDiagnostics(source);
        ExpectDuplicateMethodDiagnostic(diagnostics, duplicateMethodName, locations);
    }

    private void ExpectDuplicateMethodDiagnostic(
        ImmutableArray<Diagnostic> diagnostics,
        string duplicateMethodName,
        params (int line, int column)[] locations)
    {
        Assert.True(diagnostics.Any(Matches));

        bool Matches(Diagnostic diagnostic)
        {
            return diagnostic.Id == FluentApiDiagnostics.DuplicateFluentApiMethod.Descriptor.Id &&
                   diagnostic.Severity == DiagnosticSeverity.Error &&
                   diagnostic.GetMessage().Contains($"'{duplicateMethodName}'") &&
                   LocationsAreEqual(diagnostic);
        }

        bool LocationsAreEqual(Diagnostic diagnostic)
        {
            var expected = locations.OrderBy(l => l.line).ThenBy(l => l.column);
            var actual = diagnostic.AdditionalLocations.Concat(new Location[] { diagnostic.Location })
                .Select(GetLineAndColumn).OrderBy(l => l.line).ThenBy(l => l.column);

            return expected.SequenceEqual(actual);

            (int line, int column) GetLineAndColumn(Location location)
            {
                FileLinePositionSpan mapped = location.GetMappedLineSpan();
                return (mapped.StartLinePosition.Line + 1, mapped.StartLinePosition.Character + 1);
            }
        }
    }
}