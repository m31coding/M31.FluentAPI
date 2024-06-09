// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;
using MyList = System.Collections.Generic.IList<string>;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.AliasNamespaceClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public MyList Friends { get; set; }
}