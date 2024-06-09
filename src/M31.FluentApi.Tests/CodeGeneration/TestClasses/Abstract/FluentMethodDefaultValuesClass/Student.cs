// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentMethodDefaultValuesClass;

[FluentApi]
public class Student
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth{ get; set; }
    public DateOnly EnrollmentDate { get; set; }
    public int Semester { get; set; }
    public int? NumberOfPassedExams { get; set; }
    public int? NumberOfFailedExams { get; set; }

    [FluentMethod(0)]
    public void WithFirstName(string firstName = "Alice")
    {
        FirstName = firstName;
    }

    [FluentMethod(1)]
    public void WithLastName(string? lastName = null)
    {
        LastName = lastName;
    }

    [FluentMethod(2)]
    public void BornOn(DateOnly date = default)
    {
        DateOfBirth = date;
    }

    [FluentMethod(3)]
    public void EnrolledIn(DateOnly date = new DateOnly())
    {
        EnrollmentDate = date;
    }

    [FluentMethod(4)]
    public void InSemester(int semester = 3)
    {
        Semester = semester;
    }

    [FluentMethod(5)]
    public void WithNumberOfPassedExams(int? numberOfPassedExams = null)
    {
        NumberOfPassedExams = numberOfPassedExams;
    }

    [FluentMethod(6)]
    public void WithNumberOfFailedExams(int? numberOfFailedExams = default)
    {
        NumberOfFailedExams = numberOfFailedExams;
    }
}