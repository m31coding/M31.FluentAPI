#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteInheritedClass()
    {
        Student student = CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }
}

#endif