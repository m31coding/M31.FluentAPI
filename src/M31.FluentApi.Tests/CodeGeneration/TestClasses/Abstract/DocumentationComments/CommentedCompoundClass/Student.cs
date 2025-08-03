// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DocumentationComments.CommentedCompoundClass;
// todo: rename folder to FluentApiComments, remove other occurrences of "DocumentationComments". Hm maybe not.
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

    /// <fluentSummary method="Studies">
    /// Sets the student's course of study and current semester.
    /// </fluentSummary>
    /// <fluentParam method="Studies" name="courseOfStudy">The student's course of study.</fluentParam>
    [FluentMember(1, "Studies")]
    public string CourseOfStudy { get; set; }

    /// <fluentParam method="Studies" name="semester">The student's current semester.</fluentParam>
    [FluentMember(1, "Studies")]
    public int Semester { get; set; }
}