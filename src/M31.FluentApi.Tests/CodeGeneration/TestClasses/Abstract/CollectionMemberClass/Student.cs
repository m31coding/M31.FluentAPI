// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionMemberClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public List<string> Friends { get; set; }

    [FluentCollection(1, "Pet")]
    public string[] Pets { get; set; }

    [FluentCollection(2, "BackpackContent", "WithBackpackContent", "WithBackpackContent", "WithNoBackpackContent")]
    public HashSet<string> BackpackContent { get; set; }
}