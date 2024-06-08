using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

internal class BuilderGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        BuilderMethods builderMethods =
            BuilderMethodsCreator.CreateBuilderMethods(codeBoard.Forks, codeBoard.CancellationToken);

        foreach (BuilderStepMethod staticMethod in builderMethods.StaticMethods)
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            Method method = CreateMethod(staticMethod, codeBoard);
            codeBoard.BuilderClass.AddMethod(method);
        }

        List<Interface> interfaces = new List<Interface>(builderMethods.Interfaces.Count);
        interfaces.Add(CreateInitialStepInterface(builderMethods, codeBoard));

        foreach (BuilderInterface builderInterface in builderMethods.Interfaces)
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            Interface @interface =
                new Interface(codeBoard.Info.DefaultAccessModifier, builderInterface.InterfaceName);

            foreach (InterfaceBuilderMethod interfaceMethod in builderInterface.Methods)
            {
                Method method = CreateMethod(interfaceMethod, codeBoard);
                codeBoard.BuilderClass.AddMethod(method);
                @interface.AddMethodSignature(method.MethodSignature.ToSignatureForInterface());
            }

            @interface.AddBaseInterfaces(builderInterface.BaseInterfaces);
            interfaces.Add(@interface);
        }

        AddInterfacesToBuilderClass(
            interfaces,
            codeBoard.BuilderClass,
            codeBoard.Info.BuilderClassNameWithTypeParameters);
        AddInterfaceDefinitionsToBuilderClass(interfaces, codeBoard.BuilderClass);
    }

    private Method CreateMethod(BuilderStepMethod builderStepMethod, CodeBoard codeBoard)
    {
        ReservedVariableNames reservedVariableNames = codeBoard.ReservedVariableNames.NewLocalScope();
        reservedVariableNames.ReserveLocalVariableNames(builderStepMethod.Parameters.Select(p => p.Name));

        Method method = builderStepMethod.BuildMethodCode(
            codeBoard.Info,
            reservedVariableNames);

        return method;
    }

    private Interface CreateInitialStepInterface(BuilderMethods builderMethods, CodeBoard codeBoard)
    {
        string? firstInterfaceName = builderMethods.Interfaces.FirstOrDefault()?.InterfaceName;

        Interface initialStepInterface =
            new Interface(codeBoard.Info.DefaultAccessModifier, codeBoard.Info.InitialStepInterfaceName);

        if (firstInterfaceName != null)
        {
            initialStepInterface.AddBaseInterface(firstInterfaceName);
        }

        return initialStepInterface;
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