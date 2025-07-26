// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DocumentationComments.CommentedCompoundClass;

[FluentApi]
public class Student
{
    /// <fluentSummary>
    /// Sets the student's name.
    /// </fluentSummary>
    /// <fluentParam name="firstName">The student's first name.</fluentParam>
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }

    /// <fluentParam name="lastName">The student's last name.</fluentParam>
    [FluentMember(0, "WithName")]
    public string LastName { get; set; }
}