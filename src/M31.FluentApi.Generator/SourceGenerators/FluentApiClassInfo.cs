using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

/// <summary>
/// Represents all the required information for the fluent API class. Used by the incremental source generator.
/// GetHashCode and Equals must be implemented carefully to ensure correct caching. The property
/// <see cref="FluentApiClassAdditionalInfo"/> holds members that are irrelevant or unsuitable for equality checks.
/// </summary>
internal class FluentApiClassInfo : IEquatable<FluentApiClassInfo>
{
    internal FluentApiClassInfo(
        string name,
        string? @namespace,
        GenericInfo? genericInfo,
        bool isStruct,
        bool isInternal,
        bool hasPrivateConstructor,
        string builderClassName,
        string newLineString,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos,
        IReadOnlyCollection<string> usingStatements,
        FluentApiClassAdditionalInfo additionalInfo)
    {
        Name = name;
        Namespace = @namespace;
        GenericInfo = genericInfo;
        IsStruct = isStruct;
        IsInternal = isInternal;
        HasPrivateConstructor = hasPrivateConstructor;
        BuilderClassName = builderClassName;
        NewLineString = newLineString;
        FluentApiInfos = fluentApiInfos;
        UsingStatements = usingStatements;
        AdditionalInfo = additionalInfo;
    }

    internal string Name { get; }
    internal string? Namespace { get; }
    internal GenericInfo? GenericInfo { get; }
    internal bool IsStruct { get; }
    internal bool IsInternal { get; }
    internal bool HasPrivateConstructor { get; }
    internal string BuilderClassName { get; }
    internal string NewLineString { get; }
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }
    internal IReadOnlyCollection<string> UsingStatements { get; }
    internal FluentApiClassAdditionalInfo AdditionalInfo { get; }

    public bool Equals(FluentApiClassInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name &&
               Namespace == other.Namespace &&
               Equals(GenericInfo, other.GenericInfo) &&
               IsStruct == other.IsStruct &&
               IsInternal == other.IsInternal &&
               HasPrivateConstructor == other.HasPrivateConstructor &&
               BuilderClassName == other.BuilderClassName &&
               NewLineString == other.NewLineString &&
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
            .Add(Name, Namespace, GenericInfo)
            .Add(IsStruct, IsInternal, HasPrivateConstructor)
            .Add(BuilderClassName)
            .Add(NewLineString)
            .AddSequence(FluentApiInfos)
            .AddSequence(UsingStatements);
    }
}