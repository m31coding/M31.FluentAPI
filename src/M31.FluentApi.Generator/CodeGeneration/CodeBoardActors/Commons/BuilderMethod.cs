using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethod
{
    internal string MethodName { get; }
    internal IReadOnlyCollection<Parameter> Parameters { get; }
    internal BuildBodyCode BuildBodyCode { get; }

    internal BuilderMethod(string methodName, IReadOnlyCollection<Parameter> parameters, BuildBodyCode buildBodyCode)
    {
        MethodName = methodName;
        Parameters = parameters;
        BuildBodyCode = buildBodyCode;
    }

    internal BuilderMethod(BuilderMethod builderMethod)
    {
        MethodName = builderMethod.MethodName;
        Parameters = builderMethod.Parameters;
        BuildBodyCode = builderMethod.BuildBodyCode;
    }
}