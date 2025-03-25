// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.CollectionMemberClassWithSuppression;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", null, null)]
    public List<string> Friends { get; set; }

    [FluentCollection(1, "Pet", withItem: null, withZeroItems: null)]
    public string[] Pets { get; set; }

    [FluentCollection(2, "BackpackContent", "WithBackpackContent", null, null)]
    public HashSet<string> BackpackContent { get; set; }
}