using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DocumentationGeneration;

internal static class CommentsTransformer
{
    internal static Comments TransformComments(Comments comments)
    {
        return new Comments(comments.List.Select(TransformComment).ToArray());
    }

    internal static Comment TransformComment(Comment comment)
    {
        string transformedTag = TransformTag(comment.Tag);
        IReadOnlyList<CommentAttribute> transformedAttributes = TransformAttributes(comment.Attributes);
        return new Comment(transformedTag, transformedAttributes, comment.Content);
    }

    internal static string TransformTag(string tag)
    {
        return tag; // todo
    }

    internal static IReadOnlyList<CommentAttribute> TransformAttributes(IReadOnlyList<CommentAttribute> attributes)
    {
        // todo: write unit test
        CommentAttribute? firstMethodAttribute = attributes.FirstOrDefault(a => a.Key == "method");
        return firstMethodAttribute == null ? attributes : attributes.Where(a => a != firstMethodAttribute).ToArray();
    }
}
