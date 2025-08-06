using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class BuilderMethods
{
    private readonly List<BuilderMethod> methods;
    private readonly HashSet<string> requiredUsings;

    internal BuilderMethods(List<BuilderMethod> methods, HashSet<string> requiredUsings)
    {
        this.methods = methods;
        this.requiredUsings = requiredUsings;
    }

    internal BuilderMethods()
    {
        methods = new List<BuilderMethod>();
        requiredUsings = new HashSet<string>();
    }

    internal BuilderMethods(BuilderMethod builderMethod)
    {
        methods = new List<BuilderMethod>() { builderMethod };
        requiredUsings = new HashSet<string>();
    }

    internal void Add(BuilderMethods builderMethods)
    {
        methods.AddRange(builderMethods.Methods);

        foreach (string @using in builderMethods.RequiredUsings)
        {
            requiredUsings.Add(@using);
        }
    }

    internal IReadOnlyCollection<BuilderMethod> Methods => methods;
    internal IReadOnlyCollection<string> RequiredUsings => requiredUsings;
}