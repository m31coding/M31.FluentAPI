// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

// Example from https://stackoverflow.com/questions/59021513/using-fluent-interface-with-builder-pattern.

using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class Employee
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentCollection(1, "Phone")]
    public List<Phone> Phones { get; private set; }

    [FluentCollection(2, "Job")]
    public List<Job> Jobs { get; private set; }
}

[FluentApi]
public class Phone
{
    [FluentMember(0)]
    public string Number { get; private set; }

    [FluentMember(1)]
    public string Usage { get; private set; }
}

[FluentApi]
public class Job
{
    [FluentMember(0)]
    public string CompanyName { get; private set; }

    [FluentMember(1)]
    public int Salary { get; private set; }
}