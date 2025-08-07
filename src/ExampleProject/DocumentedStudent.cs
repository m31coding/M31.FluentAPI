// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class DocumentedStudent
{
    /// <fluentSummary>
    /// Sets the student's name.
    /// </fluentSummary>
    /// <fluentParam name="firstName">The student's first name.</fluentParam>
    /// <fluentParam name="lastName">The student's last name.</fluentParam>
    /// <fluentReturns>A builder for setting the student's age.</fluentReturns>
    [FluentMember(0, "Named", 0)]
    public string FirstName { get; private set; }

    [FluentMember(0, "Named", 1)]
    public string LastName { get; private set; }

    /// <fluentSummary>
    /// Sets the student's age.
    /// </fluentSummary>
    /// <fluentParam name="age">The student's age.</fluentParam>
    /// <fluentReturns>A builder for setting the student's semester.</fluentReturns>
    [FluentMember(1, "OfAge")]
    public int Age { get; private set; }

    /// <fluentSummary>
    /// Sets the student's age based on their date of birth.
    /// </fluentSummary>
    /// <fluentParam name="dateOfBirth">The student's date of birth.</fluentParam>
    /// <fluentReturns>A builder for setting the student's semester.</fluentReturns>
    [FluentMethod(1)]
    private void BornOn(DateOnly dateOfBirth)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        Age = age;
    }

    /// <fluentSummary method="InSemester">
    /// Sets the student's current semester.
    /// </fluentSummary>
    /// <fluentParam method="InSemester" name="semester">The student's current semester.</fluentParam>
    /// <fluentReturns method="InSemester">A builder for setting the student's city.</fluentReturns>
    ///
    /// <fluentSummary method="WhoStartsUniversity">
    /// Sets the student's semester to 0.
    /// </fluentSummary>
    /// <fluentReturns method="WhoStartsUniversity">A builder for setting the student's city.</fluentReturns>
    [FluentMember(2, "InSemester")]
    [FluentDefault("WhoStartsUniversity")]
    public int Semester { get; private set; } = 0;

    /// <fluentSummary method="LivingIn">
    /// Sets the student's city.
    /// </fluentSummary>
    /// <fluentParam method="LivingIn" name="city">The student's city.</fluentParam>
    /// <fluentReturns method="LivingIn">A builder for setting whether the student is happy.</fluentReturns>
    ///
    /// <fluentSummary method="LivingInBoston">
    /// Sets the student's city to Boston.
    /// </fluentSummary>
    /// <fluentReturns method="LivingInBoston">A builder for setting whether the student is happy.</fluentReturns>
    ///
    /// <fluentSummary method="InUnknownCity">
    /// Sets the student's city to null.
    /// </fluentSummary>
    /// <fluentReturns method="InUnknownCity">A builder for setting whether the student is happy.</fluentReturns>
    [FluentMember(3, "LivingIn")]
    [FluentDefault("LivingInBoston")]
    [FluentNullable("InUnknownCity")]
    public string? City { get; private set; } = "Boston";

    /// <fluentSummary method="WhoIsHappy">
    /// Sets the <see cref="DocumentedStudent.IsHappy"/> property.
    /// </fluentSummary>
    /// <fluentParam method="WhoIsHappy" name="isHappy">Indicates whether the student is happy.</fluentParam>
    /// <fluentReturns method="WhoIsHappy">A builder for setting the student's friends.</fluentReturns>
    ///
    /// <fluentSummary method="WhoIsSad">
    /// Sets the <see cref="DocumentedStudent.IsHappy"/> property to false.
    /// </fluentSummary>
    /// <fluentReturns method="WhoIsSad">A builder for setting the student's friends.</fluentReturns>
    ///
    /// <fluentSummary method="WithUnknownMood">
    /// Sets the <see cref="DocumentedStudent.IsHappy"/> property to null.
    /// </fluentSummary>
    /// <fluentReturns method="WithUnknownMood">A builder for setting the student's friends.</fluentReturns>
    [FluentPredicate(4, "WhoIsHappy", "WhoIsSad")]
    [FluentNullable("WithUnknownMood")]
    public bool? IsHappy { get; private set; }

    /// <fluentSummary method="WhoseFriendsAre">
    /// Sets the student's friends.
    /// </fluentSummary>
    /// <fluentParam method="WhoseFriendsAre" name="friends">The student's friends.</fluentParam>
    /// <fluentReturns method="WhoseFriendsAre">The <see cref="DocumentedStudent"/>.</fluentReturns>
    ///
    /// <fluentSummary method="WhoseFriendIs">
    /// Sets a single friend.
    /// </fluentSummary>
    /// <fluentParam method="WhoseFriendIs" name="friend">The student's friend.</fluentParam>
    /// <fluentReturns method="WhoseFriendIs">The <see cref="DocumentedStudent"/>.</fluentReturns>
    ///
    /// <fluentSummary method="WhoHasNoFriends">
    /// Sets the student's friends to an empty collection.
    /// </fluentSummary>
    /// <fluentReturns method="WhoHasNoFriends">The <see cref="DocumentedStudent"/>.</fluentReturns>
    [FluentCollection(5, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
    public IReadOnlyCollection<string> Friends { get; private set; }
}