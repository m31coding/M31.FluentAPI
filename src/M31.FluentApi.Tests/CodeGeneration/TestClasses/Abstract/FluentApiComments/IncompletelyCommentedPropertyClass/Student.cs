// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.
    IncompletelyCommentedPropertyClass;

[FluentApi]
public class Student
{
    /// <fluentSummary>
    /// Sets the student's city.
    /// </fluentSummary>
    /// <fluentParam name="city">The student's city.</fluentParam>
    ///
    /// <fluentSummary>
    /// Set's the student's city to Boston.
    /// </fluentSummary>
    ///
    /// <fluentSummary>
    /// Set's the student's city to null.
    /// </fluentSummary>
    [FluentMember(0, "LivingIn")]
    [FluentDefault("LivingInBoston")]
    [FluentNullable("InUnknownCity")]
    public string? City { get; set; } = "Boston";
}