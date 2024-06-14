#define TEST_GENERATED_CODE
#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using System.Collections.Generic;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration;

public partial class CodeGenerationTests
{
    [Fact, Priority(1)]
    public void CanExecuteContinueWithInForkClass()
    {
        var student1 = TestClasses.Abstract.ContinueWithInForkClass
            .CreateStudent
            .WithMember1("1")
            .WithMember2A("2A")
            .WithMember3("3")
            .WithMember4("4");

        var student2 = TestClasses.Abstract.ContinueWithInForkClass
            .CreateStudent
            .WithMember1("1")
            .WithMember2B("2B")
            .WithMember4("4");

        Assert.Equal("1", student1.Member1);
        Assert.Equal("2A", student1.Member2A);
        Assert.Null(student1.Member2B);
        Assert.Equal("3", student1.Member3);
        Assert.Equal("4", student1.Member4);

        Assert.Equal("1", student2.Member1);
        Assert.Null(student2.Member2A);
        Assert.Equal("2B", student2.Member2B);
        Assert.Null(student2.Member3);
        Assert.Equal("4", student2.Member4);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaClass()
    {
        var student = TestClasses.Abstract.FluentLambdaClass
            .CreateStudent
            .WithName("Alice")
            .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("23", student.Address.HouseNumber);
        Assert.Equal("Market Street", student.Address.Street);
        Assert.Equal("San Francisco", student.Address.City);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaCompoundClass()
    {
        var student = TestClasses.Abstract.FluentLambdaCompoundClass
            .CreateStudent
            .WithName("Alice")
            .WithDetails(
                a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"),
                p => p.WithNumber("222-222-2222").WithUsage("CELL"));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("23", student.Address.HouseNumber);
        Assert.Equal("Market Street", student.Address.Street);
        Assert.Equal("San Francisco", student.Address.City);
        Assert.Equal("222-222-2222", student.Phone.Number);
        Assert.Equal("CELL", student.Phone.Usage);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaNullablePropertyClass()
    {
        var student1 = TestClasses.Abstract.FluentLambdaNullablePropertyClass
            .CreateStudent
            .WithName("Alice")
            .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));

        Assert.Equal("Alice", student1.Name);
        Assert.Equal("23", student1.Address!.HouseNumber);
        Assert.Equal("Market Street", student1.Address!.Street);
        Assert.Equal("San Francisco", student1.Address!.City);

        var student2 = TestClasses.Abstract.FluentLambdaNullablePropertyClass
            .CreateStudent
            .WithName("Alice")
            .WithoutAddress();

        Assert.Equal("Alice", student2.Name);
        Assert.Null(student2.Address);

        var student3 = TestClasses.Abstract.FluentLambdaNullablePropertyClass
            .CreateStudent
            .WithName("Alice")
            .WithAddress(_ => null);

        Assert.Equal("Alice", student3.Name);
        Assert.Null(student3.Address);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaRecursiveClass()
    {
        var student = TestClasses.Abstract.FluentLambdaRecursiveClass
            .CreateStudent
            .WithName("Alice")
            .WithFriend(f => f
                .WithName("Bob")
                .WithFriend(f2 => f2
                    .WithName("Eve")
                    .WithoutFriend()));

        Assert.Equal("Alice", student.Name);
        Assert.Equal("Bob", student.Friend!.Name);
        Assert.Equal("Eve", student.Friend!.Friend!.Name);
        Assert.Null(student.Friend!.Friend!.Friend);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentLambdaSingleStepClass()
    {
        var student = TestClasses.Abstract.FluentLambdaSingleStepClass
            .CreateStudent
            .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));

        Assert.Equal("23", student.Address.HouseNumber);
        Assert.Equal("Market Street", student.Address.Street);
        Assert.Equal("San Francisco", student.Address.City);
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentMethodClass()
    {
        var student = TestClasses.Abstract.FluentMethodClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
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
    public void CanExecuteGenericOverloadedMethodClass()
    {
        string string1 = "string1";
        string string2 = "string2";
        int int0 = 0;

        var student1 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1(0, "string1");
        Assert.Equal(new string[] { "Called Method1(int, string)" }, student1.Logs);

        var student2 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student2.Logs);

        var student3 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<string>("string1", "string2");
        Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student3.Logs);

        var student4 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<int, int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student4.Logs);

        var student5 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<int, int>(0, out string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student5.Logs);

        var student6 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<int, int>(in int0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student6.Logs);

        var student7 = TestClasses.Abstract.GenericOverloadedMethodClass
            .CreateStudent.Method1<int, int>(in int0, ref string2);
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student7.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClass()
    {
        string string1 = "string1";
        string string2 = "string2";
        int int0 = 0;

        var student1 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1(0, "string1");
        Assert.Equal(new string[] { "Called Method1(int, string)" }, student1.Logs);

        var student2 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student2.Logs);

        var student3 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<string>("string1", "string2");
        Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student3.Logs);

