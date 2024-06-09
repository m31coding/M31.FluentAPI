// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.InternalPropertyClass;

[FluentApi]
public class Student
{
    [FluentMember(0, "InSemester")]
    internal int Semester { get; set; }
}