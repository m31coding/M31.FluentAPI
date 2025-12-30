#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TryBreakFluentApiClass1;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteTryBreakFluentApiClass1()
    {
        Exception? exception = Record.Exception(() =>
        {
            Student student = CreateStudent
                .SomeMethod("hello world");
        });

        Assert.Null(exception);
    }
}

#endif