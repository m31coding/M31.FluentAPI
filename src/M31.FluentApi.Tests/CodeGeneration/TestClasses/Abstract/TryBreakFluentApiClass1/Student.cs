// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.TryBreakFluentApiClass1;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    private void SomeMethod(string someMethodMethodInfo)
    {

    }
}