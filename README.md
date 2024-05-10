# Fluent APIs in C#

![M31.FluentApi logo](media/fluent-api-logo-256.jpg)

Everybody wants to use fluent APIs but writing them is tedious. With this library providing fluent APIs for your classes becomes a breeze. Simply annotate them with attributes and the source code for the fluent API will be generated. The fluent API library leverages incremental source code generation at development time and your IDE will offer you the corresponding code completion immediately.

The generated code follows the builder design pattern and allows you to construct objects step by step. This approach avoids big constructors and results in very readable code. 

[![license](https://img.shields.io/badge/license-MIT-brightgreen)](https://github.com/m31coding/M31.BinarySearchTrees/blob/master/LICENSE)
[![.net version](https://img.shields.io/badge/.NET-6.0-6D429C)](https://dotnet.microsoft.com/en-us/)
[![version](https://img.shields.io/nuget/v/M31.FluentApi)](https://www.nuget.org/packages/M31.FluentApi/)
[![CI](https://github.com/m31coding/M31.FluentAPI/actions/workflows/ci.yml/badge.svg)](https://github.com/m31coding/M31.FluentAPI/actions/workflows/ci.yml)
[![m31coding](https://img.shields.io/badge/www-m31coding.com-34345B)](https://www.m31coding.com)

Accompanying blog post: [www.m31coding.com>blog>fluent-api](https://www.m31coding.com/blog/fluent-api.html)

## Installing via NuGet

Install the latest version of the package `M31.FluentApi` via your IDE or use the package manager console:

```
PM> Install-Package M31.FluentApi
```

A package reference will be added to your `csproj` file. Moreover, since this library provides code via source code generation, consumers of your project don't need the reference to `M31.FluentApi`. Therefore, it is recommended to use the `PrivateAssets` metadata tag:

```xml
<PackageReference Include="M31.FluentApi" Version="1.2.0" PrivateAssets="all"/>
```

If you would like to examine the generated code, you may emit it by adding the following lines to your `csproj` file:

```xml
<PropertyGroup>
    <CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)Generated</CompilerGeneratedFilesOutputPath>
    <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
</PropertyGroup>
```

The code can then be found in the `obj/Generated` folder.

## Usage

If you use this library for the first time I recommend that you read the storybook: 

- [01 Basics](src/M31.FluentApi.Storybook/01_Basics.cs)
- [02 Control attributes](src/M31.FluentApi.Storybook/02_ControlAttributes.cs)


Here is the full example from the introduction to the basics:

```cs
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
```

![fluent-api-usage](media/fluent-api.gif)

You may have a look at the generated code for this example: [CreateStudent.g.cs](src/M31.FluentApi.Tests/CodeGeneration/TestClasses/StudentClass/CreateStudent.g.cs). Note that if you use private members or properties with a private set accessor, as it is the case in this example, the generated code will use reflection to set the properties.

## Attributes

The attributes `FluentApi` and `FluentMember` are all you need in order to get started. 

The attributes `FluentPredicate` and `FluentCollection` can be used instead of a `FluentMember` attribute if the decorated member is a boolean or a collection, respectively. 

`FluentDefault` and `FluentNullable` can be used in combination with these attributes to set a default value or null, respectively. 

The `FluentMethod` attribute is used for custom builder method implementations.

The control attribute `FluentContinueWith` indicates a jump to the specified builder step, and `FluentBreak` stops the builder.


### FluentApi

```cs
FluentApi(string builderClassName = "Create{Name}")
```
Use this attribute for your class / struct / record. The optional parameter allows you to specify the name of the builder class that will be generated. Within the argument the template `{Name}` can be used, which will be replaced by the name of your decorated type.

```cs
[FluentApi]
public class Student
```

```cs
Student alice = CreateStudent...
```

### FluentMember

```cs
FluentMember(int builderStep, string method = "With{Name}", int parameterPosition = 0)
```

Use this attribute for fields and properties of your class. They can be private but properties must have a set accessor. The `builderStep` parameter specifies the step in which the member can be set. With the `method` parameter you can specify the name of the builder method.

```cs
[FluentMember(0)]
public string FirstName { get; private set; }
```

```cs
...WithFirstName("Alice")...
```

If two `FluentMember` attributes with the same builder step and equal method names are specified, a compound method will be created, which is a builder method that sets multiple properties at once. For compounds the position of the parameters can be controlled by the parameter `parameterPosition`.

```cs
[FluentMember(0, "Named", 0)]
public string FirstName { get; private set; }

[FluentMember(0, "Named", 1)]
public string LastName { get; private set; }
```

```cs
...Named("Alice", "King")...
```


### FluentPredicate

```cs
FluentPredicate(int builderStep, string method = "{Name}", string negatedMethod = "Not{Name}")
```

Can be used instead of a `FluentMember` attribute if the decorated member is of type `bool`. This attribute generates three methods, one for setting the value of the member to `true`, one for setting it to `false`, and one for passing the boolean value.

```cs
[FluentPredicate(4, "WhoIsHappy", "WhoIsSad")]
public bool IsHappy { get; private set; }
```

```cs
...WhoIsHappy()...
...WhoIsSad()...
...WhoIsHappy(true)...
```


### FluentCollection

```cs
FluentCollection(
    int builderStep,
    string singularName,
    string withItems = "With{Name}",
    string withItem = "With{SingularName}",
    string withZeroItems = "WithZero{Name}")
```

Can be used instead of a `FluentMember` attribute if the decorated member is a collection. This attribute generates methods for setting multiple items, one item and zero items. The supported collection types can be seen in the source file [CollectionInference.cs](src/M31.FluentApi.Generator/SourceGenerators/Collections/CollectionInference.cs). 

```cs
[FluentCollection(5, "Friend", "WhoseFriendsAre", "WhoseFriendIs", "WhoHasNoFriends")]
public IReadOnlyCollection<string> Friends { get; private set; }
```

```cs
....WhoseFriendsAre(new string[] { "Bob", "Carol", "Eve" })...
...WhoseFriendsAre("Bob", "Carol", "Eve")...
...WhoseFriendIs("Alice")...
...WhoHasNoFriends()...
```


### FluentDefault

```cs
FluentDefault(string method = "WithDefault{Name}")
```

Can be used for fields and properties in addition to other attributes. When the generated builder method is called the member will keep its initial value.

```cs
[FluentMember(3, "LivingIn")]
[FluentDefault("LivingInBoston")]
[FluentNullable("InUnknownCity")]
public string? City { get; private set; } = "Boston";
```

```cs
...LivingInBoston()... // City is not changed
```


### FluentNullable

```cs
FluentNullable(string method = "Without{Name}")
```

Can be used for fields and properties in addition to other attributes. Generates a builder method that sets the member to `null`.

```cs
[FluentMember(3, "LivingIn")]
[FluentDefault("LivingInBoston")]
[FluentNullable("InUnknownCity")]
public string? City { get; private set; } = "Boston";
```

```cs
...InUnknownCity()... // City is set to null
```


### FluentMethod

```cs
FluentMethod(int builderStep, string method = "{Name}")
```

Use this attribute on methods to provide a custom implementation for setting values or triggering additional behavior. The decorated method must return `void`.

```cs
[FluentMethod(1)]
private void BornOn(DateOnly dateOfBirth)
{
    DateOnly today = DateOnly.FromDateTime(DateTime.Today);
    int age = today.Year - dateOfBirth.Year;
    if (dateOfBirth > today.AddYears(-age)) age--;
    Age = age;
}
```

```cs
...BornOn(new DateOnly(2003, 6, 24))...
```


### FluentContinueWith

```cs
FluentContinueWith(int builderStep)
```

Can be used at all steps on fields, properties, and methods to jump to a specific builder step. Useful for skipping steps and branching. May be used to create optional builder methods:

```cs
[FluentMember(0)]
public string FirstName { get; private set; }

[FluentMember(1)]
[FluentContinueWith(1)]
public string? MiddleName { get; private set; }

[FluentMember(1)]
public string LastName { get; private set; }
```

```cs
...WithFirstName("Bob").WithLastName("Bishop")...
...WithFirstName("Alice").WithMiddleName("Sophia").WithLastName("King")...
```


### FluentBreak

```cs
FluentBreak()
```

Can be used at all steps on fields, properties, and methods to stop the builder. Only relevant for non-linear APIs that make use of `FluentContinueWith`.

```cs
[FluentMethod(1)]
[FluentBreak]
private void WhoseAddressIsUnknown()
{
}
```

```cs
...WhoseAddressIsUnknown();
```

### Forks

To create forks specify builder methods at the same builder step. The resulting API offers all specified methods at this step but only one can be called:

```cs
[FluentMember(1, "OfAge")]
public int Age { get; private set; }

[FluentMethod(1)]
private void BornOn(DateOnldateOfBirth)
{
    DateOnly today = DateOnlFromDateTime(DateTime.Today);
    int age = today.Year dateOfBirth.Year;
    if (dateOfBirth > today.AddYea(-age)) age--;
    Age = age;
}
```

```cs
...OfAge(22)...
...BornOn(new DateOnly(2002, 8, 3))...
```


## Problems with the IDE

As of 2023 code generation with Roslyn is still a relatively new feature but is already supported quite well in Visual Studio and Rider. Since code generation is potentially triggered with every single key stroke, there are sometimes situations where the code completion index of the IDE does not keep up with all the changes.

In particular, if your IDE visually indicates that there are errors in your code but it compiles and runs just fine, try the following things:

- Rebuild the project or the whole solution
- Unload and reload the project
- Close and reopen the IDE
- Remove the .vs folder (Visual Studio) or the .idea folder (Rider)

## Support and Contribution

This library is free. If you find it valuable and wish to express your support, please leave a star. You are kindly invited to contribute. If you see the possibility for enhancement, please create a GitHub issue and you will receive timely feedback.

Happy coding!