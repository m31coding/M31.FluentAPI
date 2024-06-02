using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class BuilderStepMethodCreator
{
    internal static IReadOnlyCollection<BuilderStepMethod> CreateBuilderStepMethods(IReadOnlyList<Fork> forks)
    {
        if (forks.Count == 0)
        {
            return Array.Empty<BuilderStepMethod>();
        }

        return new BuilderStepMethodCreator(forks).Create();
    }

    private readonly IReadOnlyList<Fork> forks;
    private readonly Fork firstFork;
    private readonly Dictionary<int, Fork> builderStepToFork;

    private BuilderStepMethodCreator(IReadOnlyList<Fork> forks)
    {
        this.forks = forks;
        firstFork = forks.First();
        builderStepToFork = forks.ToDictionary(f => f.BuilderStep);
    }

    private IReadOnlyCollection<BuilderStepMethod> Create()
    {
        return forks.SelectMany(CreateBuilderStepMethods).ToArray();
    }

    private IEnumerable<BuilderStepMethod> CreateBuilderStepMethods(Fork fork)
    {
        bool isFirstStep = fork == firstFork;

        foreach (ForkBuilderMethod builderMethod in fork.BuilderMethods)
        {
            foreach (BuilderStepMethod builderStepMethod in
                     CreateBuilderStepMethods(isFirstStep, fork.InterfaceName, builderMethod))
            {
                yield return builderStepMethod;
            }
        }
    }

    private BuilderStepMethod[] CreateBuilderStepMethods(
        bool isFirstStep,
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        Fork? nextFork = TryGetNextFork(builderMethod.NextBuilderStep);
        return nextFork == null
            ? CreateBuilderStepMethodsForLastStep(isFirstStep, interfaceName, builderMethod)
            : CreateBuilderStepMethods(isFirstStep, interfaceName, builderMethod, nextFork);
    }

    private BuilderStepMethod[] CreateBuilderStepMethods(
        bool isFirstStep,
        string interfaceName,
        ForkBuilderMethod builderMethod,
        Fork nextFork)
    {
        string nextInterfaceName = nextFork.InterfaceName;

        BaseInterface? baseInterface =
            builderMethod.IsSkippable ? new BaseInterface(nextInterfaceName, nextFork.BuilderStep) : null;

        if (isFirstStep)
        {
            return new BuilderStepMethod[]
            {
                new FirstStepBuilderMethod(builderMethod.Value, nextInterfaceName),
                new InterjacentBuilderMethod(builderMethod.Value, nextInterfaceName, interfaceName, baseInterface),
            };
        }

        return new BuilderStepMethod[]
        {
            new InterjacentBuilderMethod(builderMethod.Value, nextInterfaceName, interfaceName, baseInterface),
        };
    }

    private BuilderStepMethod[] CreateBuilderStepMethodsForLastStep(
        bool isFirstStep,
        string interfaceName,
        ForkBuilderMethod builderMethod)
    {
        if (isFirstStep)
        {
            return new BuilderStepMethod[]
            {
                new SingleStepBuilderMethod(builderMethod.Value),
                new LastStepBuilderMethod(builderMethod.Value, interfaceName, null),
            };
        }

        return new BuilderStepMethod[]
        {
            new LastStepBuilderMethod(builderMethod.Value, interfaceName, null),
        };
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