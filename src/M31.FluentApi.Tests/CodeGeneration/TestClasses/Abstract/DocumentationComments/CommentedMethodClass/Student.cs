// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DocumentationComments.CommentedMethodClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }

    [FluentMember(0, "WithName")]
    public string LastName{ get; set; }

    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    /// <summary>
    /// This summary will not be taken into account.
    /// </summary>
    /// <param name="dateOfBirth">This parameter documentation will not be taken into account.</param>
    /// <fluentSummary>
    /// Calculates and sets the student's age based on the provided date of birth.
    /// </fluentSummary>
    /// <fluentParam name="dateOfBirth">The student's date of birth.</fluentParam>
    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = new DateOnly(2024, 9, 26);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }

    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }
}