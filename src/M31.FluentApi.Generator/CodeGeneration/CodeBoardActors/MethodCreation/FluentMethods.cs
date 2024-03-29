using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class FluentMethods : IBuilderMethodCreator
{
    internal MethodSymbolInfo SymbolInfo { get; }
    internal FluentMethodAttributeInfo MethodAttributeInfo { get; }

    internal FluentMethods(MethodSymbolInfo symbolInfo, FluentMethodAttributeInfo methodAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        MethodAttributeInfo = methodAttributeInfo;
    }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        List<Parameter> parameters = SymbolInfo.ParameterInfos
            .Select(i => new Parameter(i.TypeForCodeGeneration, i.ParameterName, i.DefaultValue)).ToList();

        BuilderMethod builderMethod =
            methodCreator.BuilderMethodFactory.CreateBuilderMethod(
                SymbolInfo.Name,
                MethodAttributeInfo.FluentMethodName,
                parameters);

        return new BuilderMethods(builderMethod);
    }
}