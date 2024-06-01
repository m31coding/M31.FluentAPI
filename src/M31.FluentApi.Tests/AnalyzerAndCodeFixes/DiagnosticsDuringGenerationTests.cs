using System.Linq;
using M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;
using M31.FluentApi.Tests.Helpers;
using Microsoft.CodeAnalysis;
using Xunit;
using static M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.TestSourceCodeReader;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes;

public class DiagnosticsDuringGenerationTests
{
    [Fact]
    public void CanDetectDuplicateMethod1()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "WithName",
            (11, 6), (14, 6));

        RunGeneratorAndCheckDiagnostics("DuplicateMethodClass1", "Student", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectDuplicateMethod2()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "WithFriend",
            (12, 6), (15, 6));

        RunGeneratorAndCheckDiagnostics("DuplicateMethodClass2", "Student", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectDuplicateMethod3()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "WithName",
            (14, 6), (20, 6));

        RunGeneratorAndCheckDiagnostics("DuplicateMethodClass3", "Student", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectDuplicateMethod4()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "Method1",
            (12, 6), (17, 6), (22, 6));

        RunGeneratorAndCheckDiagnostics("DuplicateMethodClass4", "Student", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectDuplicateMethod5()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "Method1",
            (12, 6), (17, 6));

        RunGeneratorAndCheckDiagnostics("DuplicateMethodClass5", "Student", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectDuplicateMethodPartialClass()
    {
        ExpectedDiagnostic expectedDiagnostic = new ExpectedDiagnostic(
            DuplicateFluentApiMethod.Descriptor,
            "WithName",
            (10, 6), (11, 6));
        RunGeneratorAndCheckDiagnostics("DuplicateMethodPartialClass", "Student1|Student2", expectedDiagnostic);
    }

    [Fact]
    public void CanDetectReservedMethod1()
    {
        ExpectedDiagnostic expectedDiagnostic1 = new ExpectedDiagnostic(
            ReservedMethodName.Descriptor,
            "InitialStep",
            (11, 6));

        ExpectedDiagnostic expectedDiagnostic2 = new ExpectedDiagnostic(
            ReservedMethodName.Descriptor,
            "InitialStep",
            (17, 6));

        RunGeneratorAndCheckDiagnostics("ReservedMethodClass1", "Student", expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public void CanDetectReservedMethod2()
    {
        ExpectedDiagnostic expectedDiagnostic1 = new ExpectedDiagnostic(
            ReservedMethodName.Descriptor,
            "InitialStep",
            (12, 6));

        RunGeneratorAndCheckDiagnostics("ReservedMethodClass2", "Student", expectedDiagnostic1);
    }

    private void RunGeneratorAndCheckDiagnostics(
        string testClassFolder,
        string classes,
        params ExpectedDiagnostic[] expectedDiagnostics)
    {
        string[] splitClasses = classes.Split('|');
        SourceWithFix[] sourceCodeWithFixes = splitClasses.Select(c => ReadSource(testClassFolder, c)).ToArray();
        string[] sourceCode = sourceCodeWithFixes.Select(c => c.Source).ToArray();
        Diagnostic[] diagnostics = ManualGenerator.RunGeneratorsAndGetDiagnostics(sourceCode).ToArray();

        Assert.Equal(expectedDiagnostics.Length, diagnostics.Length);

        expectedDiagnostics = expectedDiagnostics
            .OrderBy(d => d.Locations.MinBy(l => l.line)).ToArray();

        diagnostics = diagnostics
            .OrderBy(d => GetLineAndColumn(d.Location).line).ToArray();

        for (int i = 0; i < expectedDiagnostics.Length; i++)
        {
            Assert.True(Matches(expectedDiagnostics[i], diagnostics[i]));
        }

        bool Matches(ExpectedDiagnostic expectedDiagnostic, Diagnostic diagnostic)
        {
            return diagnostic.Descriptor.Equals(expectedDiagnostic.Descriptor) &&
                   MessageArgumentsMatch(expectedDiagnostic, diagnostic) &&
                   LocationsAreEqual(expectedDiagnostic, diagnostic);
        }

        static bool MessageArgumentsMatch(ExpectedDiagnostic expectedDiagnostic, Diagnostic diagnostic)
        {
            if (expectedDiagnostic.MessageArgument == null)
            {
                return true;
            }

            return diagnostic.GetMessage().Contains($"{expectedDiagnostic.MessageArgument}");
        }

        static bool LocationsAreEqual(ExpectedDiagnostic expectedDiagnostic, Diagnostic diagnostic)
        {
            (int line, int column)[] expected =
                expectedDiagnostic.Locations.OrderBy(l => l.line).ThenBy(l => l.column).ToArray();

            (int line, int column)[] actual = new Location[] { diagnostic.Location }
                .Concat(diagnostic.AdditionalLocations)
                .Select(GetLineAndColumn).OrderBy(l => l.line).ThenBy(l => l.column).ToArray();

            return expected.SequenceEqual(actual);
        }

        static (int line, int column) GetLineAndColumn(Location location)
        {
            FileLinePositionSpan mapped = location.GetMappedLineSpan();
            return (mapped.StartLinePosition.Line + 1, mapped.StartLinePosition.Character + 1);
        }
    }

    private class ExpectedDiagnostic
    {
        public ExpectedDiagnostic(
            DiagnosticDescriptor descriptor,
            string? messageArgument,
            params (int line, int column)[] locations)
        {
            Descriptor = descriptor;
            MessageArgument = messageArgument;
            Locations = locations;
        }

        public DiagnosticDescriptor Descriptor { get; }
        public string? MessageArgument { get; }
        public (int line, int column)[] Locations { get; }
    }
}