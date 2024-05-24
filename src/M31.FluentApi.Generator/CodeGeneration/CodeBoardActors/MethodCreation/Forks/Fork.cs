namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class Fork
{
    internal Fork(int builderStep, string interfaceName, IReadOnlyList<ForkBuilderMethod> builderMethods)
    {
        BuilderStep = builderStep;
        InterfaceName = interfaceName;
        BuilderMethods = builderMethods;
    }

    internal int BuilderStep { get; }
    internal string InterfaceName { get; }
    internal IReadOnlyList<ForkBuilderMethod> BuilderMethods { get; }
}