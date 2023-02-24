using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class Fork
{
    internal Fork(string interfaceName, IReadOnlyList<BuilderMethod> builderMethods)
    {
        InterfaceName = interfaceName;
        BuilderMethods = builderMethods;
    }

    internal string InterfaceName { get; }
    internal IReadOnlyList<BuilderMethod> BuilderMethods { get; }
}