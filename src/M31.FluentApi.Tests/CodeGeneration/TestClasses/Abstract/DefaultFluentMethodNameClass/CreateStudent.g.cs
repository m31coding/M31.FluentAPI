// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DefaultFluentMethodNameClass;

public class CreateStudent : CreateStudent.IIsHappy, CreateStudent.IWithSemester, CreateStudent.IWithFriends
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static IIsHappy WithName(string firstName, string lastName)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.WithName(firstName, lastName);
        return createStudent;
    }

    public IWithSemester IsHappy(bool isHappy = true)
    {
        student.IsHappy = isHappy;
        return this;
    }

    public IWithSemester NotIsHappy()
    {
        student.IsHappy = false;
        return this;
    }

    public IWithFriends WithSemester(int semester)
    {
        student.Semester = semester;
        return this;
    }

    public Student WithFriends(System.Collections.Generic.IReadOnlyCollection<string> friends)
    {
        student.Friends = friends;
        return student;
    }

    public Student WithFriends(params string[] friends)
    {
        student.Friends = friends;
        return student;
    }

    public Student WithFriend(string friend)
    {
        student.Friends = new string[1]{ friend };
        return student;
    }

    public Student WithZeroFriends()
    {
        student.Friends = new string[0];
        return student;
    }

    public interface IIsHappy
    {
        IWithSemester IsHappy(bool isHappy = true);
        IWithSemester NotIsHappy();
    }

    public interface IWithSemester
    {
        IWithFriends WithSemester(int semester);
    }

    public interface IWithFriends
    {
        Student WithFriends(System.Collections.Generic.IReadOnlyCollection<string> friends);
        Student WithFriends(params string[] friends);
        Student WithFriend(string friend);
        Student WithZeroFriends();
    }
}