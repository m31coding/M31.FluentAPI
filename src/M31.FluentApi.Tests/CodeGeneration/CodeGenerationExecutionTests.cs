#define TEST_GENERATED_CODE
#if TEST_GENERATED_CODE

using System.Collections.Generic;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration;

public partial class CodeGenerationTests
{
    [Fact, Priority(1)]
    public void CanExecuteGenericClassWithGenericMethods()
    {
        var student = TestClasses.Abstract.GenericClassWithGenericMethods
            .CreateStudent<string, string?, int, int, int>
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

    [Fact, Priority(1)]
    public void CanExecuteGenericClassWithPrivateGenericMethods()
    {
        var student = TestClasses.Abstract.GenericClassWithPrivateGenericMethods
            .CreateStudent<string, string?, int, int, int>
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

    [Fact, Priority(1)]
    public void CanExecuteGenericClassWithOverloadedGenericMethod()
    {
        string string1 = "string1";
        string string2 = "string2";
        int int0 = 0;

        var student1 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1(0, "string1");
        Assert.Equal(new string[] { "Called Method1(int, string)" }, student1.Logs);

        var student2 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student2.Logs);

        var student3 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<string>("string1", "string2");
        Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student3.Logs);

        var student4 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student4.Logs);

        var student5 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(0, out string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student5.Logs);

        var student6 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(in int0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student6.Logs);

        var student7 = TestClasses.Abstract.GenericClassWithOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(in int0, ref string2);
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student7.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericClassWithPrivateOverloadedGenericMethod()
    {
        string string1 = "string1";
        string string2 = "string2";
        int int0 = 0;

        var student1 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1(0, "string1");
        Assert.Equal(new string[] { "Called Method1(int, string)" }, student1.Logs);

        var student2 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student2.Logs);

        var student3 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<string>("string1", "string2");
        Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student3.Logs);

        var student4 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student4.Logs);

        var student5 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(0, out string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student5.Logs);

        var student6 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(in int0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student6.Logs);

        var student7 = TestClasses.Abstract.GenericClassWithPrivateOverloadedGenericMethod
            .CreateStudent.Method1<int, int>(in int0, ref string2);
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student7.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClass()
    {
        string string1 = "string1";

        TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
            .CreateStudent.ReturnVoidMethod();

        int result1 = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
            .CreateStudent.ReturnIntMethod();
        Assert.Equal(24, result1);

        int result2 = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
            .CreateStudent.ReturnIntMethodWithRefParameter(ref string1);
        Assert.Equal(28, result2);

        List<int> result3 = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
            .CreateStudent.ReturnListMethod();
        Assert.Equal(new List<int>() { 1, 2, 3 }, result3);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnMultiStepPrivateMethodsClass()
    {
        string string1 = "string1";

        TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
            .CreateStudent.WithName("Alice").ReturnVoidMethod();

        int result1 = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
            .CreateStudent.WithName("Alice").ReturnIntMethod();
        Assert.Equal(24, result1);

        int result2 = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
            .CreateStudent.WithName("Alice").ReturnIntMethodWithRefParameter(ref string1);
        Assert.Equal(28, result2);

        List<int> result3 = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
            .CreateStudent.WithName("Alice").ReturnListMethod();
        Assert.Equal(new List<int>() { 1, 2, 3 }, result3);
    }
}
#endif