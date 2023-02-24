using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.PredicateClass;

[FluentApi]
public class Student
{
    [FluentPredicate(0, "WhoIsHappy", "WhoIsSad")]
    public bool IsHappy { get; set; }
}