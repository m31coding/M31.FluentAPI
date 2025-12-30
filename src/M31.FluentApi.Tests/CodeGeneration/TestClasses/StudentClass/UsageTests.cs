#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.StudentClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteStudentClass()
    {
        Student student = CreateStudent
            .Named("Alice", "King")
            .BornOn(new DateOnly(2002, 7, 4))
            .InSemester(4)
            .LivingInBoston()
            .WhoIsHappy()
            .WhoseFriendsAre("Bob");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("King", student.LastName);
        Assert.True(student.Age > 0);
        Assert.Equal(4, student.Semester);
        Assert.Equal("Boston", student.City);
        Assert.True(student.IsHappy);
        Assert.Equal(["Bob"], student.Friends);
    }
}

#endif