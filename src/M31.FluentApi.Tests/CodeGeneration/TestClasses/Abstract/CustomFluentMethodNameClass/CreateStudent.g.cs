// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CustomFluentMethodNameClass;

public class CreateStudent : CreateStudent.IWhoIsHappy, CreateStudent.IInSemester, CreateStudent.IWhoseFriendsAre
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static IWhoIsHappy WithName(string firstName, string lastName)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.SetNameFromFirstNameAndLastName(firstName, lastName);
        return createStudent;
    }

    public IInSemester WhoIsHappy(bool isHappy = true)
    {
        student.IsHappy = isHappy;
        return this;
    }

    public IInSemester WhoIsSad()
    {
        student.IsHappy = false;
        return this;
    }

    public IWhoseFriendsAre InSemester(int semester)
    {
        student.Semester = semester;
        return this;
    }

    public Student WhoseFriendsAre(System.Collections.Generic.IReadOnlyCollection<string> friends)
    {
        student.Friends = friends;
        return student;
    }

    public Student WhoseFriendsAre(params string[] friends)
    {
        student.Friends = friends;
        return student;
    }

    public Student WhoseFriendIs(string friend)
    {
        student.Friends = new string[1]{ friend };
        return student;
    }

    public Student WhoHasNoFriends()
    {
        student.Friends = new string[0];
        return student;
    }

    public interface IWhoIsHappy
    {
        IInSemester WhoIsHappy(bool isHappy = true);
        IInSemester WhoIsSad();
    }

    public interface IInSemester
    {
        IWhoseFriendsAre InSemester(int semester);
    }

    public interface IWhoseFriendsAre
    {
        Student WhoseFriendsAre(System.Collections.Generic.IReadOnlyCollection<string> friends);
        Student WhoseFriendsAre(params string[] friends);
        Student WhoseFriendIs(string friend);
        Student WhoHasNoFriends();
    }
}