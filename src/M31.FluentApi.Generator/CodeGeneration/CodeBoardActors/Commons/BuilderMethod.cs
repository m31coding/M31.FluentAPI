using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethod
{
    internal string MethodName { get; }
    internal GenericInfo? GenericInfo { get; }
    internal IReadOnlyCollection<Parameter> Parameters { get; }
    internal BuildBodyCode BuildBodyCode { get; }

    internal BuilderMethod(
        string methodName,
        GenericInfo? genericInfo,
        IReadOnlyCollection<Parameter> parameters,
        BuildBodyCode buildBodyCode)
    {
        MethodName = methodName;
        GenericInfo = genericInfo;
        Parameters = parameters;
        BuildBodyCode = buildBodyCode;
    }

    internal BuilderMethod(BuilderMethod builderMethod)
    {
        MethodName = builderMethod.MethodName;
        GenericInfo = builderMethod.GenericInfo;
        Parameters = builderMethod.Parameters;
        BuildBodyCode = builderMethod.BuildBodyCode;
    }
}