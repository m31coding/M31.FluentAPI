// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaSingleStepClass;

[FluentApi]
public class Student
{
    [FluentLambda(0)]
    public Address Address { get; set; }
}