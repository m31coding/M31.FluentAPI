using System;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using Xunit;

namespace M31.FluentApi.Tests.Components.FluentApiComments;

public class CommentGetLineTests
{
    [Fact]
    public void CanGetLineOfSimpleComment()
    {
        Comment comment = new Comment("fluentSummary", Array.Empty<CommentAttribute>(), "Sets Property1.");
        string line = comment.GetLine();
        Assert.Equal("/// <fluentSummary>Sets Property1.</fluentSummary>", line);
    }

    [Fact]
    public void CanGetLineOfCommentWithAttributes()
    {
        CommentAttribute[] attributes = new CommentAttribute[]
        {
            new CommentAttribute("method", "method1"),
            new CommentAttribute("name", "parameter1")
        };
        Comment comment = new Comment("fluentParam", attributes, "The parameter1.");
        string line = comment.GetLine();
        Assert.Equal(@"/// <fluentParam method=""method1"" name=""parameter1"">The parameter1.</fluentParam>", line);
    }
}