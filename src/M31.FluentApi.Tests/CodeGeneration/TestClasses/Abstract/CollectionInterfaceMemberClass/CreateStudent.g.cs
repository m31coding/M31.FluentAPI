// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionInterfaceMemberClass;

public class CreateStudent :
    CreateStudent.ICreateStudent,
    CreateStudent.IWhoseFriendsAre,
    CreateStudent.IWithPets,
    CreateStudent.IWithBackpackContent
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static ICreateStudent InitialStep()
    {
        return new CreateStudent();
    }

    public static IWithPets WhoseFriendsAre(System.Collections.Generic.IList<string> friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = friends;
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendsAre(System.Collections.Generic.IList<string> friends)
    {
        student.Friends = friends;
        return this;
    }

    public static IWithPets WhoseFriendsAre(params string[] friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(friends);
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendsAre(params string[] friends)
    {
        student.Friends = new List<string>(friends);
        return this;
    }

    public static IWithPets WhoseFriendIs(string friend)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(1){ friend };
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendIs(string friend)
    {
        student.Friends = new List<string>(1){ friend };
        return this;
    }

    public static IWithPets WhoHasNoFriends()
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(0);
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoHasNoFriends()
    {
        student.Friends = new List<string>(0);
        return this;
    }

    IWithBackpackContent IWithPets.WithPets(System.Collections.Generic.IReadOnlyCollection<string> pets)
    {
        student.Pets = pets;
        return this;
    }

    IWithBackpackContent IWithPets.WithPets(params string[] pets)
    {
        student.Pets = pets;
        return this;
    }

    IWithBackpackContent IWithPets.WithPet(string pet)
    {
        student.Pets = new string[1]{ pet };
        return this;
    }

    IWithBackpackContent IWithPets.WithZeroPets()
    {
        student.Pets = new string[0];
        return this;
    }

    Student IWithBackpackContent.WithBackpackContent(System.Collections.Generic.ISet<string> backpackContent)
    {
        student.BackpackContent = backpackContent;
        return student;
    }

    Student IWithBackpackContent.WithBackpackContent(params string[] backpackContent)
    {
        student.BackpackContent = new HashSet<string>(backpackContent);
        return student;
    }

    Student IWithBackpackContent.WithBackpackContent(string backpackContent)
    {
        student.BackpackContent = new HashSet<string>(1){ backpackContent };
        return student;
    }

    Student IWithBackpackContent.WithNoBackpackContent()
    {
        student.BackpackContent = new HashSet<string>(0);
        return student;
    }

    public interface ICreateStudent : IWhoseFriendsAre
    {
    }

    public interface IWhoseFriendsAre
    {
        IWithPets WhoseFriendsAre(System.Collections.Generic.IList<string> friends);

        IWithPets WhoseFriendsAre(params string[] friends);

        IWithPets WhoseFriendIs(string friend);

        IWithPets WhoHasNoFriends();
    }

    public interface IWithPets
    {
        IWithBackpackContent WithPets(System.Collections.Generic.IReadOnlyCollection<string> pets);

        IWithBackpackContent WithPets(params string[] pets);

        IWithBackpackContent WithPet(string pet);

        IWithBackpackContent WithZeroPets();
    }

    public interface IWithBackpackContent
    {
        Student WithBackpackContent(System.Collections.Generic.ISet<string> backpackContent);

        Student WithBackpackContent(params string[] backpackContent);

        Student WithBackpackContent(string backpackContent);

        Student WithNoBackpackContent();
    }
}