using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class BuilderAndTargetInfo
{
    internal BuilderAndTargetInfo(
        string fluentApiClassName,
        string? @namespace,
        bool fluentApiTypeIsStruct,
        bool fluentApiTypeIsInternal,
        bool fluentApiTypeHasPrivateConstructor,
        string builderClassName)
    {
        Namespace = @namespace;
        FluentApiClassName = fluentApiClassName;
        FluentApiTypeIsStruct = fluentApiTypeIsStruct;
        FluentApiTypeIsInternal = fluentApiTypeIsInternal;
        FluentApiTypeHasPrivateConstructor = fluentApiTypeHasPrivateConstructor;
        BuilderClassName = builderClassName;
        BuilderInstanceName = builderClassName.FirstCharToLower();
        ClassInstanceName = fluentApiClassName.FirstCharToLower();
    }

    internal string? Namespace { get; }
    internal string FluentApiClassName { get; }
    internal bool FluentApiTypeIsStruct { get; }
    internal bool FluentApiTypeIsInternal { get; }
    internal bool FluentApiTypeHasPrivateConstructor { get; }
    internal string BuilderClassName { get; }
    internal string BuilderInstanceName { get; }
    internal string ClassInstanceName { get; }
}