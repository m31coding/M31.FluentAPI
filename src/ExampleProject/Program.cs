using ExampleProject;

Student alice = CreateStudent.Named("Alice", "King").OfAge(22).WhoStartsUniversity()
    .LivingIn("New York").WhoIsHappy().WhoseFriendsAre("Bob", "Carol", "Eve");

Student bob = CreateStudent.Named("Bob", "Bishop").BornOn(new DateOnly(2002, 8, 3)).InSemester(2)
    .LivingInBoston().WithUnknownMood().WhoseFriendIs("Alice");

Console.WriteLine($"Alice: \"Hi {alice.Friends.First()}!\"");
Console.WriteLine($"Bob: \"Hi {bob.Friends.First()}!\"");