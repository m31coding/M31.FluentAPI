using System.IO;
using System.Threading.Tasks;
using M31.FluentApi.Attributes;
using M31.FluentApi.Generator.SourceAnalyzers.DocumentationComments;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using Xunit;
using M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;
using Microsoft.CodeAnalysis.Testing;
using static M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.TestSourceCodeReader;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes;

public class FluentApiCommentsProviderTests
{
    [Theory]
    [InlineData("FluentApiComments", "FirstName", "FirstName")]
    [InlineData("FluentApiComments", "LastNa", "LastName")]
    [InlineData("FluentApiComments", "int Age ", "Age")]
    [InlineData("FluentApiComments", " BornOn", "BornOn")]
    [InlineData("FluentApiComments", "int Sem", "Semester")]
    [InlineData("FluentApiComments", " City ", "City")]
    [InlineData("FluentApiComments", "IsHappy ", "IsHappy")]
    [InlineData("FluentApiComments", " Friends", "Friends")]
    public async Task CanProvideFluentApiComments(string commentTestClass, string selectedSpan, string member)
    {
        SourceWithFix source = ReadSource(commentTestClass, "Student", $"Student.{member}.txt");
        var test = new CSharpCodeRefactoringTest<FluentApiCommentsProvider, XUnitVerifier>
        {
            TestCode = source.Source.SelectSpan(selectedSpan),
            FixedCode = source.FixedSource!,
#if NET6_0
            ReferenceAssemblies = new ReferenceAssemblies(
                "net6.0",
                new PackageIdentity("Microsoft.NETCore.App.Ref", "6.0.0"),
                Path.Combine("ref", "net6.0")),
#else
            throw new NotImplementedException();
#endif
            TestState =
            {
                AdditionalReferences = { typeof(FluentApiAttribute).Assembly }
            }
        };
        await test.RunAsync();
    }
}