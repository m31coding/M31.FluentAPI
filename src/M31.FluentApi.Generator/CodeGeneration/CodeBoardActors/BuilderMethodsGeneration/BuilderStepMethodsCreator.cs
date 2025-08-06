using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration.LoopHandling;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

internal class BuilderStepMethodsCreator
{
    internal static BuilderStepMethods CreateBuilderMethods(IReadOnlyList<Fork> forks, CancellationToken cancellationToken)
    {
        return forks.Count == 0 ? EmptyBuilderMethods()
            : new BuilderStepMethodsCreator(forks, cancellationToken).Create();
    }

    private readonly IReadOnlyList<Fork> forks;
    private readonly CancellationToken cancellationToken;
    private readonly ListDictionary<string, BuilderStepMethod> interfaceNameToBuilderMethods;
    private readonly Dictionary<int, Fork> builderStepToFork;

    private BuilderStepMethodsCreator(IReadOnlyList<Fork> forks, CancellationToken cancellationToken)
    {
        this.forks = forks;
        this.cancellationToken = cancellationToken;
        interfaceNameToBuilderMethods = new ListDictionary<string, BuilderStepMethod>();
        builderStepToFork = forks.ToDictionary(f => f.BuilderStep);
    }

    private static BuilderStepMethods EmptyBuilderMethods()
    {
        return new BuilderStepMethods(Array.Empty<BuilderStepMethod>(), Array.Empty<BuilderInterface>());
    }

    private BuilderStepMethods Create()
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return EmptyBuilderMethods();
        }

        InterfaceBuilderMethod[] interfaceMethods = forks.SelectMany(CreateInterfaceBuilderMethods).ToArray();

        if (cancellationToken.IsCancellationRequested)
        {
            return EmptyBuilderMethods();
        }

        IReadOnlyCollection<BuilderInterface> interfaces =
            BuilderStepMethods.CreateInterfaces(interfaceMethods, cancellationToken);

        if (cancellationToken.IsCancellationRequested)
        {
            return EmptyBuilderMethods();
        }

        interfaces = LoopHandler.HandleLoops(interfaces, cancellationToken);

        if (cancellationToken.IsCancellationRequested)
        {
            return EmptyBuilderMethods();
        }

        IReadOnlyCollection<BuilderStepMethod> staticMethods = CreateStaticBuilderStepMethods();

        if (cancellationToken.IsCancellationRequested)
        {
            return EmptyBuilderMethods();
        }

        return new BuilderStepMethods(staticMethods, interfaces);
    }

    private IEnumerable<InterfaceBuilderMethod> CreateInterfaceBuilderMethods(Fork fork)
    {
        foreach (ForkBuilderMethod builderMethod in fork.BuilderMethods)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            InterfaceBuilderMethod builderStepMethod = CreateInterfaceBuilderMethod(fork.InterfaceName, builderMethod);
            interfaceNameToBuilderMethods.AddItem(fork.InterfaceName, builderStepMethod);
            yield return builderStepMethod;
        }
    }

    private InterfaceBuilderMethod CreateInterfaceBuilderMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        Fork? nextFork = TryGetNextFork(builderMethod.NextBuilderStep);
        return nextFork == null
            ? CreateLastStepBuilderMethod(interfaceName, builderMethod)
            : CreateInterjacentBuilderMethod(interfaceName, builderMethod, nextFork);
    }

    private InterfaceBuilderMethod CreateInterjacentBuilderMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod,
        Fork nextFork)
    {
        string nextInterfaceName = nextFork.InterfaceName;

        BaseInterface? baseInterface =
            builderMethod.IsSkippable ? new BaseInterface(nextInterfaceName, nextFork.BuilderStep) : null;
        return new InterjacentBuilderMethod(builderMethod.Value, nextInterfaceName, interfaceName, baseInterface);
    }

    private InterfaceBuilderMethod CreateLastStepBuilderMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        return new LastStepBuilderMethod(builderMethod.Value, interfaceName);
    }

    private IReadOnlyCollection<BuilderStepMethod> CreateStaticBuilderStepMethods()
    {
        List<BuilderStepMethod> staticBuilderMethods = new List<BuilderStepMethod>();
        HashSet<string> seenInterfaces = new HashSet<string>();
        Queue<string> interfaces = new Queue<string>();
        string firstInterface = forks.First().InterfaceName;
        seenInterfaces.Add(firstInterface);
        interfaces.Enqueue(firstInterface);

        while (interfaces.Count != 0)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Array.Empty<BuilderStepMethod>();
            }

            string @interface = interfaces.Dequeue();

            if (!interfaceNameToBuilderMethods.TryGetValue(@interface, out List<BuilderStepMethod> methods))
            {
                continue;
            }

            foreach (BuilderStepMethod method in methods)
            {
                switch (method)
                {
                    case InterjacentBuilderMethod interjacentBuilderMethod:
                        staticBuilderMethods.Add(
                            new StaticBuilderMethod(method, interjacentBuilderMethod.ReturnType));

                        string? baseInterface = interjacentBuilderMethod.BaseInterface?.Name;
                        if (baseInterface != null && seenInterfaces.Add(baseInterface))
                        {
                            interfaces.Enqueue(baseInterface);
                        }

                        break;

                    case LastStepBuilderMethod:
                        staticBuilderMethods.Add(new StaticSingleStepBuilderMethod(method));
                        break;

                    default:
                        throw new ArgumentException($"Unexpected builder step method type: {method.GetType()}.");
                }
            }
        }

        return staticBuilderMethods;
    }

    private Fork? TryGetNextFork(int? nextBuilderStep)
    {
        if (nextBuilderStep == null)
        {
            return null;
        }

        if (!builderStepToFork.TryGetValue(nextBuilderStep.Value, out Fork nextFork))
        {
            throw new GenerationException(
                $"Unable to obtain the next fork. Builder step {nextBuilderStep.Value} is unknown.");
        }

        return nextFork;
    }
}