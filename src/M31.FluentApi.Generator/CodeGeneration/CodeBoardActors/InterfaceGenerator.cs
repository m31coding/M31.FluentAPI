using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class InterfaceGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        List<Interface> interfaces = CreateInterfaces(codeBoard);
        AddInterfacesToBuilderClass(
            interfaces,
            codeBoard.BuilderClass,
            codeBoard.Info.BuilderClassNameWithTypeParameters);
        AddInterfaceDefinitionsToBuilderClass(interfaces, codeBoard.BuilderClass);
    }

    private List<Interface> CreateInterfaces(CodeBoard codeBoard)
    {
        string interfaceAccessModifier = codeBoard.Info.FluentApiTypeIsInternal ? "internal" : "public";

        IGrouping<string, InterfaceMethod>[] methodsGroupedByInterface = codeBoard.BuilderClass.Methods
            .OfType<InterfaceMethod>().GroupBy(m => m!.InterfaceName).ToArray();

        List<Interface> interfaces = new List<Interface>(methodsGroupedByInterface.Length);

        foreach (IGrouping<string, InterfaceMethod> group in methodsGroupedByInterface)
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            Interface @interface = new Interface(interfaceAccessModifier, group.Key);

            foreach (InterfaceMethod method in group)
            {
                @interface.AddMethodSignature(method.MethodSignature.ToStandAloneMethodSignature());
            }

            interfaces.Add(@interface);
        }

        return interfaces;
    }

    private void AddInterfacesToBuilderClass(List<Interface> interfaces, Class builderClass, string prefix)
    {
        foreach (Interface @interface in interfaces)
        {
            builderClass.AddInterface($"{prefix}.{@interface.Name}");
        }
    }

    private void AddInterfaceDefinitionsToBuilderClass(List<Interface> interfaces, Class builderClass)
    {
        foreach (Interface @interface in interfaces)
        {
            builderClass.AddDefinition(@interface);
        }
    }
}