// Non-nullable member is uninitialized
#pragma warning disable CS8618

// ReSharper disable CheckNamespace

using M31.FluentApi.Attributes;

BasicExample.Usage.UseTheGeneratedFluentApi();
CustomNamesExample.Usage.UseTheGeneratedFluentApi();
CompoundExample.Usage.UseTheGeneratedFluentApi();
ForkExample.Usage.UseTheGeneratedFluentApi();
SpecialMembersExample.Usage.UseTheGeneratedFluentApi();
OrthogonalAttributesExample.Usage.UseTheGeneratedFluentApi();
FluentMethodExample.Usage.UseTheGeneratedFluentApi();
FullExample.Usage.UseTheGeneratedFluentApi();

namespace BasicExample
{
    /* Generates a builder class with name CreateStudent and methods WithFirstName and WithLastName.
       The methods can only be accessed in the specified order, WithFirstName (builder step 0) has to be called before
       WithLastName (builder step 1).
       Although I use classes with properties in all examples of this file, the FluentApi attribute can also be applied
       to structs and records and the FluentMember attribute also works with fields. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0)]
        public string FirstName { get; private set; }

        [FluentMember(1)]
        public string LastName { get; private set; }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student = CreateStudent.WithFirstName("Alice").WithLastName("King");
        }
    }
}

namespace CustomNamesExample
{
    /* The desired name of the builder class and its methods can be specified in the attributes.
       For these name parameters you may use the template {Name}, which will be replaced by the name of the decorated
       element.
       This approach renders your attributes more robust against name refactorings of the decorated
       elements, however, this comes with the cost of less readability of the attributes. In this file I opted for a
       greater readability and use this feature only in this example and only for the FluentApiAttribute. */

    [FluentApi("New{Name}")]
    public class Student
    {
        [FluentMember(0, "WhoseFirstNameIs")]
        public string FirstName { get; private set; }

        [FluentMember(1, "WhoseLastNameIs")]
        public string LastName { get; private set; }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student = NewStudent.WhoseFirstNameIs("Alice").WhoseLastNameIs("King");
        }
    }
}

namespace CompoundExample
{
    /* Multiple properties can be combined into a compound builder method by specifying the same builder step and the
       same method name. The last parameter of the FluentMember attribute specifies the parameter position for the
       generated builder method. This attribute parameter is only relevant when specifying compounds. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0, "Named", 0)]
        public string FirstName { get; private set; }

        [FluentMember(0, "Named", 1)]
        public string LastName { get; private set; }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student = CreateStudent.Named("Alice", "King");
        }
    }
}

namespace ForkExample
{
    /* You can create a fork by specifying FluentMember attributes with the same builder step but different names. In
       this example a compound method called Named is created and a fork with the methods OfAge and BornOn. After the
       Named method your IDE will offer you the OfAge method and the BornOn method but you can only choose one. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0, "Named", 0)]
        public string FirstName { get; private set; }

        [FluentMember(0, "Named", 1)]
        public string LastName { get; private set; }

        [FluentMember(1, "OfAge")]
        public int Age { get; private set; }

        [FluentMember(1, "BornOn")]
        public DateOnly DateOfBirth
        {
            set => Age = ComputeAge(value);
        }

        private static int ComputeAge(DateOnly dateOfBirth)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-age)) age--;
            return age;
        }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student1 = CreateStudent.Named("Alice", "King").BornOn(new DateOnly(2003, 6, 24));
            Student student2 = CreateStudent.Named("Bob", "Bishop").OfAge(22);
        }
    }
}

namespace SpecialMembersExample
{
    /* The FluentMember attribute can be applied to all property and field types. However, for booleans and collections
       you can also use the more specific attributes FluentPredicate and FluentCollection, respectively.
       The FluentPredicate attribute results in two builder methods, one for setting the value to true and one for
       setting it to false.
       The FluentCollection attribute can be used for lists, arrays, sets and related interfaces. It generates methods
       for setting multiple items, one item and zero items. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0)]
        public string Name { get; private set; }

        [FluentPredicate(1, "WhoIsHappy", "WhoIsSad")]
        public bool IsHappy { get; private set; }

        [FluentCollection(3, "Friend")]
        public IReadOnlyCollection<string> Friends { get; private set; }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student1 = CreateStudent.WithName("Alice").WhoIsHappy().WithFriends("Bob", "Carol", "Eve");
            Student student2 = CreateStudent.WithName("Bob").WhoIsHappy()
                .WithFriends(new List<string> { "Alice", "Charlie", "David" });
            Student student3 = CreateStudent.WithName("Eve").WhoIsSad().WithFriend("Alice");
            Student student4 = CreateStudent.WithName("Frank").WhoIsSad().WithZeroFriends();
        }
    }
}

namespace OrthogonalAttributesExample
{
    /* Additional attributes ('orthogonal attributes') can be used in combination with FluentMember, FluentPredicate and
       FluentCollection. These are the attributes FluentDefault and FluentNullable.
       The FluentDefault attribute generates a builder method that does not change the initial value of the field or
       property.
       The FluentNullable attribute generates a builder method that sets the value to null.
       As you can see for the City property, it is possible to use both of them at the same time. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0, "InSemester")]
        [FluentDefault("WhoStartsUniversity")]
        public int Semester { get; private set; } = 0;

        [FluentMember(1, "LivingIn")]
        [FluentDefault("LivingInBoston")]
        [FluentNullable("InUnknownCity")]
        public string? City { get; private set; } = "Boston";

        [FluentCollection(2, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
        [FluentNullable("WhoseFriendsAreUnknown")]
        public IReadOnlyCollection<string>? Friends { get; private set; }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student1 = CreateStudent.WhoStartsUniversity().LivingInBoston().WhoseFriendsAreUnknown();
            Student student2 = CreateStudent.InSemester(2).LivingIn("San Francisco").WhoseFriendIs("Alice");
            Student student3 = CreateStudent.InSemester(5).LivingIn("New York").WhoseFriendsAre("Bob", "Eve");
        }
    }
}

namespace FluentMethodExample
{
    /* The generated builder methods simply set the values of the fields and properties. For public fields and
       properties this is straightforward, for private fields and private set accessors reflection will be used.
       Nevertheless, in both cases the builder methods merely set the values to the given arguments.
       There might be use cases where you want to have more control over the builder methods, e.g. for setting the value
       in a particular way or for triggering additional behavior. To this end you can define custom methods that return
       void and decorate them with FluentMethod attributes. */

