#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithInForkClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteContinueWithInForkClassTest1()
    {
        var student = CreateStudent
            .WithMember1("1")
            .WithMember2A("2A")
            .WithMember3("3")
            .WithMember4("4");

        Assert.Equal("1", student.Member1);
        Assert.Equal("2A", student.Member2A);
        Assert.Null(student.Member2B);
        Assert.Equal("3", student.Member3);
        Assert.Equal("4", student.Member4);
    }

    [Fact, Priority(1)]
    public void CanExecuteContinueWithInForkClassTest2()
    {
        var student = CreateStudent
            .WithMember1("1")
            .WithMember2B("2B")
            .WithMember4("4");

        Assert.Equal("1", student.Member1);
        Assert.Null(student.Member2A);
        Assert.Equal("2B", student.Member2B);
        Assert.Null(student.Member3);
        Assert.Equal("4", student.Member4);
    }
}

#endif