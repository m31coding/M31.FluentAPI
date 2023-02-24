// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TwoParameterCompoundClassReversedParameters;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName", 1)]
    public string FirstName { get; set; }

    [FluentMember(0, "WithName", 0)]
    public string LastName { get; set; }
}