    [FluentApi]
    public class Student
    {
        [FluentMember(0)]
        public string Name { get; private set; }

        public IReadOnlyCollection<string> Friends { get; private set; }

        [FluentMethod(1)]
        private void AndFriends(IEnumerable<string> friends)
        {
            Friends = friends.ToHashSet(StringComparer.CurrentCultureIgnoreCase);
        }

        [FluentMethod(1)]
        private void AndFriend(string friend)
        {
            Friends = new string[] { friend };
        }

        [FluentMethod(2)]
        private void GreetFriends()
        {
            Console.WriteLine($"Good to see you {string.Join(", ", Friends)}!");
        }
    }

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student student1 = CreateStudent.WithName("Bob").AndFriend("Alice").GreetFriends();
            Student student2 = CreateStudent.WithName("Carol").AndFriends(new[] { "Charlie", "Eve" }).GreetFriends();
        }
    }
}

namespace FullExample
{
    /* Here is one final example with all the things you have learned in this overview. Note that this time
       I have modeled the BornOn method with a fluent method instead of a property.
       Moreover, the names of all the methods that are generated are specified explicitly. When you use this library I
       would like you to take some time and find good names for the builder methods. In this way I am confident that you
       will get an expressive and readable fluent API.
       Well, that's it. Now you are good to go, happy coding! - Kevin

       PS: If you are interested in the generated code of the examples compile the Storybook application and have a look
       in the obj/Generated folder. You may also have a look at the files in M31.FluentApi.Tests/CodeGeneration/
       TestClasses. */

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

    public static class Usage
    {
        public static void UseTheGeneratedFluentApi()
        {
            Student alice = CreateStudent.Named("Alice", "King").OfAge(22).WhoStartsUniversity()
                .LivingIn("New York").WhoIsHappy().WhoseFriendsAre("Bob", "Carol", "Eve");

            Student bob = CreateStudent.Named("Bob", "Bishop").BornOn(new DateOnly(2002, 8, 3)).InSemester(2)
                .LivingInBoston().WithUnknownMood().WhoseFriendIs("Alice");
        }
    }
}