#define TEST_GENERATED_CODE
#if TEST_GENERATED_CODE

// ReSharper disable NotAccessedVariable

using System;
using System.Collections.Generic;
using System.Reflection;
using M31.FluentApi.Tests.CodeGeneration.Helpers;
using Xunit;
using Xunit.Priority;

namespace M31.FluentApi.Tests.CodeGeneration;

public partial class CodeGenerationTests
{


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
        {
            TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
                .CreateStudent.WithName("Alice").ReturnVoidMethod();
        }
        {
            int result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
                .CreateStudent.WithName("Alice").ReturnIntMethod();
            Assert.Equal(24, result);
        }
        {
            string string1 = "string1";
            int result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
                .CreateStudent.WithName("Alice").ReturnIntMethodWithRefParameter(ref string1);
            Assert.Equal(28, result);
        }
        {
            List<int> result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
                .CreateStudent.WithName("Alice").ReturnListMethod();
            Assert.Equal(new List<int>() { 1, 2, 3 }, result);
        }
    }

    [Fact, Priority(1)]
    public void CanExecuteFluentReturnSingleStepPrivateMethodsClass()
    {
        {
            TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
                .CreateStudent.ReturnVoidMethod();
        }
        {
            int result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
                .CreateStudent.ReturnIntMethod();
            Assert.Equal(24, result);
        }
        {
            string string1 = "string1";
            int result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
                .CreateStudent.ReturnIntMethodWithRefParameter(ref string1);
            Assert.Equal(28, result);
        }
        {
            List<int> result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
                .CreateStudent.ReturnListMethod();
            Assert.Equal(new List<int>() { 1, 2, 3 }, result);
        }
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
        {
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1(0, "string1");
            Assert.Equal(new string[] { "Called Method1(int, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<int>(0, "string1");
            Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<string>("string1", "string2");
            Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<int, int>(0, "string1");
            Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student.Logs);
        }
        {
            string string1 = "string1";
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<int, int>(0, out string1);
            Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student.Logs);
        }
        {
            int i = 0;
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<int, int>(in i, "string1");
            Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student.Logs);
        }
        {
            int i = 0;
            string string1 = "string1";
            var student = TestClasses.Abstract.GenericOverloadedMethodClass
                .CreateStudent.Method1<int, int>(in i, ref string1);
            Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student.Logs);
        }
    }

    [Fact, Priority(1)]
    public void CanExecuteGenericOverloadedPrivateMethodClass()
    {
        {
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1(0, "string1");
            Assert.Equal(new string[] { "Called Method1(int, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<int>(0, "string1");
            Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<string>("string1", "string2");
            Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student.Logs);
        }
        {
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<int, int>(0, "string1");
            Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student.Logs);
        }
        {
            string string1 = "string1";
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<int, int>(0, out string1);
            Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student.Logs);
        }
        {
            int i = 0;
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<int, int>(in i, "string1");
            Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student.Logs);
        }
        {
            int i = 0;
            string string1 = "string";
            var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
                .CreateStudent.Method1<int, int>(in i, ref string1);
            Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student.Logs);
        }
    }

    [Fact, Priority(1)]
    public void CanExecuteInheritedClass()
    {
        var student = TestClasses.Abstract.InheritedClass
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
    }


    [Fact, Priority(1)]
    public void CanExecuteInheritedClassProtectedMembers()
    {
        var student = TestClasses.Abstract.InheritedClassProtectedMembers
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", GetProperty("Name"));
        Assert.Equal(new DateOnly(2002, 8, 3), GetProperty("DateOfBirth"));
        Assert.Equal(2, GetProperty("Semester"));

        object GetProperty(string propertyName)
        {
            return typeof(TestClasses.Abstract.InheritedClassProtectedMembers.Student)
                .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetValue(student)!;
        }
    }

    [Fact, Priority(1)]
    public void CanExecuteInheritedClassProtectedSetters()
    {
        var student = TestClasses.Abstract.InheritedClassProtectedSetters
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.Name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
        Assert.Equal(2, student.Semester);
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
        {
            var student = TestClasses.Abstract.SkippableMemberClass
                .CreateStudent
                .WithFirstName("Alice")
                .WithMiddleName("Sophia")
                .WithLastName("King");

            Assert.Equal("Alice", student.FirstName);
            Assert.Equal("Sophia", student.MiddleName);
            Assert.Equal("King", student.LastName);
        }
        {
            var student = TestClasses.Abstract.SkippableMemberClass
                .CreateStudent
                .WithFirstName("Alice")
                .WithLastName("King");

            Assert.Equal("Alice", student.FirstName);
            Assert.Null(student.MiddleName);
            Assert.Equal("King", student.LastName);
        }
    }

    [Fact, Priority(1)]
    public void CanExecuteSkippableFirstMemberClass()
    {
        {
            var student = TestClasses.Abstract.SkippableFirstMemberClass
                .CreateStudent
                .WithFirstName("Alice")
                .WithLastName("King");

            Assert.Equal("Alice", student.FirstName);
            Assert.Equal("King", student.LastName);
        }
        {
            var student = TestClasses.Abstract.SkippableFirstMemberClass
                .CreateStudent
                .WithLastName("King");

            Assert.Null(student.FirstName);
            Assert.Equal("King", student.LastName);
        }
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
        {
            var student = TestClasses.Abstract.SkippableSeveralMembersClass
                .CreateStudent
                .WithMember2("2")
                .WithMember4("4");

            Assert.Null(student.Member1);
            Assert.Equal("2", student.Member2);
            Assert.Null(student.Member3);
            Assert.Equal("4", student.Member4);
        }
        {
            var student = TestClasses.Abstract.SkippableSeveralMembersClass
                .CreateStudent
                .WithMember4("4");

            Assert.Null(student.Member1);
            Assert.Null(student.Member2);
            Assert.Null(student.Member3);
            Assert.Equal("4", student.Member4);
        }
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
    public void CanExecuteThreeMemberRecordPrimaryConstructor()
    {
        var student = TestClasses.Abstract.ThreeMemberRecordPrimaryConstructor
            .CreateStudent
            .WithName("Alice")
            .BornOn(new DateOnly(2002, 8, 3))
            .InSemester(2);

        Assert.Equal("Alice", student.name);
        Assert.Equal(new DateOnly(2002, 8, 3), student.dateOfBirth);
        Assert.Equal(2, student.semester);
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