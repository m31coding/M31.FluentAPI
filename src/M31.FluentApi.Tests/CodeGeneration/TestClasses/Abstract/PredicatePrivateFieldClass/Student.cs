// Field is never used
#pragma warning disable CS0169
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PredicatePrivateFieldClass;

[FluentApi]
public class Student
{
    [FluentPredicate(0, "WhoIsHappy", "WhoIsSad")]
    private bool isHappy;
}