#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GetPrivateInitPropertyClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteGetPrivateInitPropertyClass()
    {
        var student = CreateStudent
            .InSemester(4);

        Assert.Equal(4, student.Semester);
    }
}

#endif