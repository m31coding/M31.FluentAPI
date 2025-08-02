using System.IO;
using System.Threading.Tasks;
using M31.FluentApi.Attributes;
using M31.FluentApi.Generator.SourceAnalyzers;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Xunit;
using M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;
using static M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.TestSourceCodeReader;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes;

public class MyRefactoringProviderTests
{
    [Theory]
    [InlineData("UncommentedPropertyClass", "FirstName")]
    public async Task CanProvideFluentApiComments(string commentTestClass, string selectedSpan)
    {
        SourceWithFix source = ReadSource(Path.Join("FluentApiComments", commentTestClass), "Student");
        await new CSharpCodeRefactoringTest<FluentApiCommentsProvider, XUnitVerifier>
        {
            TestCode = source.Source.SelectSpan(selectedSpan),
            FixedCode = source.FixedSource!,
            TestState =
            {
                 AdditionalReferences = { typeof(FluentApiAttribute).Assembly }
            }
        }.RunAsync();
    }
}