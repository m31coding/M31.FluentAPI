// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DocumentationComments.CommentedPropertiesClassAdvanced;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    /// <fluentSummary>
    /// Sets the students's age.
    /// </fluentSummary>
    /// <fluentParam name="age">The student's age.</fluentParam>
    [FluentMember(1)]
    public int Age { get; set; }

    /// <fluentSummary method="LivingIn">
    /// Sets the student's city.
    /// </fluentSummary>
    /// <fluentParam method="LivingIn" name="city">The student's city.</fluentParam>
    /// <fluentSummary method="LivingInBoston">
    /// Set's the student's city to Boston.
    /// </fluentSummary>
    /// <fluentSummary method="InUnknownCity">
    /// Set's the student's city to null.
    /// </fluentSummary>
    [FluentMember(2, "LivingIn")]
    [FluentDefault("LivingInBoston")]
    [FluentNullable("InUnknownCity")]
    public string? City { get; set; } = "Boston";

    [FluentPredicate(3, "WhoIsHappy", "WhoIsSad")]
    [FluentNullable("WithUnknownMood")]
    public bool? IsHappy { get; private set; }
}