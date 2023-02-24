using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal static class BuilderStepMethodCreator
{
    internal static IReadOnlyCollection<BuilderStepMethod> CreateBuilderStepMethods(IReadOnlyList<Fork> forks)
    {
        return forks.Count switch
        {
            0 => Array.Empty<BuilderStepMethod>(),
            1 => CreateSingleStepMethods(forks.First()).ToArray(),
            _ => CreateMultiStepMethods(forks)
        };
    }

    private static IEnumerable<BuilderStepMethod> CreateSingleStepMethods(Fork fork)
    {
        foreach (BuilderMethod builderMethod in fork.BuilderMethods)
        {
            yield return new SingleStepBuilderMethod(builderMethod);
        }
    }

    private static IReadOnlyCollection<BuilderStepMethod> CreateMultiStepMethods(IReadOnlyList<Fork> forks)
    {
        List<BuilderStepMethod> builderStepMethods =
            new List<BuilderStepMethod>(forks.Sum(f => f.BuilderMethods.Count));

        builderStepMethods.AddRange(CreateFirstBuilderStepMethods(forks[0], forks[1]));

        for (int i = 1; i < forks.Count - 1; i++)
        {
            builderStepMethods.AddRange(CreateInterjacentBuilderStepMethods(forks[i], forks[i + 1]));
        }

        builderStepMethods.AddRange(CreateLastBuilderStepMethods(forks[forks.Count - 1]));

        return builderStepMethods;
    }

    private static IEnumerable<BuilderStepMethod> CreateFirstBuilderStepMethods(Fork fork, Fork nextFork)
    {
        foreach (BuilderMethod builderMethod in fork.BuilderMethods)
        {
            yield return new FirstStepBuilderMethod(builderMethod, nextFork.InterfaceName);
        }
    }

    private static IEnumerable<BuilderStepMethod> CreateInterjacentBuilderStepMethods(Fork fork, Fork nextFork)
    {
        foreach (BuilderMethod builderMethod in fork.BuilderMethods)
        {
            yield return new InterjacentBuilderMethod(builderMethod, nextFork.InterfaceName, fork.InterfaceName);
        }
    }

    private static IEnumerable<BuilderStepMethod> CreateLastBuilderStepMethods(Fork fork)
    {
        foreach (BuilderMethod builderMethod in fork.BuilderMethods)
        {
            yield return new LastStepBuilderMethod(builderMethod, fork.InterfaceName);
        }
    }
}