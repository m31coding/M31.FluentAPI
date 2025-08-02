// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable all

using M31.FluentApi.Attributes;

namespace M31.FluentApi.Tests.CodeGeneration.TestClasses.Abstract.DocumentationComments.
    CommentedLambdaCollectionClass;

[FluentApi]
public class Phone
{
    [FluentMember(0)]
    public string Number { get; set; }

    [FluentMember(1)]
    public string Usage { get; set; }
}