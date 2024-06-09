// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentNullableNoNullableAnnotationClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithName")]
    [FluentNullable("WhoseNameIsUnknown")]
    public string Name { get; set; }
}