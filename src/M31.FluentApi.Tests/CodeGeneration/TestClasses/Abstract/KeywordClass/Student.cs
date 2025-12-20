// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.KeywordClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Operator { get; set; }

    [FluentMember(1, "With{Name}")]
    public string @Class { get; set; }

    [FluentMember(2)]
    public int Void { get; set; }
}