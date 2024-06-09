using System.Threading.Tasks;
using M31.FluentApi.Tests.AnalyzerAndCodeFixes.Helpers;
using Microsoft.CodeAnalysis.Testing;
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
        SourceWithFix source = ReadSource("ConflictingControlAttributesClass1", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(13, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(14, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectConflictingControlAttributes2()
    {
        SourceWithFix source = ReadSource("ConflictingControlAttributesClass2", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(13, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(17, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectConflictingControlAttributes3()
    {
        SourceWithFix source = ReadSource("ConflictingControlAttributesClass3", "Student");

        var expectedDiagnostic1 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(16, 6);

        var expectedDiagnostic2 = Verifier.Diagnostic(ConflictingControlAttributes.Descriptor.Id)
            .WithLocation(17, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic1, expectedDiagnostic2);
    }

    [Fact]
    public async Task CanDetectFluentLambdaMemberWithoutFluentApiClass()
    {
        SourceWithFix source = ReadSource("FluentLambdaMemberWithoutFluentApiClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(FluentLambdaMemberWithoutFluentApi.Descriptor.Id)
            .WithLocation(15, 6)
            .WithArguments("Address");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectDuplicateMainAttribute()
    {
        SourceWithFix source = ReadSource("DuplicateMainAttributeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(DuplicateMainAttribute.Descriptor.Id)
            .WithLocation(11, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectGetMissingSetAndAddSetAccessor()
    {
        SourceWithFix source = ReadSource("GetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 9)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectGetMissingSetAndAddSetAccessorForRecords()
    {
        SourceWithFix source = ReadSource("GetMissingSetRecord", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 9)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectGetMissingSetAndAddSetAccessorForRecordStructs()
    {
        SourceWithFix source = ReadSource("GetMissingSetRecordStruct", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 9)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectGetMissingSetAndAddSetAccessorForStructs()
    {
        SourceWithFix source = ReadSource("GetMissingSetStruct", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 9)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidCollectionType()
    {
        SourceWithFix source = ReadSource("InvalidCollectionTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(UnsupportedFluentCollectionType.Descriptor.Id)
            .WithLocation(13, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidFluentMethodReturnType()
    {
        SourceWithFix source = ReadSource("InvalidFluentMethodReturnTypeClass", "Student");

        // Pass null for the fixed source because it does not compile. See also file Student.fixed.illustration.txt.
        source = source with { FixedSource = null };

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentMethodReturnType.Descriptor.Id)
            .WithLocation(15, 12)
            .WithArguments("int");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidFluentPredicateType()
    {
        SourceWithFix source = ReadSource("InvalidFluentPredicateTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentPredicateType.Descriptor.Id)
            .WithLocation(13, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectInvalidNullableType()
    {
        SourceWithFix source = ReadSource("InvalidNullableTypeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(InvalidFluentNullableType.Descriptor.Id)
            .WithLocation(12, 12)
            .WithArguments("int");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectMissingBuilderStep()
    {
        SourceWithFix source = ReadSource("MissingBuilderStepClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingBuilderStep.Descriptor.Id)
            .WithLocation(13, 6)
            .WithArguments(99);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectMissingDefaultConstructor()
    {
        SourceWithFix source = ReadSource("MissingDefaultConstructorClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingDefaultConstructor.Descriptor.Id)
            .WithLocation(8, 14)
            .WithArguments("Student");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectNullableTypeNoNullableAnnotation()
    {
        SourceWithFix source = ReadSource("NullableTypeNoNullableAnnotationClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(FluentNullableTypeWithoutNullableAnnotation.Descriptor.Id)
            .WithLocation(14, 12)
            .WithArguments("string");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectOrthogonalAttributeInCompound()
    {
        SourceWithFix source = ReadSource("OrthogonalAttributeInCompoundClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(OrthogonalAttributeMisusedWithCompound.Descriptor.Id)
            .WithLocation(16, 6)
            .WithArguments("FluentDefault");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectOrthogonalAttributeWithoutMainAttribute()
    {
        SourceWithFix source = ReadSource("OrthogonalAttributeWithoutMainAttributeClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(OrthogonalAttributeMisused.Descriptor.Id)
            .WithLocation(10, 6)
            .WithArguments("FluentDefault");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanHandlePartialClass()
    {
        SourceWithFix source1 = ReadSource("PartialClass", "Student1");
        SourceWithFix source2 = ReadSource("PartialClass", "Student2");

        await Verifier.VerifyCodeFixAsync(
            new SourceWithFix[] { source1, source2 },
            DiagnosticResult.EmptyDiagnosticResults);
    }

    [Fact]
    public async Task CanDetectPrivateGetMissingSetAndAddSetAccessor()
    {
        SourceWithFix source = ReadSource("PrivateGetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 17)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectPublicGetMissingSetAndAddPrivateSetAccessor()
    {
        SourceWithFix source = ReadSource("PublicGetMissingSetClass", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(MissingSetAccessor.Descriptor.Id)
            .WithLocation(11, 16)
            .WithArguments("Semester");

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectSkippableLastStep1()
    {
        SourceWithFix source = ReadSource("SkippableLastStepClass1", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(LastBuilderStepCannotBeSkipped.Descriptor.Id)
            .WithLocation(16, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }

    [Fact]
    public async Task CanDetectSkippableLastStep2()
    {
        SourceWithFix source = ReadSource("SkippableLastStepClass2", "Student");

        var expectedDiagnostic = Verifier.Diagnostic(LastBuilderStepCannotBeSkipped.Descriptor.Id)
            .WithLocation(13, 6);

        await Verifier.VerifyCodeFixAsync(source, expectedDiagnostic);
    }
}