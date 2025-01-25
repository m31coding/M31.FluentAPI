// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Reflection;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionNullableArrayClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWhoseFriendsAre
{
    private readonly Student student;
    private static readonly PropertyInfo friendsPropertyInfo;

    static CreateStudent()
    {
        friendsPropertyInfo = typeof(Student).GetProperty("Friends", BindingFlags.Instance | BindingFlags.Public)!;
    }

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static Student WhoseFriendsAre(params string[]? friends)
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.friendsPropertyInfo.SetValue(createStudent.student, friends);
        return createStudent.student;
    }

    public static Student WhoseFriendIs(string friend)
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.friendsPropertyInfo.SetValue(createStudent.student, new string[1]{ friend });
        return createStudent.student;
    }

    public static Student WhoHasNoFriends()
    {
        CreateStudent createStudent = new CreateStudent();
        CreateStudent.friendsPropertyInfo.SetValue(createStudent.student, new string[0]);
        return createStudent.student;
    }

    Student IWhoseFriendsAre.WhoseFriendsAre(params string[]? friends)
    {
        CreateStudent.friendsPropertyInfo.SetValue(student, friends);
        return student;
    }

    Student IWhoseFriendsAre.WhoseFriendIs(string friend)
    {
        CreateStudent.friendsPropertyInfo.SetValue(student, new string[1]{ friend });
        return student;
    }

    Student IWhoseFriendsAre.WhoHasNoFriends()
    {
        CreateStudent.friendsPropertyInfo.SetValue(student, new string[0]);
        return student;
    }

    public interface ICreateStudent : IWhoseFriendsAre
    {
    }

    public interface IWhoseFriendsAre
    {
        Student WhoseFriendsAre(params string[]? friends);

        Student WhoseFriendIs(string friend);

        Student WhoHasNoFriends();
    }
}