#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClassTest1()
    {
        Exception? exception = Record.Exception(() => CreateStudent.ReturnIntMethod());
        Assert.Null(exception);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClassTest2()
    {
        int result = CreateStudent.ReturnIntMethod();
        Assert.Equal(24, result);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClassTest3()
    {
        string string1 = "string1";
        int result = CreateStudent.ReturnIntMethodWithRefParameter(ref string1);
        Assert.Equal(28, result);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClassTest4()
    {
        List<int> result = CreateStudent.ReturnListMethod();
        Assert.Equal(new List<int>() { 1, 2, 3 }, result);
    }
}

#endif