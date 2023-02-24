using System.IO;
using System.Threading;
using System.Threading.Tasks;
using M31.FluentApi.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;

internal static class AnalyzerAndCodeFixVerifier<TAnalyzer, TCodeFix>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TCodeFix : CodeFixProvider, new()
{
    internal static DiagnosticResult Diagnostic(string diagnosticId)
    {
        return CSharpCodeFixVerifier<TAnalyzer, TCodeFix, XUnitVerifier>
            .Diagnostic(diagnosticId);
    }

    internal static async Task VerifyCodeFixAsync(
        string source,
        string? fixedSource,
        params DiagnosticResult[] expected)
    {
        var test = new CodeFixTest(source, fixedSource, expected);
        await test.RunAsync(CancellationToken.None);
    }

    private class CodeFixTest : CSharpCodeFixTest<TAnalyzer, TCodeFix, XUnitVerifier>
    {
        internal CodeFixTest(
            string source,
            string? fixedSource,
            params DiagnosticResult[] expected)
        {
            TestCode = source;

            if (fixedSource != null)
            {
                FixedCode = fixedSource;
            }

            ExpectedDiagnostics.AddRange(expected);

#if NET6_0
            ReferenceAssemblies = new ReferenceAssemblies(
                "net6.0",
                new PackageIdentity("Microsoft.NETCore.App.Ref", "6.0.0"),
                Path.Combine("ref", "net6.0"));
#else
            ReferenceAssemblies = ReferenceAssemblies.Net.Net50;
#endif

            TestState.AdditionalReferences.Add(typeof(FluentApiAttribute).Assembly);
        }

        protected override CompilationOptions CreateCompilationOptions()
        {
            return new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                allowUnsafe: true,
                nullableContextOptions: NullableContextOptions.Enable);
        }
    }
}