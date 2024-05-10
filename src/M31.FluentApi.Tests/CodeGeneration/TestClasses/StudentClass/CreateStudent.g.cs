// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System;
using System.Collections.Generic;
using M31.FluentApi.Attributes;
using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.StudentClass;

public class CreateStudent :
    CreateStudent.IOfAgeBornOn,
    CreateStudent.IInSemester,
    CreateStudent.ILivingIn,
    CreateStudent.IWhoIsHappy,
    CreateStudent.IWhoseFriendsAre
{
    private readonly Student student;
    private static readonly PropertyInfo firstNamePropertyInfo;
    private static readonly PropertyInfo lastNamePropertyInfo;
    private static readonly PropertyInfo agePropertyInfo;
    private static readonly MethodInfo bornOnMethodInfo;
    private static readonly PropertyInfo semesterPropertyInfo;
    private static readonly PropertyInfo cityPropertyInfo;
    private static readonly PropertyInfo isHappyPropertyInfo;
    private static readonly PropertyInfo friendsPropertyInfo;

    static CreateStudent()
    {
        firstNamePropertyInfo = typeof(Student).GetProperty("FirstName", BindingFlags.Instance | BindingFlags.Public)!;
        lastNamePropertyInfo = typeof(Student).GetProperty("LastName", BindingFlags.Instance | BindingFlags.Public)!;
        agePropertyInfo = typeof(Student).GetProperty("Age", BindingFlags.Instance | BindingFlags.Public)!;
        bornOnMethodInfo = typeof(Student).GetMethod(
            "BornOn",
            0,
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            new Type[] { typeof(System.DateOnly) },
            null)!;
        semesterPropertyInfo = typeof(Student).GetProperty("Semester", BindingFlags.Instance | BindingFlags.Public)!;
        cityPropertyInfo = typeof(Student).GetProperty("City", BindingFlags.Instance | BindingFlags.Public)!;
        isHappyPropertyInfo = typeof(Student).GetProperty("IsHappy", BindingFlags.Instance | BindingFlags.Public)!;
        friendsPropertyInfo = typeof(Student).GetProperty("Friends", BindingFlags.Instance | BindingFlags.Public)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static IOfAgeBornOn Named(string firstName, string lastName)
    {
        CreateStudent createStudent = new CreateStudent();
        firstNamePropertyInfo.SetValue(createStudent.student, firstName);
        lastNamePropertyInfo.SetValue(createStudent.student, lastName);
        return createStudent;
    }

    public IInSemester OfAge(int age)
    {
        agePropertyInfo.SetValue(student, age);
        return this;
    }

    public IInSemester BornOn(System.DateOnly dateOfBirth)
    {
        bornOnMethodInfo.Invoke(student, new object?[] { dateOfBirth });
        return this;
    }

    public ILivingIn InSemester(int semester)
    {
        semesterPropertyInfo.SetValue(student, semester);
        return this;
    }

    public ILivingIn WhoStartsUniversity()
    {
        return this;
    }

    public IWhoIsHappy LivingIn(string? city)
    {
        cityPropertyInfo.SetValue(student, city);
        return this;
    }

    public IWhoIsHappy LivingInBoston()
    {
        return this;
    }

    public IWhoIsHappy InUnknownCity()
    {
        cityPropertyInfo.SetValue(student, null);
        return this;
    }

    public IWhoseFriendsAre WhoIsHappy(bool? isHappy = true)
    {
        isHappyPropertyInfo.SetValue(student, isHappy);
        return this;
    }

    public IWhoseFriendsAre WhoIsSad()
    {
        isHappyPropertyInfo.SetValue(student, false);
        return this;
    }

    public IWhoseFriendsAre WithUnknownMood()
    {
        isHappyPropertyInfo.SetValue(student, null);
        return this;
    }

    public Student WhoseFriendsAre(System.Collections.Generic.IReadOnlyCollection<string> friends)
    {
        friendsPropertyInfo.SetValue(student, friends);
        return student;
    }

    public Student WhoseFriendsAre(params string[] friends)
    {
        friendsPropertyInfo.SetValue(student, friends);
        return student;
    }

    public Student WhoseFriendIs(string friend)
    {
        friendsPropertyInfo.SetValue(student, new string[1]{ friend });
        return student;
    }

    public Student WhoHasNoFriends()
    {
        friendsPropertyInfo.SetValue(student, new string[0]);
        return student;
    }

    public interface IOfAgeBornOn
    {
        IInSemester OfAge(int age);

        IInSemester BornOn(System.DateOnly dateOfBirth);
    }

    public interface IInSemester
    {
        ILivingIn InSemester(int semester);

        ILivingIn WhoStartsUniversity();
    }

    public interface ILivingIn
    {
        IWhoIsHappy LivingIn(string? city);

        IWhoIsHappy LivingInBoston();

        IWhoIsHappy InUnknownCity();
    }

    public interface IWhoIsHappy
    {
        IWhoseFriendsAre WhoIsHappy(bool? isHappy = true);

        IWhoseFriendsAre WhoIsSad();

        IWhoseFriendsAre WithUnknownMood();
    }

    public interface IWhoseFriendsAre
    {
        Student WhoseFriendsAre(System.Collections.Generic.IReadOnlyCollection<string> friends);

        Student WhoseFriendsAre(params string[] friends);

        Student WhoseFriendIs(string friend);

        Student WhoHasNoFriends();
    }
}