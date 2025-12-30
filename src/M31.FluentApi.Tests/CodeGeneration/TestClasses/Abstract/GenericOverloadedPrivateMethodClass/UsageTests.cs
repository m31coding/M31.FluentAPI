#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericOverloadedPrivateMethodClass;

public class UsageTests
{
    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest1()
    {
        Student student = CreateStudent.Method1(0, "string1");
        Assert.Equal(new string[] { "Called Method1(int, string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest2()
    {
        Student student = CreateStudent.Method1<int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest3()
    {
        Student student = CreateStudent.Method1<string>("string1", "string2");
        Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest4()
    {
        Student student = CreateStudent.Method1<int, int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest5()
    {
        string string1 = "string1";
        Student student = CreateStudent.Method1<int, int>(0, out string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest6()
    {
        int i = 0;
        Student student = CreateStudent.Method1<int, int>(in i, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClassTest7()
    {
        int i = 0;
        string string1 = "string";
        Student student = CreateStudent.Method1<int, int>(in i, ref string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student.Logs);
    }
}

#endif