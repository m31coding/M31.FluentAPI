using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class ForkBuilderMethod
{
    public ForkBuilderMethod(BuilderMethod builderMethod, int? nextBuilderStep)
    {
        Value = builderMethod;
        NextBuilderStep = nextBuilderStep;
    }

    internal BuilderMethod Value { get; }
    internal int? NextBuilderStep { get; }
}