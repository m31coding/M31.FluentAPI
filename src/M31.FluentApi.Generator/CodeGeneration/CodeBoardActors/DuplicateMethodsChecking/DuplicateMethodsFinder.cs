using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;

internal class DuplicateMethodsFinder
{
    internal static IReadOnlyCollection<DuplicateMethods> FindGroupsOfDuplicateMethods(
        IReadOnlyCollection<BuilderMethod> builderMethods)
    {
        BuilderMethodIdentity[] methodIdentities = builderMethods.Select(CreateBuilderMethodIdentity).ToArray();
        return FindDuplicateMethods(methodIdentities);
    }

    private static BuilderMethodIdentity CreateBuilderMethodIdentity(BuilderMethod builderMethod)
    {
        // Create method identities with the actual builder method names.
        MethodIdentity methodIdentity =
            MethodIdentity.Create(builderMethod.MethodName, builderMethod.Parameters.Select(p => p.Type));

        return new BuilderMethodIdentity(builderMethod, methodIdentity);
    }

    private static IReadOnlyCollection<DuplicateMethods> FindDuplicateMethods(BuilderMethodIdentity[] methodIdentities)
    {
        return methodIdentities.GroupBy(m => m).Where(g => g.Count() > 1)
            .Select(g => new DuplicateMethods(g.First().BuilderMethod.MethodName, g.Select(v => v).ToArray()))
            .ToArray();
    }
}