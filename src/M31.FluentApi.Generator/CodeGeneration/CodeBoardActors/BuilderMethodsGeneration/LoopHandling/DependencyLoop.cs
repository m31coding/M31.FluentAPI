namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration.LoopHandling;

/// <summary>
/// Interfaces that form a circular dependency.
/// </summary>
internal class DependencyLoop
{
    private DependencyLoop(IReadOnlyCollection<BuilderInterface> interfaces, string commonInterfaceName)
    {
        Interfaces = interfaces;
        CommonInterfaceName = commonInterfaceName;
    }

    internal IReadOnlyCollection<BuilderInterface> Interfaces { get; }
    internal string CommonInterfaceName { get; }

    internal static DependencyLoop Create(IReadOnlyCollection<BuilderInterface> interfaces)
    {
        string commonInterfaceName = GetCommonInterfaceName(interfaces.SelectMany(i => i.Methods).ToArray());
        return new DependencyLoop(interfaces, commonInterfaceName);
    }

    private static string GetCommonInterfaceName(IReadOnlyCollection<InterfaceBuilderMethod> methods)
    {
        return $"I{string.Join(string.Empty, methods.Select(m => m.InterfaceName).Distinct().Select(RemoveCapitalI))}";

        static string RemoveCapitalI(string interfaceName)
        {
            return interfaceName.Substring(1, interfaceName.Length - 1);
        }
    }
}