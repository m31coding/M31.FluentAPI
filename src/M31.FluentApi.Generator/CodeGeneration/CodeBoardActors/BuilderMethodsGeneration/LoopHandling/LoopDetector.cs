namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration.LoopHandling;

internal static class LoopDetector
{
    internal static IReadOnlyCollection<DependencyLoop> DetectLoops(IReadOnlyCollection<BuilderInterface> interfaces)
    {
        if (interfaces.Count(i => i.BaseInterfaces.Count > 0) < 2)
        {
            return Array.Empty<DependencyLoop>();
        }

        Dictionary<BuilderInterface, int> order = interfaces.Select((i, index) => (i, index))
            .ToDictionary(kvp => kvp.i, kvp => kvp.index);

        Dictionary<string, BuilderInterface> interfaceNameToBuilderInterface =
            interfaces.ToDictionary(i => i.InterfaceName);

        IReadOnlyCollection<IReadOnlyCollection<BuilderInterface>> stronglyConnectedComponents =
            TarjansSccAlgorithm<BuilderInterface>.GetStronglyConnectedComponents(
                interfaces,
                i => i.BaseInterfaces.Select(b => interfaceNameToBuilderInterface[b]),
                false);

        return OrderLoops(stronglyConnectedComponents.Select(i => DependencyLoop.Create(OrderInterfaces(i)))).ToArray();

        IReadOnlyCollection<BuilderInterface> OrderInterfaces(IReadOnlyCollection<BuilderInterface> unorderedInterfaces)
        {
            return unorderedInterfaces.OrderBy(i => order[i]).ToArray();
        }

        IEnumerable<DependencyLoop> OrderLoops(IEnumerable<DependencyLoop> unorderedLoops)
        {
            return unorderedLoops.OrderBy(l => order[l.Interfaces.First()]);
        }
    }
}