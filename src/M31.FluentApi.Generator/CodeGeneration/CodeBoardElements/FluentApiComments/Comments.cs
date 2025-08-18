using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;

internal class Comments
{
    internal Comments(IReadOnlyList<Comment> comments)
    {
        List = comments;
    }

    public IReadOnlyList<Comment> List { get; }
    internal bool Any => List.Count > 0;

    internal List<string> GetLines()
    {
        return List.Select(c => c.GetLine()).ToList();
    }

    protected bool Equals(Comments other)
    {
        return List.SequenceEqual(other.List);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Comments)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode().AddSequence(List);
    }
}