// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ThreeMemberRecordPrimaryConstructor;

public class CodeGenerationTests
{
    [Fact, Priority(1)]
    public void CanExecuteThreeMemberRecordPrimaryConstructor()
    {
        var student = CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.dateOfBirth);
        Assert.Equal(2, student.semester);
    }
}