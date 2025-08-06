using System.Text.Json;
using ExampleProject;

#pragma warning disable CS8321 // Local function is declared but never used

// Student
//

Student student1 = CreateStudent.Named("Alice", "King").OfAge(22).WhoStartsUniversity()
    .LivingIn("New York").WhoIsHappy().WhoseFriendsAre("Bob", "Carol", "Eve");
Student student2 = CreateStudent.Named("Bob", "Bishop").BornOn(new DateOnly(2002, 8, 3)).InSemester(2)
    .LivingInBoston().WithUnknownMood().WhoseFriendIs("Alice");

Console.WriteLine(JsonSerializer.Serialize(student1));
Console.WriteLine(JsonSerializer.Serialize(student2));

// ExchangeStudent (inherited from Student)
//

ExchangeStudent exchangeStudent = CreateExchangeStudent.Named("Bob", "Bishop").BornOn(new DateOnly(2002, 8, 3))
    .InSemester(2).LivingInBoston().WithUnknownMood().WhoseFriendIs("Alice").WithHomeCountry("United States");

Console.WriteLine(JsonSerializer.Serialize(exchangeStudent));

// Person
//

Person person1 = CreatePerson.WithFirstName("Alice").WithMiddleName("Sophia").WithLastName("King").WhoLivesAtAddress()
    .WithHouseNumber("23").WithStreet("Market Street").InCity("San Francisco");
Person person2 = CreatePerson.WithFirstName("Bob").WithLastName("Bishop").WhoseAddressIsUnknown();
Person person3 = CreatePerson.WithFirstName("Eve").WithLastName("Knight").WhoIsADigitalNomad().LivingInCity("Berlin");

Console.WriteLine(JsonSerializer.Serialize(person1));
Console.WriteLine(JsonSerializer.Serialize(person2));
Console.WriteLine(JsonSerializer.Serialize(person3));

// Order (forced steps)
//
// Example from https://youtu.be/qCIr30WxJQw?si=FRALafrpA1zWACA8.
//

Order order = CreateOrder
    .WithNumber(10)
    .CreatedOn(DateTime.UtcNow)
    .ShippedTo(a => a
        .Street("street")
        .City("city")
        .Zip("zip")
        .DefaultState()
        .Country("country"));

Console.WriteLine(JsonSerializer.Serialize(order));

// Order (arbitrary steps)
//
// Example from https://youtu.be/qCIr30WxJQw?si=FRALafrpA1zWACA8.
//

Order2 order2 = CreateOrder2
    .CreatedOn(DateTime.UtcNow)
    .ShippedTo(a => a
        .Country("country")
        .Street("street")
        .Zip("zip")
        .City("city")
        .Build())
    .WithNumber(10)
    .Build();

Console.WriteLine(JsonSerializer.Serialize(order2));

// HashCode
//

int hashCode = CreateHashCode
    .Add(42).Add(3.14).AddSequence(new[] { 1, 2, 3 }).Add("Hello world").Value();

Console.WriteLine(hashCode);

// Docker file
//
// Example from https://mitesh1612.github.io/blog/2021/08/11/how-to-design-fluent-api.
//

string dockerFile = CreateDockerFile
    .FromImage("node:12")
    .CopyFiles("package*.json", "./")
    .RunCommand("npm install")
    .WithEnvironmentVariable("PORT", "8080")
    .ExposePort(8080)
    .WithCommand("npm start")
    .ToString();

Console.WriteLine(dockerFile);

// Employee
//
// Example from https://stackoverflow.com/questions/59021513/using-fluent-interface-with-builder-pattern.
//

Employee employee = CreateEmployee
    .WithName("My Name")
    .WithPhone(
        p => p.WithNumber("222-222-2222").WithUsage("CELL"))
    .WithJobs(
        j => j.WithCompanyName("First Company").WithSalary(100),
        j => j.WithCompanyName("Second Company").WithSalary(200));

Console.WriteLine(JsonSerializer.Serialize(employee));

// HttpRequest
//
// Example from https://github.com/dotnet/csharplang/discussions/7325.
//

HttpRequestMessage message = CreateHttpRequest
    .WithMethod(HttpMethod.Post)
    .WithUrl("https://example.com")
    .WithHeaders(("Accept", "application/json"), ("Authorization", "Bearer x"))
    .WithJsonContent(
        new { Name = "X", Quantity = 10 },
        opt => opt.PropertyNameCaseInsensitive = true)
    .GetMessage();

Console.WriteLine(JsonSerializer.Serialize(message));

// await RunAsyncExample();
static async Task RunAsyncExample()
{
    HttpClient client = new HttpClient();
    HttpResponseMessage response = await CreateHttpRequest
        .WithMethod(HttpMethod.Get)
        .WithUrl("https://www.m31coding.com")
        .WithZeroHeaders()
        .WithoutContent()
        .SendAsync(client);

    Console.WriteLine(response.StatusCode);
}

// Node
//

Node<int> tree = CreateTree<int>.Root(8)
    .Left(3, n => n
        .Left(1)
        .Right(6))
    .Right(10, n => n
        .LeftNull()
        .Right(14));

Console.WriteLine(JsonSerializer.Serialize(tree));