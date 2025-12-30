#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateConstructorClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecutePrivateConstructorClass()
    {
        Student student = CreateStudent
            .InSemester(2);

        Assert.Equal(2, student.Semester);
    }
}

#endif