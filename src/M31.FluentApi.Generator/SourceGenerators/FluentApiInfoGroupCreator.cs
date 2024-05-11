using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiInfoGroupCreator
{
    internal static IReadOnlyCollection<FluentApiInfoGroup> CreateGroups(
        IReadOnlyCollection<FluentApiInfo> infos,
        ClassInfoReport classInfoReport)
    {
        return new FluentApiInfoGroupCreator(classInfoReport).CreateGroups(infos);
    }

    private readonly ClassInfoReport classInfoReport;

    private FluentApiInfoGroupCreator(ClassInfoReport classInfoReport)
    {
        this.classInfoReport = classInfoReport;
    }

    private IReadOnlyCollection<FluentApiInfoGroup> CreateGroups(IReadOnlyCollection<FluentApiInfo> infos)
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
        ReportInvalidSteps(stepToNextStep, infos);

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

    private Dictionary<int, int?> GetStepToNextStepDictionary(
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

    private void ReportInvalidSteps(Dictionary<int, int?> stepToNextStep, IReadOnlyCollection<FluentApiInfo> infos)
    {
        foreach (FluentApiInfo info in infos)
        {
            foreach (FluentContinueWithAttributeInfo continueWith in
                     info.ControlAttributeInfos.OfType<FluentContinueWithAttributeInfo>())
            {
                if (!stepToNextStep.ContainsKey(continueWith.ContinueWithBuilderStep))
                {
                    AttributeDataExtended attributeData = info.AdditionalInfo.ControlAttributeData[continueWith];
                    classInfoReport.ReportDiagnostic(
                        MissingBuilderStep.CreateDiagnostic(attributeData, continueWith.ContinueWithBuilderStep));
                }
            }
        }
    }

    private int? GetNextBuilderStep(FluentApiInfo[] fluentApiInfos, int? defaultNextBuilderStep)
    {
        Dictionary<Step, List<AttributeDataExtended>> nextSteps = new Dictionary<Step, List<AttributeDataExtended>>();

        foreach (FluentApiInfo fluentApiInfo in fluentApiInfos)
        {
            foreach (ControlAttributeInfoBase controlAttributeInfo in fluentApiInfo.ControlAttributeInfos)
            {
                var (nextBuilderStep, attributeDataExtended) = ToNextBuilderStep(controlAttributeInfo, fluentApiInfo);

                if (nextSteps.TryGetValue(nextBuilderStep, out List<AttributeDataExtended> present))
                {
                    present.Add(attributeDataExtended);
                }
                else
                {
                    nextSteps[nextBuilderStep] = new List<AttributeDataExtended>() { attributeDataExtended };
                }
            }
        }

        if (nextSteps.Count == 0)
        {
            return defaultNextBuilderStep;
        }

        if (nextSteps.Count == 1)
        {
            return nextSteps.First().Key.Value;
        }

        foreach (AttributeDataExtended attributeData in nextSteps.Values.SelectMany(a => a))
        {
            classInfoReport.ReportDiagnostic(ConflictingControlAttributes.CreateDiagnostic(attributeData));
        }

        return nextSteps.Keys.Any(k => k.Value == null)
            ? null
            : nextSteps.Keys.Select(k => k.Value).OfType<int>().Min();
    }

    private static (Step, AttributeDataExtended) ToNextBuilderStep(
        ControlAttributeInfoBase attributeInfo,
        FluentApiInfo fluentApiInfo)
    {
        return attributeInfo switch
        {
            FluentContinueWithAttributeInfo continueWithAttributeInfo
                => (new Step(continueWithAttributeInfo.ContinueWithBuilderStep),
                    fluentApiInfo.AdditionalInfo.ControlAttributeData[continueWithAttributeInfo]),

            FluentBreakAttributeInfo breakAttributeInfo
                => (new Step(null), fluentApiInfo.AdditionalInfo.ControlAttributeData[breakAttributeInfo]),

            FluentReturnAttributeInfo returnAttributeInfo
                => (new Step(null), fluentApiInfo.AdditionalInfo.ControlAttributeData[returnAttributeInfo]),

            _ => throw new ArgumentException($"Unknown control attribute info type: {attributeInfo.GetType()}")
        };
    }
}