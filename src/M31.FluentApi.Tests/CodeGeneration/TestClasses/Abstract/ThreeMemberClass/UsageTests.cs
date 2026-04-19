#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ThreeMemberClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteThreeMemberClass()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanCreateThreeMemberClassFromExisting()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Student newStudent = CreateStudent.FromExisting(student)
            .BornOn(new DateOnly(2003, 4, 4))
            .InSemester(1);

        Assert.Equal("Alice", newStudent.Name);
        Assert.Equal(new DateOnly(2003, 4, 4), newStudent.DateOfBirth);
        Assert.Equal(1, newStudent.Semester);
    }
}

#endif