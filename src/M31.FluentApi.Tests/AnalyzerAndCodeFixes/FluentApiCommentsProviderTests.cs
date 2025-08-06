using System;
using System.IO;
using System.Threading.Tasks;
using M31.FluentApi.Attributes;
using M31.FluentApi.Generator.SourceAnalyzers.FluentApiComments;
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
    [InlineData("CommentedClass", "Student", "FirstName", "FirstName")]
    [InlineData("CommentedClass", "Student", "LastNa", "LastName")]
    [InlineData("CommentedClass", "Student", "int Age ", "Age")]
    [InlineData("CommentedClass", "Student", " BornOn", "BornOn")]
    [InlineData("CommentedClass", "Student", "int Sem", "Semester")]
    [InlineData("CommentedClass", "Student", " City ", "City")]
    [InlineData("CommentedClass", "Student", "IsHappy ", "IsHappy")]
    [InlineData("CommentedClass", "Student", " Friends", "Friends")]
    [InlineData("LambdaCollectionClass", "Student", "PhoneNumbers", "PhoneNumbers")]
    public async Task CanProvideFluentApiComments(
        string commentTestClass, string @class, string selectedSpan, string member)
    {
        SourceWithFix source = ReadSource(Path.Combine("FluentApiComments", commentTestClass), @class,
            $"Student.{member}.txt");
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
            throw new NotSupportedException();
#endif

            TestState =
            {
                AdditionalReferences = { typeof(FluentApiAttribute).Assembly }
            }
        };

        await test.RunAsync();
    }
}