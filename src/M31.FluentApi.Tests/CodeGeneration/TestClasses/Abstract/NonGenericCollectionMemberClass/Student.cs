// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System.Collections;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.NonGenericCollectionMemberClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public IEnumerable Friends { get; set; }

    [FluentCollection(1, "Pet")]
    public IList Pets { get; set; }

    [FluentCollection(2, "BackpackContent", "WithBackpackContent", "WithBackpackContent", "WithNoBackpackContent")]
    public ICollection BackpackContent { get; set; }
}