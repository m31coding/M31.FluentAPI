#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentReturnMultiStepPrivateMethodsClassTest1()
    {
        Exception? exception = Record.Exception(() => CreateStudent.WithName("Alice").ReturnVoidMethod());
        Assert.Null(exception);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnMultiStepPrivateMethodsClassTest2()
    {
        int result = CreateStudent.WithName("Alice").ReturnIntMethod();
        Assert.Equal(24, result);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnMultiStepPrivateMethodsClassTest3()
    {
        string string1 = "string1";
        int result = CreateStudent.WithName("Alice").ReturnIntMethodWithRefParameter(ref string1);
        Assert.Equal(28, result);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnMultiStepPrivateMethodsClassTest4()
    {
        List<int> result = CreateStudent.WithName("Alice").ReturnListMethod();
        Assert.Equal(new List<int>() { 1, 2, 3 }, result);
    }
}

#endif