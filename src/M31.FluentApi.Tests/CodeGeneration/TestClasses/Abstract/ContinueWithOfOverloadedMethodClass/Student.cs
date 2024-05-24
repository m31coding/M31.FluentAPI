// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.ContinueWithOfOverloadedMethodClass;

[FluentApi]
public class Student
{
    [FluentMethod(0)]
    [FluentContinueWith(1)]
    public void Method1()
    {
    }

    [FluentMethod(0)]
    [FluentContinueWith(2)]
    public void Method1(int p1)
    {
    }

    [FluentMember(1)]
    public string Property1 { get; private set; }

    [FluentMember(2)]
    public string Property2 { get; private set; }
}