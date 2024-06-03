using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class BuilderStepMethodCreator
{
    internal static IReadOnlyCollection<BuilderStepMethod> CreateBuilderStepMethods(IReadOnlyList<Fork> forks)
    {
        return forks.Count == 0 ? Array.Empty<BuilderStepMethod>() : new BuilderStepMethodCreator(forks).Create();
    }

    private readonly IReadOnlyList<Fork> forks;
    private readonly ListDictionary<Fork, BuilderStepMethod> forkToBuilderStepMethod;
    private readonly Dictionary<int, Fork> builderStepToFork;

    private BuilderStepMethodCreator(IReadOnlyList<Fork> forks)
    {
        this.forks = forks;
        forkToBuilderStepMethod = new ListDictionary<Fork, BuilderStepMethod>();
        builderStepToFork = forks.ToDictionary(f => f.BuilderStep);
    }

    private IReadOnlyCollection<BuilderStepMethod> Create()
    {
        BuilderStepMethod[] builderStepMethods = forks.SelectMany(CreateBuilderStepMethods).ToArray();
        return CreateStaticBuilderStepMethods().Concat(builderStepMethods).ToArray();
    }

    private IEnumerable<BuilderStepMethod> CreateBuilderStepMethods(Fork fork)
    {
        foreach (ForkBuilderMethod builderMethod in fork.BuilderMethods)
        {
            BuilderStepMethod builderStepMethod = CreateBuilderStepMethod(fork.InterfaceName, builderMethod);
            forkToBuilderStepMethod.AddItem(fork, builderStepMethod);
            yield return builderStepMethod;
        }
    }

    private BuilderStepMethod CreateBuilderStepMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        Fork? nextFork = TryGetNextFork(builderMethod.NextBuilderStep);
        return nextFork == null
            ? CreateLastStepBuilderMethod(interfaceName, builderMethod)
            : CreateInterjacentBuilderMethod(interfaceName, builderMethod, nextFork);
    }

    private BuilderStepMethod CreateInterjacentBuilderMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod,
        Fork nextFork)
    {
        string nextInterfaceName = nextFork.InterfaceName;

        BaseInterface? baseInterface =
            builderMethod.IsSkippable ? new BaseInterface(nextInterfaceName, nextFork.BuilderStep) : null;
        return new InterjacentBuilderMethod(builderMethod.Value, nextInterfaceName, interfaceName, baseInterface);
    }

    private BuilderStepMethod CreateLastStepBuilderMethod(
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        return new LastStepBuilderMethod(builderMethod.Value, interfaceName, null);
    }

    private IReadOnlyCollection<BuilderStepMethod> CreateStaticBuilderStepMethods()
    {
        List<BuilderStepMethod> staticBuilderStepMethods = new List<BuilderStepMethod>();

        Fork firstFork = forks.First();
        if (!forkToBuilderStepMethod.TryGetValue(firstFork, out List<BuilderStepMethod> methods))
        {
            return Array.Empty<BuilderStepMethod>();
        }

        foreach (BuilderStepMethod method in methods)
        {
            switch (method)
            {
                case InterjacentBuilderMethod interjacentBuilderMethod:
                    staticBuilderStepMethods.Add(
                        new FirstStepBuilderMethod(method, interjacentBuilderMethod.ReturnType));
                    break;

                case LastStepBuilderMethod:
                    staticBuilderStepMethods.Add(new SingleStepBuilderMethod(method));
                    break;

                default:
                    throw new ArgumentException($"Unexpected builder step method type: {method.GetType()}.");
            }
        }

        return staticBuilderStepMethods;
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