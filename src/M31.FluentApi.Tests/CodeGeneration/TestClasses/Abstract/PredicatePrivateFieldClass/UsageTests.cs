#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System.Reflection;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PredicatePrivateFieldClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecutePredicatePrivateFieldClass()
    {
        var student = CreateStudent
            .WhoIsHappy();

        bool isHappy = (bool)typeof(Student).GetField(
                "isHappy",
                BindingFlags.Instance | BindingFlags.NonPublic)
            !.GetValue(student)!;
        Assert.True(isHappy);
    }
}

#endif