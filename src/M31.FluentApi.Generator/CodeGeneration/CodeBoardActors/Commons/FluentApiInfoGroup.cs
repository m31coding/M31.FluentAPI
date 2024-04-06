using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class FluentApiInfoGroup
{
    internal FluentApiInfoGroup(
        int builderStep,
        int? nextBuilderStep,
        string fluentMethodName,
        Type attributeInfoType,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos)
    {
        BuilderStep = builderStep;
        NextBuilderStep = nextBuilderStep;
        FluentMethodName = fluentMethodName;
        AttributeInfoType = attributeInfoType;
        FluentApiInfos = fluentApiInfos;
    }

    internal int BuilderStep { get; }
    internal int? NextBuilderStep { get; }
    internal string FluentMethodName { get; }
    internal Type AttributeInfoType { get; }
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }
    internal bool IsCompoundGroup => FluentApiInfos.Count > 1;

    internal static IReadOnlyCollection<FluentApiInfoGroup> CreateGroups(IReadOnlyCollection<FluentApiInfo> infos)
    {
        if (infos.Count == 0)
        {
            return Array.Empty<FluentApiInfoGroup>();
        }

        IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo>[] grouping = infos
            .GroupBy(i => (i.AttributeInfo.BuilderStep, i.FluentMethodName, i.AttributeInfo.GetType()))
            .OrderBy(g => g.Key.BuilderStep)
            .ToArray();

        Dictionary<int, int?> stepToNextStep = GetStepToNextStepDictionary(grouping);
        List<FluentApiInfoGroup> infoGroups = new List<FluentApiInfoGroup>();

        foreach (var group in grouping)
        {
            int? defaultNextBuilderStep = stepToNextStep[group.Key.builderStep];
            AddGroups(group, defaultNextBuilderStep);
        }

        return infoGroups;

        void AddGroups(
            IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo> group,
            int? defaultNextBuilderStep)
        {
            var (builderStep, fluentMethodName, type) = group.Key;
            FluentApiInfo[] infoArray = group.ToArray();
            int? nextBuilderStep = GetNextBuilderStep(infoArray, defaultNextBuilderStep);

            // single fluent API info or compound
            if (infoArray.Length == 1 || type == typeof(FluentMemberAttributeInfo))
            {
                infoGroups.Add(new FluentApiInfoGroup(builderStep, nextBuilderStep, fluentMethodName, type, infoArray));
                return;
            }

            if (type != typeof(FluentMethodAttributeInfo))
            {
                throw new GenerationException($"Unexpected group type: {type.Name}.");
            }

            // method overloads; split into separate groups
            foreach (FluentApiInfo info in group)
            {
                infoGroups.Add(new FluentApiInfoGroup(
                    builderStep,
                    nextBuilderStep,
                    fluentMethodName,
                    type,
                    new[] { info }));
            }
        }
    }

    private static Dictionary<int, int?> GetStepToNextStepDictionary(
        IGrouping<(int builderStep, string fluentMethodName, Type type), FluentApiInfo>[] grouping)
    {
        int[] builderSteps = grouping.Select(g => g.Key.builderStep).Distinct().ToArray();
        Dictionary<int, int?> stepToNextStep = new Dictionary<int, int?>();

        for (int i = 0; i < builderSteps.Length - 1; i++)
        {
            stepToNextStep[builderSteps[i]] = builderSteps[i + 1];
        }

        stepToNextStep[builderSteps[builderSteps.Length - 1]] = null;
        return stepToNextStep;
    }

    private static int? GetNextBuilderStep(FluentApiInfo[] fluentApiInfos, int? defaultNextBuilderStep)
    {
        HashSet<int?> nextSteps = new HashSet<int?>();
        nextSteps.UnionWith(fluentApiInfos.SelectMany(i => i.ControlAttributeInfos).Select(ToNextBuilderStep));

        static int? ToNextBuilderStep(ControlAttributeInfoBase attributeInfo)
        {
            return attributeInfo switch
            {
                FluentContinueWithAttributeInfo continueWithAttributeInfo
                    => continueWithAttributeInfo.ContinueWithBuilderStep,

                FluentBreakAttributeInfo breakAttributeInfo
                    => null,

                _ => throw new ArgumentException($"Unknown control attribute info type: {attributeInfo.GetType()}")
            };
        }

        if (nextSteps.Count == 0)
        {
            return defaultNextBuilderStep;
        }

        if (nextSteps.Count == 1)
        {
            return nextSteps.First();
        }

        // todo: implement diagnostics
        throw new GenerationException("Conflicting control attributes.");
    }
}