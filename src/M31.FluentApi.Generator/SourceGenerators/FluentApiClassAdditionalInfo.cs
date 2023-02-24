namespace M31.FluentApi.Generator.SourceGenerators;

internal class FluentApiClassAdditionalInfo
{
    internal FluentApiClassAdditionalInfo(Dictionary<FluentApiInfo, FluentApiAdditionalInfo> additionalInfos)
    {
        AdditionalInfos = additionalInfos;
    }

    internal Dictionary<FluentApiInfo, FluentApiAdditionalInfo> AdditionalInfos { get; }
}