// <auto-generated/>
// This code was generated by the library M31.FluentAPI.
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#nullable enable

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionInterfaceMemberClass;

public class CreateStudent :
    CreateStudent.IWithPets,
    CreateStudent.IWithBackpackContent
{
    private readonly Student student;

    private CreateStudent()
    {
        student = new Student();
    }

    public static IWithPets WhoseFriendsAre(System.Collections.Generic.IList<string> friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = friends;
        return createStudent;
    }

    public static IWithPets WhoseFriendsAre(params string[] friends)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(friends);
        return createStudent;
    }

    public static IWithPets WhoseFriendIs(string friend)
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(1){ friend };
        return createStudent;
    }

    public static IWithPets WhoHasNoFriends()
    {
        CreateStudent createStudent = new CreateStudent();
        createStudent.student.Friends = new List<string>(0);
        return createStudent;
    }

    public IWithBackpackContent WithPets(System.Collections.Generic.IReadOnlyCollection<string> pets)
    {
        student.Pets = pets;
        return this;
    }

    public IWithBackpackContent WithPets(params string[] pets)
    {
        student.Pets = pets;
        return this;
    }

    public IWithBackpackContent WithPet(string pet)
    {
        student.Pets = new string[1]{ pet };
        return this;
    }

    public IWithBackpackContent WithZeroPets()
    {
        student.Pets = new string[0];
        return this;
    }

    public Student WithBackpackContent(System.Collections.Generic.ISet<string> backpackContent)
    {
        student.BackpackContent = backpackContent;
        return student;
    }

    public Student WithBackpackContent(params string[] backpackContent)
    {
        student.BackpackContent = new HashSet<string>(backpackContent);
        return student;
    }

    public Student WithBackpackContent(string backpackContent)
    {
        student.BackpackContent = new HashSet<string>(1){ backpackContent };
        return student;
    }

    public Student WithNoBackpackContent()
    {
        student.BackpackContent = new HashSet<string>(0);
        return student;
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