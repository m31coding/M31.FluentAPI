using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class FluentApiInfoGroup
{
    internal FluentApiInfoGroup(
        int attributeInfoBuilderStep,
        string fluentMethodName,
        Type attributeInfoType,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos)
    {
        AttributeInfoBuilderStep = attributeInfoBuilderStep;
        FluentMethodName = fluentMethodName;
        AttributeInfoType = attributeInfoType;
        FluentApiInfos = fluentApiInfos;
    }

    internal int AttributeInfoBuilderStep { get; }
    internal string FluentMethodName { get; }
    internal Type AttributeInfoType { get; }
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }

    internal bool IsCompoundGroup => FluentApiInfos.Count > 1 &&
                                     AttributeInfoType == typeof(FluentMemberAttributeInfo);

    internal bool IsOverloadedMethodGroup => FluentApiInfos.Count > 1 &&
                                             AttributeInfoType == typeof(FluentMethodAttributeInfo);

    internal static IReadOnlyCollection<FluentApiInfoGroup> CreateGroups(IReadOnlyCollection<FluentApiInfo> infos)
    {
        IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo>[] grouping = infos
            .GroupBy(i => (i.AttributeInfo.BuilderStep, i.FluentMethodName, i.AttributeInfo.GetType()))
            .ToArray();

        return grouping.Select(CreateInfoGroup).ToArray();

        FluentApiInfoGroup CreateInfoGroup(
            IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo> group)
        {
            return new FluentApiInfoGroup(group.Key.builderStep, group.Key.fluentMethodName, group.Key.type,
                group.ToArray());
        }
    }
}