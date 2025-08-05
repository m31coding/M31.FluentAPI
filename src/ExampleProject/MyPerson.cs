using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
class MyPerson
{
    /// <fluentSummary>
    /// ...
    /// </fluentSummary>
    /// <fluentParam name="firstName">...</fluentParam>
    /// <fluentParam name="lastName">...</fluentParam>
    [FluentMember(0, "Name")]
    public string FirstName { get; set; }

    // todo: check generated code for this duplicate. Also test lambda!
    /// <fluentSummary>
    /// ...
    /// </fluentSummary>
    /// <fluentParam name="firstName">...</fluentParam>
    /// <fluentParam name="lastName">...</fluentParam>
    [FluentMember(0, "Name")]
    public string LastName { get; set; }

    /// <fluentSummary>
    /// Sets the student's age.
    /// </fluentSummary>
    /// <fluentParam name="age">The student's age.</fluentParam>
    [FluentMember(1, "OfAge")]
    public int Age { get; set; }

    /// <fluentSummary method="InSemester">
    /// Sets the student's semester.
    /// </fluentSummary>
    /// <fluentParam method="InSemester" name="semester">The student's semester.</fluentParam>
    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }

    /// <summary>
    /// This summary will not be taken into account.
    /// </summary>
    /// <param name="city">This parameter will not be taken into account.</param>
    [FluentMember(3, "LivingIn")]
    public string City { get; set; }
}