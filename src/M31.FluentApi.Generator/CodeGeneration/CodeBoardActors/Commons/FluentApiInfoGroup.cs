using M31.FluentApi.Generator.Commons;
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
    internal bool IsCompoundGroup => FluentApiInfos.Count > 1;

    internal static IReadOnlyCollection<FluentApiInfoGroup> CreateGroups(IReadOnlyCollection<FluentApiInfo> infos)
    {
        IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo>[] grouping = infos
            .GroupBy(i => (i.AttributeInfo.BuilderStep, i.FluentMethodName, i.AttributeInfo.GetType()))
            .ToArray();

        List<FluentApiInfoGroup> infoGroups = new List<FluentApiInfoGroup>();

        foreach (IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo> group in grouping)
        {
            AddGroups(group);
        }

        return infoGroups;

        void AddGroups(IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo> group)
        {
            var (builderStep, fluentMethodName, type) = group.Key;
            FluentApiInfo[] infoArray = group.ToArray();

            // single fluent API info or compound
            if (infoArray.Length == 1 || group.Key.type == typeof(FluentMemberAttributeInfo))
            {
                infoGroups.Add(new FluentApiInfoGroup(builderStep, fluentMethodName, type, infoArray));
                return;
            }

            if (group.Key.type != typeof(FluentMethodAttributeInfo))
            {
                throw new GenerationException($"Unexpected group type: {group.Key.type.Name}.");
            }

            // method overloads; split into separate groups
            foreach (FluentApiInfo info in group)
            {
                infoGroups.Add(new FluentApiInfoGroup(builderStep, fluentMethodName, type, new[] { info }));
            }
        }
    }
}