// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CommentedClass;

[FluentApi]
public class Student
{
    /// <fluentSummary>
    /// Sets the first and last name of the student.
    /// And other stuff.
    /// </fluentSummary>
    /// <summary>
    /// Some irrelevant summary.
    /// </summary>
    /**
     *  <fluentSummary>Hello
     *  World
     *  </fluentSummary>
     */
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }

    [FluentMember(0, "WithName")]
    public string LastName{ get; set; }

    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    /// <fluentSummary>
    /// Calculates and sets the student's age based on the provided date of birth.
    /// </fluentSummary>
    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = new DateOnly(2024, 9, 26);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }

    /// <fluentSummary>
    /// Sets the current semester the student is enrolled in.
    /// </fluentSummary>
    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }
}