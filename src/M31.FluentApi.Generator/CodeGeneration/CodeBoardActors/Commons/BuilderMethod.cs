using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethod
{
    internal string MethodName { get; }
    internal GenericInfo? GenericInfo { get; }
    internal IReadOnlyCollection<Parameter> Parameters { get; }
    internal string? ReturnTypeToRespect { get; }
    internal BuildBodyCode BuildBodyCode { get; }
    internal Comments Comments { get; }

    internal BuilderMethod(
        string methodName,
        GenericInfo? genericInfo,
        IReadOnlyCollection<Parameter> parameters,
        string? returnTypeToRespect,
        BuildBodyCode buildBodyCode,
        Comments comments)
    {
        MethodName = methodName;
        GenericInfo = genericInfo;
        Parameters = parameters;
        ReturnTypeToRespect = returnTypeToRespect;
        BuildBodyCode = buildBodyCode;
        Comments = comments;
    }

    internal BuilderMethod(BuilderMethod builderMethod)
    {
        MethodName = builderMethod.MethodName;
        GenericInfo = builderMethod.GenericInfo;
        Parameters = builderMethod.Parameters;
        ReturnTypeToRespect = builderMethod.ReturnTypeToRespect;
        BuildBodyCode = builderMethod.BuildBodyCode;
        Comments = builderMethod.Comments;
    }
}