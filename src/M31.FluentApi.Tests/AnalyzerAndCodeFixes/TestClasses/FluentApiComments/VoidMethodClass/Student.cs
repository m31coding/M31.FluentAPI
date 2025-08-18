// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System;
using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.AnalyzerAndCodeFixes.TestClasses.FluentApiComments.VoidMethodClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; private set; }

    [FluentMethod(1)]
    public void Study()
    {
    }

    [FluentMethod(2)]
    [FluentReturn]
    public void Sleep()
    {
    }
}