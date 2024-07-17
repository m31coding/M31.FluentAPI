using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
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
        ConstructorInfo fluentApiTypeConstructorInfo,
        string builderClassName)
    {
        Namespace = @namespace;
        FluentApiClassName = fluentApiClassName;
        FluentApiClassNameWithTypeParameters = WithTypeParameters(fluentApiClassName, genericInfo);
        GenericInfo = genericInfo;
        FluentApiTypeIsStruct = fluentApiTypeIsStruct;
        FluentApiTypeIsInternal = fluentApiTypeIsInternal;
        DefaultAccessModifier = fluentApiTypeIsInternal ? "internal" : "public";
        FluentApiTypeConstructorInfo = fluentApiTypeConstructorInfo;
        BuilderClassName = builderClassName;
        BuilderClassNameWithTypeParameters = WithTypeParameters(builderClassName, genericInfo);
        BuilderInstanceName = builderClassName.FirstCharToLower();
        ClassInstanceName = fluentApiClassName.FirstCharToLower();
        InitialStepInterfaceName = $"I{builderClassName}";
    }

    internal string? Namespace { get; }
    internal string FluentApiClassName { get; }
    internal string FluentApiClassNameWithTypeParameters { get; }
    internal GenericInfo? GenericInfo { get; }
    internal bool FluentApiTypeIsStruct { get; }
    internal bool FluentApiTypeIsInternal { get; }
    internal string DefaultAccessModifier { get; }
    internal ConstructorInfo FluentApiTypeConstructorInfo { get; }
    internal string BuilderClassName { get; }
    internal string BuilderClassNameWithTypeParameters { get; }
    internal string BuilderInstanceName { get; }
    internal string ClassInstanceName { get; }
    internal string InitialStepInterfaceName { get; }

    private static string WithTypeParameters(string typeName, GenericInfo? genericInfo)
    {
        string parameterListInAngleBrackets = genericInfo?.ParameterListInAngleBrackets ?? string.Empty;
        return $"{typeName}{parameterListInAngleBrackets}";
    }
}