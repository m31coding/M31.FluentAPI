// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionInterfaceMemberClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public IList<string> Friends { get; set; }

    [FluentCollection(1, "Pet")]
    public IReadOnlyCollection<string> Pets { get; set; }

    [FluentCollection(2, "BackpackContent", "WithBackpackContent", "WithBackpackContent", "WithNoBackpackContent")]
    public ISet<string> BackpackContent { get; set; }
}