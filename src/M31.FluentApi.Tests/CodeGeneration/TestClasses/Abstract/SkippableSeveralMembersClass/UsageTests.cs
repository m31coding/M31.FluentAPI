#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableSeveralMembersClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteSkippableSeveralMembersClassTest1()
    {
        Student student = CreateStudent
            .WithMember2("2")
            .WithMember4("4");

        Assert.Null(student.Member1);
        Assert.Equal("2", student.Member2);
        Assert.Null(student.Member3);
        Assert.Equal("4", student.Member4);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableSeveralMembersClassTest2()
    {
        Student student = CreateStudent
            .WithMember4("4");

        Assert.Null(student.Member1);
        Assert.Null(student.Member2);
        Assert.Null(student.Member3);
        Assert.Equal("4", student.Member4);
    }
}

#endif