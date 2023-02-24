using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class ForkUnderConstruction
{
    internal string InterfaceName { get; private set; }
    internal List<BuilderMethod> BuilderMethods { get; }

    internal ForkUnderConstruction()
    {
        InterfaceName = "I";
        BuilderMethods = new List<BuilderMethod>();
    }

    internal void AddBuilderMethods(string interfacePartialName, IEnumerable<BuilderMethod> builderMethods)
    {
        InterfaceName += interfacePartialName;
        this.BuilderMethods.AddRange(builderMethods);
    }
}