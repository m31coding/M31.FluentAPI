using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

internal abstract class InterfaceBuilderMethod : BuilderStepMethod
{
    public InterfaceBuilderMethod(
        BuilderMethod builderMethod,
        string interfaceName,
        BaseInterface? baseInterface)
        : base(builderMethod)
    {
        InterfaceName = interfaceName;
        BaseInterface = baseInterface;
    }

    internal string InterfaceName { get; }
    internal BaseInterface? BaseInterface { get; }
}