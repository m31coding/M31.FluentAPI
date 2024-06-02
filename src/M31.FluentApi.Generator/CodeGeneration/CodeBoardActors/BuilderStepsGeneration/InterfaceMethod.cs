using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class InterfaceMethod : Method
{
    internal InterfaceMethod(MethodSignature methodSignature, string interfaceName, BaseInterface? baseInterface)
        : base(methodSignature)
    {
        InterfaceName = interfaceName;
        BaseInterface = baseInterface;
    }

    internal string InterfaceName { get; }
    internal BaseInterface? BaseInterface { get; }
}