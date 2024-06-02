using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class ForkBuilderMethod
{
    public ForkBuilderMethod(BuilderMethod builderMethod, int? nextBuilderStep, bool isSkippable)
    {
        Value = builderMethod;
        NextBuilderStep = nextBuilderStep;
        IsSkippable = isSkippable;
    }

    internal BuilderMethod Value { get; }
    internal int? NextBuilderStep { get; }
    internal bool IsSkippable { get; }
}