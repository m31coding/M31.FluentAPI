using System.Text.Json;
using ExampleProject;

// Student
//

Student student1 = CreateStudent.Named("Alice", "King").OfAge(22).WhoStartsUniversity()
    .LivingIn("New York").WhoIsHappy().WhoseFriendsAre("Bob", "Carol", "Eve");
Student student2 = CreateStudent.Named("Bob", "Bishop").BornOn(new DateOnly(2002, 8, 3)).InSemester(2)
    .LivingInBoston().WithUnknownMood().WhoseFriendIs("Alice");

Console.WriteLine(JsonSerializer.Serialize(student1));
Console.WriteLine(JsonSerializer.Serialize(student2));

// Person
//

Person person1 = CreatePerson.WithFirstName("Alice").WithMiddleName("Sophia").WithLastName("King").WhoLivesAtAddress()
    .WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco");
Person person2 = CreatePerson.WithFirstName("Bob").WithLastName("Bishop").WhoseAddressIsUnknown();
Person person3 = CreatePerson.WithFirstName("Eve").WithLastName("Johnson").WhoIsADigitalNomad().LivingInCity("Berlin");

Console.WriteLine(JsonSerializer.Serialize(person1));
Console.WriteLine(JsonSerializer.Serialize(person2));
Console.WriteLine(JsonSerializer.Serialize(person3));