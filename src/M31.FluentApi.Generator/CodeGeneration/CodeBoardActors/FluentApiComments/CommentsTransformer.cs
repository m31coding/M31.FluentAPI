using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.FluentApiComments;

internal static class CommentsTransformer
{
    internal static Comments TransformComments(Comments comments)
    {
        return new Comments(comments.List.Select(TransformComment).OfType<Comment>().ToArray());
    }

    internal static Comment? TransformComment(Comment comment)
    {
        string? transformedTag = TransformTag(comment.Tag);
        if (transformedTag == null)
        {
            return null;
        }

        IReadOnlyList<CommentAttribute> transformedAttributes = TransformAttributes(comment.Attributes);
        return new Comment(transformedTag, transformedAttributes, comment.Content);
    }

    private static string? TransformTag(string tag)
    {
        if (!tag.StartsWith("fluent"))
        {
            return null;
        }

        return tag.Substring("fluent".Length).FirstCharToLower();
    }

    private static IReadOnlyList<CommentAttribute> TransformAttributes(IReadOnlyList<CommentAttribute> attributes)
    {
        CommentAttribute? firstMethodAttribute = attributes.FirstOrDefault(a => a.Key == "method");
        return firstMethodAttribute == null ? attributes : attributes.Where(a => a != firstMethodAttribute).ToArray();
    }
}