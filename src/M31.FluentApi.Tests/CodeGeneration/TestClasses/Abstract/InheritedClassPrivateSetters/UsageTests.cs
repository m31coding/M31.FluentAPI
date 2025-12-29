#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InheritedClassPrivateSetters;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteInheritedClassPrivateSetters()
    {
        var student =
            CreateStudent
                .WithName("Alice")
                .BornOn(new DateOnly(2002, 8, 3))
                .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(22, student.Age);
        Assert.Equal(2, student.Semester);
    }
}

#endif