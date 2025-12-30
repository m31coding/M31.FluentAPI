#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System.Collections.Generic;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClassWithPrivateGenericMethods;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteGenericClassWithGenericMethods()
    {
        var student = CreateStudent<string, string?, int, int, int>
            .WithProperty1("property1")
            .WithProperty2(null)
            .WithProperty3(0)
            .WithProperty4(0)
            .WithProperty5(0)
            .Method1(0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(), new List<int>())
            .Method2("string1", null, 0, 0, 0, 0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(),
                new List<int>())
            .Method3<int, ListAndDictionary<int, string>, Dictionary<int, string>, List<int>>("string1");

        string[] expectedLogs = { "Called Method1", "Called Method2", "Called Method3" };
        Assert.Equal(expectedLogs, student.Logs);
    }
}

#endif