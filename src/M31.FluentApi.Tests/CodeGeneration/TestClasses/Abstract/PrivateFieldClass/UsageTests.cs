#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFieldClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecutePrivateFieldClass()
    {
        var student = CreateStudent
            .InSemester(2);

        Assert.Equal(2, student.Semester);
    }
}

#endif