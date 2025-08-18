// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

using System.Collections.Generic;
using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.FluentApiComments.
    CommentedLambdaCollectionClass;

[FluentApi]
public class Student
{
    [FluentMember(0)]
    public string Name { get; set; }

    //// <fluentSummary method="WithPhoneNumbers">
    //// Sets the student's phone numbers.
    //// </fluentSummary>
    //// <fluentParam method="WithPhoneNumbers" name="phoneNumbers">The student's phone numbers.</fluentParam>
    //// <fluentParam method="WithPhoneNumbers" name="createPhoneNumbers">Functions for creating the student's phone numbers.</fluentParam>
    ////
    //// <fluentSummary method="WithPhoneNumber">
    //// Sets the student's phone number.
    //// </fluentSummary>
    //// <fluentParam method="WithPhoneNumber" name="phoneNumber">The student's phone number.</fluentParam>
    //// <fluentParam method="WithPhoneNumber" name="createPhoneNumber">A function for creating the student's phone number.</fluentParam>
    ////
    //// <fluentSummary method="WithZeroPhoneNumbers">
    //// Specifies that the student has no phone numbers.
    //// </fluentSummary>
    [FluentCollection(1, "PhoneNumber")]
    public IReadOnlyCollection<Phone> PhoneNumbers { get; set; }
}