// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PartialClass;

public partial class Student
{
    [FluentMember(1)]
    public string LastName { get; private set; }
}