#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.SkippableLoopClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteSkippableLoopClass()
    {
        Student student = CreateStudent
            .WithMember3("3")
            .WithMember1("1")
            .WithMember4("4");

        Assert.Equal("1", student.Member1);
        Assert.Null(student.Member2);
        Assert.Equal("3", student.Member3);
        Assert.Equal("4", student.Member4);
    }
}

#endif