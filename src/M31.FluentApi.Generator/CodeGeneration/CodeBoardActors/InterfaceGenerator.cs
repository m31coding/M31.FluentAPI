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
        IGrouping<string, InterfaceMethod>[] methodsGroupedByInterface = codeBoard.BuilderClass.Methods
            .OfType<InterfaceMethod>().GroupBy(m => m!.InterfaceName).ToArray();

        List<Interface> interfaces = new List<Interface>(methodsGroupedByInterface.Length);

        string? firstInterfaceName = methodsGroupedByInterface.Select(g => g.Key).FirstOrDefault();
        if (firstInterfaceName != null)
        {
            Interface initialStepInterface =
                new Interface(codeBoard.Info.DefaultAccessModifier, codeBoard.Info.InitialStepInterfaceName);
            initialStepInterface.AddBaseInterface(firstInterfaceName);
            interfaces.Add(initialStepInterface);
        }

        foreach (IGrouping<string, InterfaceMethod> group in methodsGroupedByInterface)
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            Interface @interface = new Interface(codeBoard.Info.DefaultAccessModifier, group.Key);

            foreach (InterfaceMethod method in group)
            {
                @interface.AddMethodSignature(method.MethodSignature.ToSignatureForInterface());
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