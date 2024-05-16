// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithAfterCompoundClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    [FluentContinueWith(2)]
    public string FirstName { get; private set; }

    [FluentMember(0, "WithName")]
    [FluentContinueWith(2)]
    public string LastName { get; private set; }

    [FluentMember(1)]
    public string Property1 { get; private set; }

    [FluentMember(2)]
    public string Property2 { get; private set; }
}