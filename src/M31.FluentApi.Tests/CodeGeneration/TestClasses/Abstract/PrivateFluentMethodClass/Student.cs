// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFluentMethodClass;

[FluentApi]
public class Student
{
    public string Name { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int Semester { get; set; }

    [FluentMethod(0)]
    private void WithName(string name)
    {
        Name = name;
    }

    [FluentMethod(1)]
    private void BornOn(DateOnly date)
    {
        DateOfBirth = date;
    }

    [FluentMethod(2)]
    private void InSemester(int semester)
    {
        Semester = semester;
    }
}