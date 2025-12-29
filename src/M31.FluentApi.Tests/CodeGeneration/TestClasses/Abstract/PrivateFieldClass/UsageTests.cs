#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System.Reflection;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PrivateFieldClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecutePrivateReadonlyFieldClass()
    {
        var student = CreateStudent
            .InSemester(4);

        int semester = (int)typeof(Student).GetField(
                "semester",
                BindingFlags.Instance | BindingFlags.NonPublic)
            !.GetValue(student)!;
        Assert.Equal(4, semester);
    }
}

#endif