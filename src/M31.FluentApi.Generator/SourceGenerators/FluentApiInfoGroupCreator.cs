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

        // Group FluentApiInfos that have the same builder step, the same fluent method name, and represent
        // FluentMembers (compounds).
        (int builderStep, string fluentMethodName, Type type, FluentApiInfo[] infoArray)[] grouping = infos
            .GroupBy(i => (i.AttributeInfo.BuilderStep, i.FluentMethodName, i.AttributeInfo.GetType()))
            .SelectMany(g =>
                g.First().AttributeInfo.GetType() == typeof(FluentMemberAttributeInfo)
                    ? new[] { (g.Key.BuilderStep, g.Key.FluentMethodName, g.Key.GetType(), g.ToArray()) }
                    : g.Select(g2 => (g.Key.BuilderStep, g.Key.FluentMethodName, g.Key.GetType(), new[] { g2 })))
            .OrderBy(g => g.BuilderStep)
            .ToArray();

        Dictionary<int, int?> stepToNextStep = GetStepToNextStepDictionary(grouping);
        ReportInvalidSteps(stepToNextStep, infos);

        List<FluentApiInfoGroup> infoGroups = new List<FluentApiInfoGroup>();

        foreach (var group in grouping)
        {
            int? defaultNextBuilderStep = stepToNextStep[group.builderStep];
            int? nextBuilderStep = GetNextBuilderStep(
                group.infoArray,
                defaultNextBuilderStep,
                out List<AttributeDataExtended> skippableAttributeData);

            bool groupIsSkippable = skippableAttributeData.Count > 0;

            if (nextBuilderStep == null && groupIsSkippable)
            {
                foreach (AttributeDataExtended skippableData in skippableAttributeData)
                {
                    classInfoReport.ReportDiagnostic(LastBuilderStepCannotBeSkipped.CreateDiagnostic(skippableData));
                }
            }

            infoGroups.Add(new FluentApiInfoGroup(
                group.builderStep,
                nextBuilderStep,
                groupIsSkippable,
                group.fluentMethodName,
                group.type, group.infoArray));
        }

        return infoGroups;
    }

    private Dictionary<int, int?> GetStepToNextStepDictionary(
        (int builderStep, string fluentMethodName, Type type, FluentApiInfo[] infoArray)[] grouping)
    {
        int[] builderSteps = grouping.Select(g => g.builderStep).Distinct().ToArray();
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

    private int? GetNextBuilderStep(
        FluentApiInfo[] fluentApiInfos,
        int? defaultNextBuilderStep,
        out List<AttributeDataExtended> skippableAttributeData)
    {
        Dictionary<Step, List<AttributeDataExtended>> nextSteps = new Dictionary<Step, List<AttributeDataExtended>>();
        skippableAttributeData = new List<AttributeDataExtended>();

        foreach (FluentApiInfo fluentApiInfo in fluentApiInfos)
        {
            foreach (ControlAttributeInfoBase controlAttributeInfo in fluentApiInfo.ControlAttributeInfos)
            {
                if (controlAttributeInfo is FluentSkippableAttributeInfo skippableInfo)
                {
                    skippableAttributeData.Add(fluentApiInfo.AdditionalInfo.ControlAttributeData[skippableInfo]);
                    continue;
                }

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
                => (new Step(continueWithAttributeInfo.ContinueWithBuilderStep, "continue"),
                    fluentApiInfo.AdditionalInfo.ControlAttributeData[continueWithAttributeInfo]),

            FluentBreakAttributeInfo breakAttributeInfo
                => (new Step(null, "break"), fluentApiInfo.AdditionalInfo.ControlAttributeData[breakAttributeInfo]),

            FluentReturnAttributeInfo returnAttributeInfo
                => (new Step(null, "return"), fluentApiInfo.AdditionalInfo.ControlAttributeData[returnAttributeInfo]),

            _ => throw new ArgumentException($"Unknown control attribute info type: {attributeInfo.GetType()}")
        };
    }
}