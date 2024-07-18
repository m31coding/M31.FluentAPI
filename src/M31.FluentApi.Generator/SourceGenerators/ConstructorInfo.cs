namespace M31.FluentApi.Generator.SourceGenerators;

internal record ConstructorInfo
{
    public ConstructorInfo(int numberOfParameters, bool constructorIsNonPublic)
    {
        NumberOfParameters = numberOfParameters;
        ConstructorIsNonPublic = constructorIsNonPublic;
    }

    internal int NumberOfParameters { get; }
    internal bool ConstructorIsNonPublic { get; }
}