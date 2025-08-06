using System;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.FluentApiComments;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using Xunit;

namespace M31.FluentApi.Tests.Components.FluentApiComments;

public class CommentsTransformerTests
{
    [Fact]
    public void CanTransformSimpleFluentComment()
    {
        Comment comment = new Comment("fluentSummary", Array.Empty<CommentAttribute>(), "Sets Property1.");
        Comment? transformed = CommentsTransformer.TransformComment(comment);
        Assert.NotNull(transformed);
        Assert.Equal("/// <summary>Sets Property1.</summary>", transformed!.GetLine());
    }

    [Fact]
    public void NonFluentCommentsAreNotTransformed()
    {
        Comment comment = new Comment("Summary", Array.Empty<CommentAttribute>(), "Sets Property1.");
        Comment? transformed = CommentsTransformer.TransformComment(comment);
        Assert.Null(transformed);
    }

    [Fact]
    public void CanTransformFluentCommentWithAttribute()
    {
        CommentAttribute[] attributes = new CommentAttribute[]
        {
            new CommentAttribute("name", "property1")
        };
        Comment comment = new Comment("fluentParam", attributes, "The new value of Property1.");
        Comment? transformed = CommentsTransformer.TransformComment(comment);
        Assert.NotNull(transformed);
        Assert.Equal(@"/// <param name=""property1"">The new value of Property1.</param>", transformed!.GetLine());
    }

    [Fact]
    public void MethodAttributeIsRemoved()
    {
        CommentAttribute[] attributes = new CommentAttribute[]
        {
            new CommentAttribute("method", "WithProperty1"),
            new CommentAttribute("name", "property1")
        };
        Comment comment = new Comment("fluentParam", attributes, "The new value of Property1.");
        Comment? transformed = CommentsTransformer.TransformComment(comment);
        Assert.NotNull(transformed);
        Assert.Equal(@"/// <param name=""property1"">The new value of Property1.</param>", transformed!.GetLine());
    }

    [Fact]
    public void FurtherMethodAttributesAreRetained()
    {
        CommentAttribute[] attributes = new CommentAttribute[]
        {
            new CommentAttribute("method", "WithProperty1"),
            new CommentAttribute("method", "static"), // Attribute unrelated to the FluentAPI.
            new CommentAttribute("name", "property1")
        };
        Comment comment = new Comment("fluentParam", attributes, "The new value of Property1.");
        Comment? transformed = CommentsTransformer.TransformComment(comment);
        Assert.NotNull(transformed);
        Assert.Equal(@"/// <param method=""static"" name=""property1"">The new value of Property1.</param>",
            transformed!.GetLine());
    }
}