using System.Threading.Tasks;
using Xunit;
using static M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.TestSourceCodeReader;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;
using Verifier = M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers.AnalyzerAndCodeFixVerifier<
    M31.FluentApi.Generator.SourceAnalyzers.FluentApiAnalyzer,
    M31.FluentApi.Generator.SourceAnalyzers.FluentApiCodeFixProvider>;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes;

public class AnalyzerAndCodeFixTests
{
    [Fact]
    public async Task CanDetectConflictingControlAttributes1()
    {
        (string source, string fixedSource) = ReadSource("ConflictingControlAttributesClass1", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(12, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(13, 6);

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectConflictingControlAttributes2()
    {
        (string source, string fixedSource) = ReadSource("ConflictingControlAttributesClass2", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(12, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(16, 6);

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectConflictingControlAttributes3()
    {
        (string source, string fixedSource) = ReadSource("ConflictingControlAttributesClass3", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(15, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(16, 6);

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectDuplicateMainAttribute()
    {
        (string source, string fixedSource) = ReadSource("DuplicateMainAttributeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(DuplicateMainAttribute.Descriptor.Id)
            .WithLocation(9, 6);

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectGetMissingSetAndAddSetAccessor()
    {
        (string source, string fixedSource) = ReadSource("GetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(9, 9)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidCollectionType()
    {
        (string source, string fixedSource) = ReadSource("InvalidCollectionTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(UnsupportedFluentCollectionType.Descriptor.Id)
            .WithLocation(12, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidFluentMethodReturnType()
    {
        (string source, _) = ReadSource("InvalidFluentMethodReturnTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentMethodReturnType.Descriptor.Id)
            .WithLocation(14, 12)
            .WithArguments("int");

        await Verifier.VerifyCodeFixAsync(source, null, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidFluentPredicateType()
    {
        (string source, string fixedSource) = ReadSource("InvalidFluentPredicateTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentPredicateType.Descriptor.Id)
            .WithLocation(12, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidNullableType()
    {
        (string source, string fixedSource) = ReadSource("InvalidNullableTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentNullableType.Descriptor.Id)
            .WithLocation(10, 12)
            .WithArguments("int");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectMissingBuilderStep()
    {
        (string source, string fixedSource) = ReadSource("MissingBuilderStepClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingBuilderStep.Descriptor.Id)
            .WithLocation(12, 6)
            .WithArguments(99);

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectMissingDefaultConstructor()
    {
        (string source, string fixedSource) = ReadSource("MissingDefaultConstructorClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingDefaultConstructor.Descriptor.Id)
            .WithLocation(6, 14)
            .WithArguments("Student");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectNullableTypeNoNullableAnnotation()
    {
        (string source, string fixedSource) = ReadSource("NullableTypeNoNullableAnnotationClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(FluentNullableTypeWithoutNullableAnnotation.Descriptor.Id)
            .WithLocation(13, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectOrthogonalAttributeInCompound()
    {
        (string source, string fixedSource) = ReadSource("OrthogonalAttributeInCompoundClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(OrthogonalAttributeMisusedWithCompound.Descriptor.Id)
            .WithLocation(15, 6)
            .WithArguments("FluentDefault");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectOrthogonalAttributeWithoutMainAttribute()
    {
        (string source, string fixedSource) = ReadSource("OrthogonalAttributeWithoutMainAttributeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(OrthogonalAttributeMisused.Descriptor.Id)
            .WithLocation(8, 6)
            .WithArguments("FluentDefault");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectPartialClass()
    {
        (string source, string fixedSource) = ReadSource("PartialClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(UnsupportedPartialType.Descriptor.Id)
            .WithLocation(6, 8)
            .WithArguments("Student");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectPrivateGetMissingSetAndAddSetAccessor()
    {
        (string source, string fixedSource) = ReadSource("PrivateGetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(9, 17)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectPublicGetMissingSetAndAddPrivateSetAccessor()
    {
        (string source, string fixedSource) = ReadSource("PublicGetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(9, 16)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, fixedSource, expectedDiagnostic);
    }
}