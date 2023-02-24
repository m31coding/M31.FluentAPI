// Non-nullable member is uninitialized

#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CustomFluentMethodNameClass;

[FluentApi]
public class Student
{
    public string Name { get; set; }

    [FluentPredicate(1, "WhoIsHappy", "WhoIsSad")]
    public bool IsHappy { get; set; }

    [FluentMember(2, "InSemester")]
    public int Semester { get; set; }

    [FluentCollection(3, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public IReadOnlyCollection<string> Friends { get; set; }

    [FluentMethod(0, "WithName")]
    public void SetNameFromFirstNameAndLastName(string firstName, string lastName)
    {
        Name = $"{lastName}, {firstName}";
    }
}