// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.GenericClass;

[FluentApi]
public class Student<T1, T2>
{
    [FluentMember(0)]
    public T1 Property1 { get; set; }

    [FluentMember(0)]
    public T2 Property2 { get; set; }
}