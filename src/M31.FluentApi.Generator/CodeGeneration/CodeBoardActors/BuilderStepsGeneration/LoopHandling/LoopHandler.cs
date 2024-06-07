namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration.LoopHandling;

internal class LoopHandler
{
    private readonly IReadOnlyCollection<BuilderInterface> interfaces;
    private readonly IReadOnlyCollection<DependencyLoop> loops;
    private readonly Dictionary<BuilderInterface, DependencyLoop> interfacesToLoops;
    private readonly CancellationToken cancellationToken;
    private readonly Dictionary<DependencyLoop, BuilderInterface> loopToLastCreatedBuilderInterface;

    public static IReadOnlyCollection<BuilderInterface> HandleLoops(
        IReadOnlyCollection<BuilderInterface> interfaces,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<DependencyLoop> loops = LoopDetector.DetectLoops(interfaces);

        if (loops.Count == 0)
        {
            return interfaces;
        }

        Dictionary<BuilderInterface, DependencyLoop> interfacesToLoops =
            new Dictionary<BuilderInterface, DependencyLoop>();

        foreach (DependencyLoop loop in loops)
        {
            foreach (BuilderInterface builderInterface in loop.Interfaces)
            {
                interfacesToLoops.Add(builderInterface, loop);
            }
        }

        return new LoopHandler(interfaces, loops, interfacesToLoops, cancellationToken).HandleLoops();
    }

    private LoopHandler(
        IReadOnlyCollection<BuilderInterface> interfaces,
        IReadOnlyCollection<DependencyLoop> loops,
        Dictionary<BuilderInterface, DependencyLoop> interfacesToLoops,
        CancellationToken cancellationToken)
    {
        this.interfaces = interfaces;
        this.loops = loops;
        this.interfacesToLoops = interfacesToLoops;
        this.cancellationToken = cancellationToken;
        loopToLastCreatedBuilderInterface = new Dictionary<DependencyLoop, BuilderInterface>();
    }

    private IReadOnlyCollection<BuilderInterface> HandleLoops()
    {
        List<BuilderInterface> resultInterfaces = new List<BuilderInterface>(interfaces.Count + loops.Count);
        resultInterfaces.AddRange(CreateEmptyInterfacesForLoops());
        AddCommonInterfaces(resultInterfaces);
        return resultInterfaces;
    }

    private IEnumerable<BuilderInterface> CreateEmptyInterfacesForLoops()
    {
        foreach (BuilderInterface builderInterface in interfaces)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            if (interfacesToLoops.TryGetValue(builderInterface, out DependencyLoop? loop))
            {
                string[] newBaseInterfaces = builderInterface.BaseInterfaces
                    .Except(loop.Interfaces.Select(i => i.InterfaceName))
                    .Concat(new[] { loop.CommonInterfaceName }).ToArray();
                BuilderInterface newBuilderInterface = new BuilderInterface(
                    builderInterface.InterfaceName,
                    newBaseInterfaces,
                    Array.Empty<InterfaceBuilderMethod>());
                loopToLastCreatedBuilderInterface[loop] = newBuilderInterface;
                yield return newBuilderInterface;
            }
            else
            {
                yield return builderInterface;
            }
        }
    }

    private void AddCommonInterfaces(List<BuilderInterface> resultInterfaces)
    {
        foreach (DependencyLoop loop in loops)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            IReadOnlyCollection<InterfaceBuilderMethod> newInterfaceBuilderMethods
                = loop.Interfaces.SelectMany(i => i.Methods).Select(m =>
                    CreateNewInterfaceBuilderMethodForCommonInterface(m, loop.CommonInterfaceName)).ToArray();

            BuilderInterface commonInterface = new BuilderInterface(
                loop.CommonInterfaceName,
                Array.Empty<string>(),
                newInterfaceBuilderMethods);

            BuilderInterface lastCreatedBuilderInterface = loopToLastCreatedBuilderInterface[loop];
            int index = resultInterfaces.FindIndex(i => i == lastCreatedBuilderInterface);
            resultInterfaces.Insert(index + 1, commonInterface);
        }
    }

    private static InterfaceBuilderMethod CreateNewInterfaceBuilderMethodForCommonInterface(
        InterfaceBuilderMethod method,
        string commonInterfaceName)
    {
        switch (method)
        {
            case InterjacentBuilderMethod interjacentBuilderMethod:
                return new InterjacentBuilderMethod(
                    interjacentBuilderMethod,
                    interjacentBuilderMethod.ReturnType,
                    commonInterfaceName,
                    null);

            case LastStepBuilderMethod lastStepBuilderMethod:
                return new LastStepBuilderMethod(lastStepBuilderMethod, commonInterfaceName);

            default:
                throw new ArgumentException($"Unexpected builder step method type: {method.GetType()}.");
        }
    }
}