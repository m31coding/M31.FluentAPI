using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class EmptyInterfaceBuilderMethod : InterfaceBuilderMethod // todo: remove if possible
{
    public EmptyInterfaceBuilderMethod(string interfaceName, BaseInterface? baseInterface)
        : base(
            new BuilderMethod(string.Empty, null, Array.Empty<Parameter>(), null, (_, _, _) => new List<string>()),
            interfaceName,
            baseInterface)
    {
    }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info, ReservedVariableNames reservedVariableNames)
    {
        throw new NotSupportedException();
    }
}