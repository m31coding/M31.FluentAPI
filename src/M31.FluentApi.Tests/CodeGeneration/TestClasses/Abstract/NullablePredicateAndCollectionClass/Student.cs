using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.NullablePredicateAndCollectionClass;

[FluentApi]
public class Student
{
    [FluentCollection(0, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    [FluentNullable("WhoseFriendsAreUnknown")]
    public IReadOnlyCollection<string>? Friends { get; set; }

    [FluentPredicate(1, "WhoIsHappy", "WhoIsSad")]
    [FluentNullable("WithUnknownMood")]
    public bool? IsHappy { get; private set; }
}