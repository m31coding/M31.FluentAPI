using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class BuilderAndTargetInfo
{
    internal BuilderAndTargetInfo(
        string fluentApiClassName,
        string? @namespace,
        GenericsInfo? genericsInfo,
        bool fluentApiTypeIsStruct,
        bool fluentApiTypeIsInternal,
        bool fluentApiTypeHasPrivateConstructor,
        string builderClassName)
    {
        Namespace = @namespace;
        FluentApiClassName = fluentApiClassName;
        FluentApiClassNameWithTypeParameters = WithTypeParameters(fluentApiClassName, genericsInfo);
        GenericsInfo = genericsInfo;
        FluentApiTypeIsStruct = fluentApiTypeIsStruct;
        FluentApiTypeIsInternal = fluentApiTypeIsInternal;
        FluentApiTypeHasPrivateConstructor = fluentApiTypeHasPrivateConstructor;
        BuilderClassName = builderClassName;
        BuilderClassNameWithTypeParameters = WithTypeParameters(builderClassName, genericsInfo);
        BuilderInstanceName = builderClassName.FirstCharToLower();
        ClassInstanceName = fluentApiClassName.FirstCharToLower();
    }

    internal string? Namespace { get; }
    internal string FluentApiClassName { get; }
    internal string FluentApiClassNameWithTypeParameters { get; }
    internal GenericsInfo? GenericsInfo { get; }
    internal bool FluentApiTypeIsStruct { get; }
    internal bool FluentApiTypeIsInternal { get; }
    internal bool FluentApiTypeHasPrivateConstructor { get; }
    internal string BuilderClassName { get; }
    internal string BuilderClassNameWithTypeParameters { get; }
    internal string BuilderInstanceName { get; }
    internal string ClassInstanceName { get; }

    private static string WithTypeParameters(string typeName, GenericsInfo? genericsInfo)
    {
        if (genericsInfo == null || genericsInfo.Parameters.Count == 0)
        {
            return typeName;
        }

        return $"{typeName}<{string.Join(", ", genericsInfo.Parameters)}>";
    }
}