// Non-nullable member is uninitialized
#pragma warning disable CS8618

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.DuplicateMethodClass2;

[FluentApi]
public class Student
{
    [FluentMember(0, "WithFriend")]
    public string Friend { get; set; }

    [FluentCollection(0, "Friend")]
    public IReadOnlyCollection<string> Friends { get; set; }
}