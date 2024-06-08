namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

internal class BuilderInterface
{
    internal BuilderInterface(
        string interfaceName,
        IReadOnlyCollection<string> baseInterfaces,
        IReadOnlyCollection<InterfaceBuilderMethod> methods)
    {
        InterfaceName = interfaceName;
        BaseInterfaces = baseInterfaces;
        Methods = methods;
    }

    public string InterfaceName { get; }
    public IReadOnlyCollection<string> BaseInterfaces { get; }
    public IReadOnlyCollection<InterfaceBuilderMethod> Methods { get; }
}