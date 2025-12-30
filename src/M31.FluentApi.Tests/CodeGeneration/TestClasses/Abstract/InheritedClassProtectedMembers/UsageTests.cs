#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using System.Reflection;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassProtectedMembers;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteInheritedClassProtectedMembers()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", GetProperty("Name"));
        Assert.Equal(new DateOnly(2002, 8, 3), GetProperty("DateOfBirth"));
        Assert.Equal(2, GetProperty("Semester"));

        object GetProperty(string propertyName)
        {
            return typeof(Student)
                .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(student)!;
        }
    }
}

#endif