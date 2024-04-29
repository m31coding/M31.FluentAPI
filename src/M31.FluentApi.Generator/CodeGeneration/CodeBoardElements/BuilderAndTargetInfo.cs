using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class BuilderAndTargetInfo
{
    internal BuilderAndTargetInfo(
        string fluentApiClassName,
        string? @namespace,
        GenericInfo? genericInfo,
        bool fluentApiTypeIsStruct,
        bool fluentApiTypeIsInternal,
        bool fluentApiTypeHasPrivateConstructor,
        string builderClassName)
    {
        Namespace = @namespace;
        FluentApiClassName = fluentApiClassName;
        FluentApiClassNameWithTypeParameters = WithTypeParameters(fluentApiClassName, genericInfo);
        GenericInfo = genericInfo;
        FluentApiTypeIsStruct = fluentApiTypeIsStruct;
        FluentApiTypeIsInternal = fluentApiTypeIsInternal;
        FluentApiTypeHasPrivateConstructor = fluentApiTypeHasPrivateConstructor;
        BuilderClassName = builderClassName;
        BuilderClassNameWithTypeParameters = WithTypeParameters(builderClassName, genericInfo);
        BuilderInstanceName = builderClassName.FirstCharToLower();
        ClassInstanceName = fluentApiClassName.FirstCharToLower();
    }

    internal string? Namespace { get; }
    internal string FluentApiClassName { get; }
    internal string FluentApiClassNameWithTypeParameters { get; }
    internal GenericInfo? GenericInfo { get; }
    internal bool FluentApiTypeIsStruct { get; }
    internal bool FluentApiTypeIsInternal { get; }
    internal bool FluentApiTypeHasPrivateConstructor { get; }
    internal string BuilderClassName { get; }
    internal string BuilderClassNameWithTypeParameters { get; }
    internal string BuilderInstanceName { get; }
    internal string ClassInstanceName { get; }

    private static string WithTypeParameters(string typeName, GenericInfo? genericInfo)
    {
        if (genericInfo == null || genericInfo.Parameters.Count == 0)
        {
            return typeName;
        }

        return $"{typeName}<{string.Join(", ", genericInfo.ParameterStrings)}>";
    }
}