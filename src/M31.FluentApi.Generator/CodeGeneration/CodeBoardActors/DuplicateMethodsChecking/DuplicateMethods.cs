namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;

internal class DuplicateMethods
{
    private readonly BuilderMethodIdentity[] duplicates;

    internal DuplicateMethods(string methodName, BuilderMethodIdentity[] duplicates)
    {
        MethodName = methodName;
        this.duplicates = duplicates;
    }

    internal string MethodName { get; }
    internal IReadOnlyCollection<BuilderMethodIdentity> Duplicates => duplicates;
}