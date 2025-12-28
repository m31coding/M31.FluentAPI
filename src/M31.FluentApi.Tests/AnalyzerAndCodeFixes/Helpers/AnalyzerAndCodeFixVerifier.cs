using System;
using System.Collections.Generic;
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

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;

internal static class AnalyzerAndCodeFixVerifier<TAnalyzer, TCodeFix>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TCodeFix : CodeFixProvider, new()
{
    internal static DiagnosticResult Diagnostic(string diagnosticId)
    {
        return CSharpCodeFixVerifier<TAnalyzer, TCodeFix, DefaultVerifier>
            .Diagnostic(diagnosticId);
    }

    internal static async Task VerifyCodeFixAsync(
        SourceWithFix source,
        params DiagnosticResult[] expected)
    {
        await VerifyCodeFixAsync(new SourceWithFix[] { source }, expected);
    }

    internal static async Task VerifyCodeFixAsync(
        IReadOnlyCollection<SourceWithFix> sourceCode,
        params DiagnosticResult[] expected)
    {
        CodeFixTest test = new CodeFixTest(sourceCode, expected);
        await test.RunAsync(CancellationToken.None);
    }

    private class CodeFixTest : CSharpCodeFixTest<TAnalyzer, TCodeFix, DefaultVerifier>
    {
        internal CodeFixTest(
            IReadOnlyCollection<SourceWithFix> sourceCode,
            params DiagnosticResult[] expected)
        {
            foreach (SourceWithFix source in sourceCode)
            {
                TestState.Sources.Add(source.Source);

                if (source.FixedSource != null)
                {
                    FixedState.Sources.Add(source.FixedSource);
                }
            }

            ExpectedDiagnostics.AddRange(expected);

#if NET10_0
            ReferenceAssemblies = new ReferenceAssemblies(
                "net10.0",
                new PackageIdentity("Microsoft.NETCore.App.Ref", "10.0.0"),
                Path.Combine("ref", "net10.0"));
#else
            throw new NotSupportedException();
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