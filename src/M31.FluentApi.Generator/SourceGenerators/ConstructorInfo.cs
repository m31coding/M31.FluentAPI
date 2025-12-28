using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.SourceGenerators;

internal record ConstructorInfo
{
    public ConstructorInfo(IReadOnlyCollection<ParameterSymbolInfo> parameterInfos, bool constructorIsNonPublic)
    {
        ParameterInfos = parameterInfos;
        ConstructorIsNonPublic = constructorIsNonPublic;
    }

    internal IReadOnlyCollection<ParameterSymbolInfo> ParameterInfos { get; }
    internal bool ConstructorIsNonPublic { get; }
    internal int NumberOfParameters => ParameterInfos.Count;
}