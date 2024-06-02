namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiInfoGroup
{
    internal FluentApiInfoGroup(
        int builderStep,
        int? nextBuilderStep,
        bool isSkippable,
        string fluentMethodName,
        Type attributeInfoType,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos)
    {
        BuilderStep = builderStep;
        NextBuilderStep = nextBuilderStep;
        IsSkippable = isSkippable;
        FluentMethodName = fluentMethodName;
        AttributeInfoType = attributeInfoType;
        FluentApiInfos = fluentApiInfos;
    }

    internal int BuilderStep { get; }
    internal int? NextBuilderStep { get; }
    internal bool IsSkippable { get; }
    internal string FluentMethodName { get; }
    internal Type AttributeInfoType { get; }
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }
    internal bool IsCompoundGroup => FluentApiInfos.Count > 1;
}