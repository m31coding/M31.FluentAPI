using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class ForkUnderConstruction
{
    internal int BuilderStep { get; private set; }
    internal string InterfaceName { get; private set; }
    internal List<ForkBuilderMethod> BuilderMethods { get; }

    internal ForkUnderConstruction(int builderStep)
    {
        BuilderStep = builderStep;
        InterfaceName = "I";
        BuilderMethods = new List<ForkBuilderMethod>();
    }

    internal void AddBuilderMethods(string interfacePartialName, IEnumerable<ForkBuilderMethod> builderMethods)
    {
        InterfaceName += interfacePartialName;
        BuilderMethods.AddRange(builderMethods);
    }
}