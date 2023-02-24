using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class InterfaceMethod : Method
{
    internal InterfaceMethod(MethodSignature methodSignature, string interfaceName)
        : base(methodSignature)
    {
        InterfaceName = interfaceName;
    }

    internal string InterfaceName { get; }
}