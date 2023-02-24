using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiClassInfo : IEquatable<FluentApiClassInfo>
{
    internal FluentApiClassInfo(
        string name,
        string? @namespace,
        bool isStruct,
        bool isInternal,
        bool hasPrivateConstructor,
        string builderClassName,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos,
        IReadOnlyCollection<string> usingStatements,
        FluentApiClassAdditionalInfo additionalInfo)
    {
        Name = name;
        Namespace = @namespace;
        IsStruct = isStruct;
        IsInternal = isInternal;
        HasPrivateConstructor = hasPrivateConstructor;
        BuilderClassName = builderClassName;
        FluentApiInfos = fluentApiInfos;
        UsingStatements = usingStatements;
        AdditionalInfo = additionalInfo;
    }

    internal string Name { get; }
    internal string? Namespace { get; }
    internal bool IsStruct { get; }
    internal bool IsInternal { get; }
    internal bool HasPrivateConstructor { get; }
    internal string BuilderClassName { get; }
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }
    internal IReadOnlyCollection<string> UsingStatements { get; }
    internal FluentApiClassAdditionalInfo AdditionalInfo { get; }

    public bool Equals(FluentApiClassInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name &&
               Namespace == other.Namespace &&
               IsStruct == other.IsStruct &&
               IsInternal == other.IsInternal &&
               HasPrivateConstructor == other.HasPrivateConstructor &&
               BuilderClassName == other.BuilderClassName &&
               FluentApiInfos.SequenceEqual(other.FluentApiInfos) &&
               UsingStatements.SequenceEqual(other.UsingStatements);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FluentApiClassInfo)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(Name, Namespace)
            .Add(IsStruct, IsInternal, HasPrivateConstructor)
            .Add(BuilderClassName)
            .AddSequence(FluentApiInfos)
            .AddSequence(UsingStatements);
    }
}