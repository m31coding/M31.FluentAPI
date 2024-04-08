using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiClassAdditionalInfo
{
    internal FluentApiClassAdditionalInfo(IReadOnlyCollection<FluentApiInfoGroup> fluentApiInfoGroups)
    {
        FluentApiInfoGroups = fluentApiInfoGroups;
    }

    internal IReadOnlyCollection<FluentApiInfoGroup> FluentApiInfoGroups { get; }
}