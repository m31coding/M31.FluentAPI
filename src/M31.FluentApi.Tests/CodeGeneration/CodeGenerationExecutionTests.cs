// #define TEST_GENERATED_CODE
// #if TEST_GENERATED_CODE
//
// // ReSharper disable NotAccessedVariable
//
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using M31.FluentApi.Tests.CodeGeneration.Helpers;
// using Xunit;
// using Xunit.Priority;
//
// namespace M31.FluentApi.Tests.CodeGeneration;
//
// public partial class CodeGenerationTests
// {
//     [Fact, Priority(1)]
//     public void CanExecuteContinueWithInForkClass()
//     {
//         {
//             var student = TestClasses.Abstract.ContinueWithInForkClass
//                 .CreateStudent
//                 .WithMember1("1")
//                 .WithMember2A("2A")
//                 .WithMember3("3")
//                 .WithMember4("4");
//
//             Assert.Equal("1", student.Member1);
//             Assert.Equal("2A", student.Member2A);
//             Assert.Null(student.Member2B);
//             Assert.Equal("3", student.Member3);
//             Assert.Equal("4", student.Member4);
//         }
//         {
//             var student = TestClasses.Abstract.ContinueWithInForkClass
//                 .CreateStudent
//                 .WithMember1("1")
//                 .WithMember2B("2B")
//                 .WithMember4("4");
//
//             Assert.Equal("1", student.Member1);
//             Assert.Null(student.Member2A);
//             Assert.Equal("2B", student.Member2B);
//             Assert.Null(student.Member3);
//             Assert.Equal("4", student.Member4);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaClass()
//     {
//         var student = TestClasses.Abstract.FluentLambdaClass
//             .CreateStudent
//             .WithName("Alice")
//             .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal("23", student.Address.HouseNumber);
//         Assert.Equal("Market Street", student.Address.Street);
//         Assert.Equal("San Francisco", student.Address.City);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaCollectionClass()
//     {
//         var student = TestClasses.Abstract.FluentLambdaCollectionClass
//             .CreateStudent
//             .WithName("Alice")
//             .WithAddresses(
//                 a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"),
//                 a => a.WithHouseNumber("108").WithStreet("5th Avenue").InCity("New York"));
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(2, student.Addresses.Count);
//         Assert.Equal("23", student.Addresses[0].HouseNumber);
//         Assert.Equal("Market Street", student.Addresses[0].Street);
//         Assert.Equal("San Francisco", student.Addresses[0].City);
//         Assert.Equal("108", student.Addresses[1].HouseNumber);
//         Assert.Equal("5th Avenue", student.Addresses[1].Street);
//         Assert.Equal("New York", student.Addresses[1].City);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaCompoundClass()
//     {
//         var student = TestClasses.Abstract.FluentLambdaCompoundClass
//             .CreateStudent
//             .WithName("Alice")
//             .WithDetails(
//                 a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"),
//                 p => p.WithNumber("222-222-2222").WithUsage("CELL"));
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal("23", student.Address.HouseNumber);
//         Assert.Equal("Market Street", student.Address.Street);
//         Assert.Equal("San Francisco", student.Address.City);
//         Assert.Equal("222-222-2222", student.Phone.Number);
//         Assert.Equal("CELL", student.Phone.Usage);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaManyCollectionsClass()
//     {
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesE(createAddressesE: null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.AddressesE);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesF(createAddressesF: _ => null);
//
//             Assert.Single(student.AddressesF);
//             Assert.Null(student.AddressesF[0]);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesG(createAddressesG: null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.AddressesG);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesG(createAddressesG: _ => null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.NotNull(student.AddressesG);
//             Assert.Single(student.AddressesG!);
//             Assert.Null(student.AddressesG![0]);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaManyPrivateCollectionsClass()
//     {
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesE(createAddressesE: null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.AddressesE);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesF(createAddressesF: _ => null);
//
//             Assert.Single(student.AddressesF);
//             Assert.Null(student.AddressesF[0]);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesG(createAddressesG: null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.AddressesG);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaManyPrivateCollectionsClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddressesG(createAddressesG: _ => null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.NotNull(student.AddressesG);
//             Assert.Single(student.AddressesG!);
//             Assert.Null(student.AddressesG![0]);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaNullablePropertyClass()
//     {
//         {
//             var student = TestClasses.Abstract.FluentLambdaNullablePropertyClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Equal("23", student.Address!.HouseNumber);
//             Assert.Equal("Market Street", student.Address!.Street);
//             Assert.Equal("San Francisco", student.Address!.City);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaNullablePropertyClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithoutAddress();
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.Address);
//         }
//         {
//             var student = TestClasses.Abstract.FluentLambdaNullablePropertyClass
//                 .CreateStudent
//                 .WithName("Alice")
//                 .WithAddress(_ => null);
//
//             Assert.Equal("Alice", student.Name);
//             Assert.Null(student.Address);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaRecursiveClass()
//     {
//         var student = TestClasses.Abstract.FluentLambdaRecursiveClass
//             .CreateStudent
//             .WithName("Alice")
//             .WithFriend(f => f
//                 .WithName("Bob")
//                 .WithFriend(f2 => f2
//                     .WithName("Eve")
//                     .WithoutFriend()));
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal("Bob", student.Friend!.Name);
//         Assert.Equal("Eve", student.Friend!.Friend!.Name);
//         Assert.Null(student.Friend!.Friend!.Friend);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentLambdaSingleStepClass()
//     {
//         var student = TestClasses.Abstract.FluentLambdaSingleStepClass
//             .CreateStudent
//             .WithAddress(a => a.WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco"));
//
//         Assert.Equal("23", student.Address.HouseNumber);
//         Assert.Equal("Market Street", student.Address.Street);
//         Assert.Equal("San Francisco", student.Address.City);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentMethodClass()
//     {
//         var student = TestClasses.Abstract.FluentMethodClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentReturnMultiStepPrivateMethodsClass()
//     {
//         {
//             TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
//                 .CreateStudent.WithName("Alice").ReturnVoidMethod();
//         }
//         {
//             int result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
//                 .CreateStudent.WithName("Alice").ReturnIntMethod();
//             Assert.Equal(24, result);
//         }
//         {
//             string string1 = "string1";
//             int result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
//                 .CreateStudent.WithName("Alice").ReturnIntMethodWithRefParameter(ref string1);
//             Assert.Equal(28, result);
//         }
//         {
//             List<int> result = TestClasses.Abstract.FluentReturnMultiStepPrivateMethodsClass
//                 .CreateStudent.WithName("Alice").ReturnListMethod();
//             Assert.Equal(new List<int>() { 1, 2, 3 }, result);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteFluentReturnSingleStepPrivateMethodsClass()
//     {
//         {
//             TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
//                 .CreateStudent.ReturnVoidMethod();
//         }
//         {
//             int result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
//                 .CreateStudent.ReturnIntMethod();
//             Assert.Equal(24, result);
//         }
//         {
//             string string1 = "string1";
//             int result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
//                 .CreateStudent.ReturnIntMethodWithRefParameter(ref string1);
//             Assert.Equal(28, result);
//         }
//         {
//             List<int> result = TestClasses.Abstract.FluentReturnSingleStepPrivateMethodsClass
//                 .CreateStudent.ReturnListMethod();
//             Assert.Equal(new List<int>() { 1, 2, 3 }, result);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteGenericClassWithGenericMethods()
//     {
//         var student = TestClasses.Abstract.GenericClassWithGenericMethods
//             .CreateStudent<string, string?, int, int, int>
//             .WithProperty1("property1")
//             .WithProperty2(null)
//             .WithProperty3(0)
//             .WithProperty4(0)
//             .WithProperty5(0)
//             .Method1(0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(), new List<int>())
//             .Method2("string1", null, 0, 0, 0, 0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(),
//                 new List<int>())
//             .Method3<int, ListAndDictionary<int, string>, Dictionary<int, string>, List<int>>("string1");
//
//         string[] expectedLogs = { "Called Method1", "Called Method2", "Called Method3" };
//         Assert.Equal(expectedLogs, student.Logs);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteGenericClassWithPrivateGenericMethods()
//     {
//         var student = TestClasses.Abstract.GenericClassWithPrivateGenericMethods
//             .CreateStudent<string, string?, int, int, int>
//             .WithProperty1("property1")
//             .WithProperty2(null)
//             .WithProperty3(0)
//             .WithProperty4(0)
//             .WithProperty5(0)
//             .Method1(0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(), new List<int>())
//             .Method2("string1", null, 0, 0, 0, 0, new ListAndDictionary<int, string>(), new Dictionary<int, string>(),
//                 new List<int>())
//             .Method3<int, ListAndDictionary<int, string>, Dictionary<int, string>, List<int>>("string1");
//
//         string[] expectedLogs = { "Called Method1", "Called Method2", "Called Method3" };
//         Assert.Equal(expectedLogs, student.Logs);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteGenericOverloadedMethodClass()
//     {
//         {
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1(0, "string1");
//             Assert.Equal(new string[] { "Called Method1(int, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<int>(0, "string1");
//             Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<string>("string1", "string2");
//             Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<int, int>(0, "string1");
//             Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student.Logs);
//         }
//         {
//             string string1 = "string1";
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<int, int>(0, out string1);
//             Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student.Logs);
//         }
//         {
//             int i = 0;
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<int, int>(in i, "string1");
//             Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student.Logs);
//         }
//         {
//             int i = 0;
//             string string1 = "string1";
//             var student = TestClasses.Abstract.GenericOverloadedMethodClass
//                 .CreateStudent.Method1<int, int>(in i, ref string1);
//             Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student.Logs);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteGenericOverloadedPrivateMethodClass()
//     {
//         {
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1(0, "string1");
//             Assert.Equal(new string[] { "Called Method1(int, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<int>(0, "string1");
//             Assert.Equal(new string[] { "Called Method1<T>(int, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<string>("string1", "string2");
//             Assert.Equal(new string[] { "Called Method1<T>(T, string)" }, student.Logs);
//         }
//         {
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<int, int>(0, "string1");
//             Assert.Equal(new string[] { "Called Method1<S, T>(T, string)" }, student.Logs);
//         }
//         {
//             string string1 = "string1";
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<int, int>(0, out string1);
//             Assert.Equal(new string[] { "Called Method1<S, T>(T, out string)" }, student.Logs);
//         }
//         {
//             int i = 0;
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<int, int>(in i, "string1");
//             Assert.Equal(new string[] { "Called Method1<S, T>(in T, string)" }, student.Logs);
//         }
//         {
//             int i = 0;
//             string string1 = "string";
//             var student = TestClasses.Abstract.GenericOverloadedPrivateMethodClass
//                 .CreateStudent.Method1<int, int>(in i, ref string1);
//             Assert.Equal(new string[] { "Called Method1<S, T>(in T, ref string)" }, student.Logs);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteInheritedClass()
//     {
//         var student = TestClasses.Abstract.InheritedClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteInheritedClassPrivateSetters()
//     {
//         var student = TestClasses.Abstract.InheritedClassPrivateSetters
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(22, student.Age);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteInheritedClassProtectedMembers()
//     {
//         var student = TestClasses.Abstract.InheritedClassProtectedMembers
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", GetProperty("Name"));
//         Assert.Equal(new DateOnly(2002, 8, 3), GetProperty("DateOfBirth"));
//         Assert.Equal(2, GetProperty("Semester"));
//
//         object GetProperty(string propertyName)
//         {
//             return typeof(TestClasses.Abstract.InheritedClassProtectedMembers.Student)
//                 .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic)?
//                 .GetValue(student)!;
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteInheritedClassProtectedSetters()
//     {
//         var student = TestClasses.Abstract.InheritedClassProtectedSetters
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteInheritedRecord()
//     {
//         var student = TestClasses.Abstract.InheritedRecord
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePartialClass()
//     {
//         var student = TestClasses.Abstract.PartialClass
//             .CreateStudent
//             .WithFirstName("Alice")
//             .WithLastName("King");
//
//         Assert.Equal("Alice", student.FirstName);
//         Assert.Equal("King", student.LastName);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePrivateConstructorClass()
//     {
//         var student = TestClasses.Abstract.PrivateConstructorClass
//             .CreateStudent
//             .InSemester(2);
//
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePrivateFieldClass()
//     {
//         var student = TestClasses.Abstract.PrivateFieldClass
//             .CreateStudent
//             .InSemester(2);
//
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePrivateFluentMethodClass()
//     {
//         var student = TestClasses.Abstract.PrivateFluentMethodClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePrivateMethodFluentNullableParameterClass()
//     {
//         var student = TestClasses.Abstract.PrivateFluentMethodNullableParameterClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(null);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Null(student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecutePublicReadonlyFieldClass()
//     {
//         var student = TestClasses.Abstract.PublicReadonlyFieldClass
//             .CreateStudent
//             .InSemester(2);
//
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteSkippableMemberClass()
//     {
//         {
//             var student = TestClasses.Abstract.SkippableMemberClass
//                 .CreateStudent
//                 .WithFirstName("Alice")
//                 .WithMiddleName("Sophia")
//                 .WithLastName("King");
//
//             Assert.Equal("Alice", student.FirstName);
//             Assert.Equal("Sophia", student.MiddleName);
//             Assert.Equal("King", student.LastName);
//         }
//         {
//             var student = TestClasses.Abstract.SkippableMemberClass
//                 .CreateStudent
//                 .WithFirstName("Alice")
//                 .WithLastName("King");
//
//             Assert.Equal("Alice", student.FirstName);
//             Assert.Null(student.MiddleName);
//             Assert.Equal("King", student.LastName);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteSkippableFirstMemberClass()
//     {
//         {
//             var student = TestClasses.Abstract.SkippableFirstMemberClass
//                 .CreateStudent
//                 .WithFirstName("Alice")
//                 .WithLastName("King");
//
//             Assert.Equal("Alice", student.FirstName);
//             Assert.Equal("King", student.LastName);
//         }
//         {
//             var student = TestClasses.Abstract.SkippableFirstMemberClass
//                 .CreateStudent
//                 .WithLastName("King");
//
//             Assert.Null(student.FirstName);
//             Assert.Equal("King", student.LastName);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteSkippableLoopClass()
//     {
//         var student = TestClasses.Abstract.SkippableLoopClass
//             .CreateStudent
//             .WithMember3("3")
//             .WithMember1("1")
//             .WithMember4("4");
//
//         Assert.Equal("1", student.Member1);
//         Assert.Null(student.Member2);
//         Assert.Equal("3", student.Member3);
//         Assert.Equal("4", student.Member4);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteSkippableSeveralMembersClass()
//     {
//         {
//             var student = TestClasses.Abstract.SkippableSeveralMembersClass
//                 .CreateStudent
//                 .WithMember2("2")
//                 .WithMember4("4");
//
//             Assert.Null(student.Member1);
//             Assert.Equal("2", student.Member2);
//             Assert.Null(student.Member3);
//             Assert.Equal("4", student.Member4);
//         }
//         {
//             var student = TestClasses.Abstract.SkippableSeveralMembersClass
//                 .CreateStudent
//                 .WithMember4("4");
//
//             Assert.Null(student.Member1);
//             Assert.Null(student.Member2);
//             Assert.Null(student.Member3);
//             Assert.Equal("4", student.Member4);
//         }
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteThreeMemberClass()
//     {
//         var student = TestClasses.Abstract.ThreeMemberClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteThreeMemberRecordPrimaryConstructor()
//     {
//         var student = TestClasses.Abstract.ThreeMemberRecordPrimaryConstructor
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.dateOfBirth);
//         Assert.Equal(2, student.semester);
//     }
//
//     [Fact, Priority(1)]
//     public void CanExecuteThreePrivateMembersClass()
//     {
//         var student = TestClasses.Abstract.ThreePrivateMembersClass
//             .CreateStudent
//             .WithName("Alice")
//             .BornOn(new DateOnly(2002, 8, 3))
//             .InSemester(2);
//
//         Assert.Equal("Alice", student.Name);
//         Assert.Equal(new DateOnly(2002, 8, 3), student.DateOfBirth);
//         Assert.Equal(2, student.Semester);
//     }
// }
// #endif