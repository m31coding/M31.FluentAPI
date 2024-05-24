// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class Student
{
    [FluentMember(0, "Named", 0)]
    public string FirstName { get; private set; }

    [FluentMember(0, "Named", 1)]
    public string LastName { get; private set; }

    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }

    [FluentMember(2, "InSemester")]
    [FluentDefault("WhoStartsUniversity")]
    public int Semester { get; private set; } = 0;

    [FluentMember(3, "LivingIn")]
    [FluentDefault("LivingInBoston")]
    [FluentNullable("InUnknownCity")]
    public string? City { get; private set; } = "Boston";

    [FluentPredicate(4, "WhoIsHappy", "WhoIsSad")]
    [FluentNullable("WithUnknownMood")]
    public bool? IsHappy { get; private set; }

    [FluentCollection(5, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public IReadOnlyCollection<string> Friends { get; private set; }
}