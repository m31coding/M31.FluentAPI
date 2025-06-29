// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CommentedClass;

[FluentApi]
public class Student
{
    //// <summary>
    //// Sets the first and last name of the student.
    //// </summary>
    //// <param name="firstName">The student's first name.</param>
    [FluentMember(0, "WithName")]
    public string FirstName { get; set; }

    //// <param name="lastName">The student's last name.</param>
    [FluentMember(0, "WithName")]
    public string LastName{ get; set; }

    //// <summary>
    //// Sets the student's date of birth.
    //// </summary>
    //// <param name="bornOn">The student's date of birth.</param>
    [FluentMember(1, "BornOn")]
    public DateOnly DateOfBirth{ get; set; }

    //// <summary>
    //// Sets the current semester the student is enrolled in.
    //// </summary>
    //// <param name="inSemester">The current semester number.</param>
    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }
}