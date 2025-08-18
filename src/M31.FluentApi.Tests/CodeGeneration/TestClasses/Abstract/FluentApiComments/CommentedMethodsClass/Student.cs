// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.CommentedMethodsClass;

[FluentApi]
public class Student
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    //// <summary>
    //// This summary will not be taken into account.
    //// </summary>
    //// <param name="firstName">This parameter documentation will not be taken into account.</param>
    //// <param name="lastName">This parameter documentation will not be taken into account.</param>
    //// <fluentSummary>
    //// Sets the student's first and last name.
    //// </fluentSummary>
    //// <fluentParam name="firstName">The student's first name.</fluentParam>
    //// <fluentParam name="lastName">The student's last name.</fluentParam>
    [FluentMethod(0)]
    private void WithName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    //// <fluentSummary>
    //// Calculates and sets the student's age based on the provided date of birth.
    //// </fluentSummary>
    //// <fluentParam name="dateOfBirth">The student's date of birth.</fluentParam>
    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = new DateOnly(2024, 9, 26);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }
}