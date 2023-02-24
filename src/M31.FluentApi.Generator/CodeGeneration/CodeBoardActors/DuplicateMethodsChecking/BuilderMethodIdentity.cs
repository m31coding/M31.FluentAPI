using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;

internal class BuilderMethodIdentity : IEquatable<BuilderMethodIdentity>
{
    internal BuilderMethodIdentity(BuilderMethod builderMethod, MethodIdentity methodIdentity)
    {
        BuilderMethod = builderMethod;
        MethodIdentity = methodIdentity;
    }

    internal BuilderMethod BuilderMethod { get; }
    internal MethodIdentity MethodIdentity { get; }

    public bool Equals(BuilderMethodIdentity? other)
    {
        if (other == null)
        {
            return false;
        }

        return MethodIdentity.Equals(other.MethodIdentity);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BuilderMethodIdentity other)
        {
            return false;
        }

        return Equals(other);
    }

    public override int GetHashCode()
    {
        return MethodIdentity.GetHashCode();
    }
}