using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

internal class BuilderMethods
{
    public BuilderMethods(
        IReadOnlyCollection<BuilderStepMethod> staticMethods,
        IReadOnlyCollection<BuilderInterface> interfaces)
    {
        Interfaces = interfaces;
        StaticMethods = staticMethods;
    }

    internal IReadOnlyCollection<BuilderInterface> Interfaces { get; }
    internal IReadOnlyCollection<BuilderStepMethod> StaticMethods { get; }

    internal static IReadOnlyCollection<BuilderInterface> CreateInterfaces(
        IReadOnlyCollection<InterfaceBuilderMethod> interfaceMethods,
        CancellationToken cancellationToken)
    {
        List<BuilderInterface> interfaces = new List<BuilderInterface>();

        IGrouping<string, InterfaceBuilderMethod>[] methodsGroupedByInterface =
            interfaceMethods.GroupBy(m => m.InterfaceName).ToArray();

        foreach (IGrouping<string, InterfaceBuilderMethod> group in methodsGroupedByInterface)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }

            string interfaceName = group.Key;

            List<BaseInterface> baseInterfaces = new List<BaseInterface>();

            foreach (InterfaceBuilderMethod method in group)
            {
                if (method.BaseInterface != null)
                {
                    baseInterfaces.Add(method.BaseInterface);
                }
            }

            string[] baseInterfaceNames = baseInterfaces
                .DistinctBy(i => i.Name)
                .OrderBy(i => i.Step)
                .Select(i => i.Name).ToArray();

            interfaces.Add(new BuilderInterface(interfaceName, baseInterfaceNames, group.ToArray()));
        }

        return interfaces;
    }
}