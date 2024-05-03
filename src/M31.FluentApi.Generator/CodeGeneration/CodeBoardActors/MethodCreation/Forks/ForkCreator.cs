using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;

internal class ForkCreator : ICodeBoardActor
{
    private readonly Dictionary<int, ForkUnderConstruction> forksByBuilderStep;

    internal ForkCreator()
    {
        forksByBuilderStep = new Dictionary<int, ForkUnderConstruction>();
    }

    public void Modify(CodeBoard codeBoard)
    {
        BuilderMethodFactory builderMethodFactory = new BuilderMethodFactory(codeBoard.InnerBodyCreationDelegates);
        MethodCreator methodCreator = new MethodCreator(builderMethodFactory);
        CreateForks(codeBoard.FluentApiInfos, methodCreator, codeBoard);
        codeBoard.Forks = GetForks();
    }

    private void CreateForks(
        IReadOnlyCollection<FluentApiInfo> infos,
        MethodCreator methodCreator,
        CodeBoard codeBoard)
    {
        foreach (FluentApiInfoGroup group in codeBoard.FluentApiInfoGroups)
        {
            if (codeBoard.CancellationRequested)
            {
                return;
            }

            BuilderMethodCreator builderMethodCreator = new BuilderMethodCreator(group, codeBoard);
            BuilderMethods builderMethods = builderMethodCreator.CreateBuilderMethods(methodCreator);

            foreach (string @using in builderMethods.RequiredUsings)
            {
                codeBoard.CodeFile.AddUsing(@using);
            }

            AddBuilderMethodsToFork(
                group.BuilderStep,
                group.NextBuilderStep,
                group.FluentMethodName,
                builderMethods.Methods);
        }
    }

    private void AddBuilderMethodsToFork(
        int builderStep,
        int? nextBuilderStep,
        string interfacePartialName,
        IEnumerable<BuilderMethod> builderMethods)
    {
        if (!forksByBuilderStep.TryGetValue(builderStep, out ForkUnderConstruction? forkUnderConstruction))
        {
            forkUnderConstruction = forksByBuilderStep[builderStep] = new ForkUnderConstruction(builderStep);
        }

        ForkBuilderMethod[] forkBuilderMethods =
            builderMethods.Select(b => new ForkBuilderMethod(b, nextBuilderStep)).ToArray();
        forkUnderConstruction.AddBuilderMethods(interfacePartialName, forkBuilderMethods);
    }

    private IReadOnlyList<Fork> GetForks()
    {
        List<Fork> result = new List<Fork>(forksByBuilderStep.Count);
        HashSet<string> interfaceNames = new HashSet<string>();

        foreach (ForkUnderConstruction fork in forksByBuilderStep.OrderBy(f => f.Key).Select(f => f.Value))
        {
            string interfaceName = fork.InterfaceName;
            int i = 2;

            while (interfaceNames.Contains(interfaceName))
            {
                interfaceName = $"{fork.InterfaceName}{i++}";
            }

            interfaceNames.Add(interfaceName);
            result.Add(new Fork(fork.BuilderStep, interfaceName, fork.BuilderMethods));
        }

        return result;
    }
}