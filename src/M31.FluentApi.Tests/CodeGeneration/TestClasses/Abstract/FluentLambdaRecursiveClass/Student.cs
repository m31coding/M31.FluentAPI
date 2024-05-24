// Non-nullable member is uninitialized
#pragma warning disable CS8618

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentLambdaRecursiveClass;

[FluentApi]
public class Student
{
    [FluentLambda(0)]
    [FluentNullable]
    public Student? Friend { get; set; }
}