        var student4 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<int, int>(0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student4.Logs);

        var student5 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<int, int>(0, out string1);
        Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student5.Logs);

        var student6 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<int, int>(in int0, "string1");
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student6.Logs);

        var student7 = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
            .CreateStudent.Method1<int, int>(in int0, ref string2);
        Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student7.Logs);
    }

    [Fact, Priority(1)]
    public void CanExecutePartialClass()
    {
        var student = TestClasses.Abstract.PartialClass
            .CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student.FirstName);
        Assert.Equal("King", student.LastName);
    }

    [Fact, Priority(1)]
    public void CanExecutePrivateConstructorClass()
    {
        var student = TestClasses.Abstract.PrivateConstructorClass
            .CreateStudent
            .InSemester(2);

        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecutePrivateFieldClass()
    {
        var student = TestClasses.Abstract.PrivateFieldClass
            .CreateStudent
            .InSemester(2);

        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecutePrivateFluentMethodClass()
    {
        var student = TestClasses.Abstract.PrivateFluentMethodClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecutePrivateMethodFluentNullableParameterClass()
    {
        var student = TestClasses.Abstract.PrivateFluentMethodNullableParameterClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(null);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Null(student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecutePublicReadonlyFieldClass()
    {
        var student = TestClasses.Abstract.PublicReadonlyFieldClass
            .CreateStudent
            .InSemester(2);

        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableMemberClass()
    {
        var student1 = TestClasses.Abstract.SkippableMemberClass
            .CreateStudent
            .WithFirstName("Alice")
            .WithMiddleName("Sophia")
            .WithLastName("King");

        Assert.Equal("Alice", student1.FirstName);
        Assert.Equal("Sophia", student1.MiddleName);
        Assert.Equal("King", student1.LastName);

        var student2 = TestClasses.Abstract.SkippableMemberClass
            .CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student2.FirstName);
        Assert.Null(student2.MiddleName);
        Assert.Equal("King", student2.LastName);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableFirstMemberClass()
    {
        var student1 = TestClasses.Abstract.SkippableFirstMemberClass
            .CreateStudent
            .WithFirstName("Alice")
            .WithLastName("King");

        Assert.Equal("Alice", student1.FirstName);
        Assert.Equal("King", student1.LastName);

        var student2 = TestClasses.Abstract.SkippableFirstMemberClass
            .CreateStudent
            .WithLastName("King");

        Assert.Null(student2.FirstName);
        Assert.Equal("King", student2.LastName);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableLoopClass()
    {
        var student = TestClasses.Abstract.SkippableLoopClass
            .CreateStudent
            .WithMember3("3")
            .WithMember1("1")
            .WithMember4("4");

        Assert.Equal("1", student.Member1);
        Assert.Null(student.Member2);
        Assert.Equal("3", student.Member3);
        Assert.Equal("4", student.Member4);
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableSeveralMembersClass()
    {
        var student1 = TestClasses.Abstract.SkippableSeveralMembersClass
            .CreateStudent
            .WithMember2("2")
            .WithMember4("4");

        Assert.Null(student1.Member1);
        Assert.Equal("2", student1.Member2);
        Assert.Null(student1.Member3);
        Assert.Equal("4", student1.Member4);

        var student2 = TestClasses.Abstract.SkippableSeveralMembersClass
            .CreateStudent
            .WithMember4("4");

        Assert.Null(student2.Member1);
        Assert.Null(student2.Member2);
        Assert.Null(student2.Member3);
        Assert.Equal("4", student2.Member4);
    }

    [Fact, Priority(1)]
    public void CanExecuteThreeMemberClass()
    {
        var student = TestClasses.Abstract.ThreeMemberClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }

    [Fact, Priority(1)]
    public void CanExecuteThreePrivateMembersClass()
    {
        var student = TestClasses.Abstract.ThreePrivateMembersClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }
}
#endif