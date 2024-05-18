// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections;
using M31.FluentApi.Attributes;
using System.Collections.Generic;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.NonGenericCollectionMemberClass;

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

    public static IWithPets WhoseFriendsAre(System.Collections.IEnumerable friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = friends;
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendsAre(System.Collections.IEnumerable friends)
    {
        student.Friends = friends;
        return this;
    }

    public static IWithPets WhoseFriendsAre(params object[] friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = friends;
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendsAre(params object[] friends)
    {
        student.Friends = friends;
        return this;
    }

    public static IWithPets WhoseFriendIs(object friend)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new object[1]{ friend };
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoseFriendIs(object friend)
    {
        student.Friends = new object[1]{ friend };
        return this;
    }

    public static IWithPets WhoHasNoFriends()
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new object[0];
        return createStudent;
    }

    IWithPets IWhoseFriendsAre.WhoHasNoFriends()
    {
        student.Friends = new object[0];
        return this;
    }

    IWithBackpackContent IWithPets.WithPets(System.Collections.IList pets)
    {
        student.Pets = pets;
        return this;
    }

    IWithBackpackContent IWithPets.WithPets(params object[] pets)
    {
        student.Pets = new List<object>(pets);
        return this;
    }

    IWithBackpackContent IWithPets.WithPet(object pet)
    {
        student.Pets = new List<object>(1){ pet };
        return this;
    }

    IWithBackpackContent IWithPets.WithZeroPets()
    {
        student.Pets = new List<object>(0);
        return this;
    }

    Student IWithBackpackContent.WithBackpackContent(System.Collections.ICollection backpackContent)
    {
        student.BackpackContent = backpackContent;
        return student;
    }

    Student IWithBackpackContent.WithBackpackContent(params object[] backpackContent)
    {
        student.BackpackContent = new List<object>(backpackContent);
        return student;
    }

    Student IWithBackpackContent.WithBackpackContent(object backpackContent)
    {
        student.BackpackContent = new List<object>(1){ backpackContent };
        return student;
    }

    Student IWithBackpackContent.WithNoBackpackContent()
    {
        student.BackpackContent = new List<object>(0);
        return student;
    }

    public interface ICreateStudent : IWhoseFriendsAre
    {
    }

    public interface IWhoseFriendsAre
    {
        IWithPets WhoseFriendsAre(System.Collections.IEnumerable friends);

        IWithPets WhoseFriendsAre(params object[] friends);

        IWithPets WhoseFriendIs(object friend);

        IWithPets WhoHasNoFriends();
    }

    public interface IWithPets
    {
        IWithBackpackContent WithPets(System.Collections.IList pets);

        IWithBackpackContent WithPets(params object[] pets);

        IWithBackpackContent WithPet(object pet);

        IWithBackpackContent WithZeroPets();
    }

    public interface IWithBackpackContent
    {
        Student WithBackpackContent(System.Collections.ICollection backpackContent);

        Student WithBackpackContent(params object[] backpackContent);

        Student WithBackpackContent(object backpackContent);

        Student WithNoBackpackContent();
    }
}