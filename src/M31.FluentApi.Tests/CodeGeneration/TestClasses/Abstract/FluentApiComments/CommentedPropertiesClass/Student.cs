// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All
// ReSharper disable InvalidXmlDocComment

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.CommentedPropertiesClass;

[FluentApi]
public class Student
{
    //// <summary>
    //// This summary will not be taken into account.
    //// </summary>
    //// <param name="givenName">This parameter will not be taken into account.</param>
    [FluentMember(0)]
    public string GivenName { get; set; }

    //// <fluentSummary>
    //// Sets the student's first name.
    //// </fluentSummary>
    //// <fluentParam name="firstName">The student's first name.</fluentParam>
    [FluentMember(0)]
    public string FirstName { get; set; }

    //// <fluentSummary method="WithLastName">
    //// Sets the student's last name.
    //// </fluentSummary>
    //// <fluentParam method="WithLastName" name="lastName">The student's last name.</fluentParam>
    [FluentMember(1)]
    public string LastName { get; set; }

    //// <fluentSummary>
    //// Sets the student's age.
    //// </fluentSummary>
    //// <fluentParam name="age">The student's age.</fluentParam>
    [FluentMember(2, "OfAge")]
    public int Age { get; set; }

    //// <fluentSummary method="InSemester">
    //// Sets the student's semester.
    //// </fluentSummary>
    //// <fluentParam method="InSemester" name="semester">The student's semester.</fluentParam>
    [FluentMember(3, "InSemester")]
    public int Semester { get; set; }

    //// <summary>
    //// This summary will not be taken into account.
    //// </summary>
    //// <param name="city">This parameter will not be taken into account.</param>
    [FluentMember(4, "LivingIn")]
    public string City { get; set; }
}