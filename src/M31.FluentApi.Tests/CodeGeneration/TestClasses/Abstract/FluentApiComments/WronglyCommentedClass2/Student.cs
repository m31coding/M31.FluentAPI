// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.
    WronglyCommentedClass2;

[FluentApi]
public class Student
{
    ///// <fluentSummary>
    ///// Sets the student's name.
    ///// </fluentSummary>
    ///// <fluentParam name="name">The student's name.</fluentParam>
    [FluentMember(0)]
    public string Name { get; set; }
}