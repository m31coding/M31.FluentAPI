using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class FluentMethods : IBuilderMethodCreator
{
    internal MethodSymbolInfo SymbolInfo { get; }
    internal FluentMethodAttributeInfo MethodAttributeInfo { get; }
    internal FluentReturnAttributeInfo? ReturnAttributeInfo { get; }

    internal FluentMethods(
        MethodSymbolInfo symbolInfo,
        FluentMethodAttributeInfo methodAttributeInfo,
        FluentReturnAttributeInfo? returnAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        MethodAttributeInfo = methodAttributeInfo;
        ReturnAttributeInfo = returnAttributeInfo;
    }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        BuilderMethod builderMethod =
            methodCreator.BuilderMethodFactory.CreateBuilderMethod(
                SymbolInfo,
                MethodAttributeInfo.FluentMethodName,
                ReturnAttributeInfo != null);
        return new BuilderMethods(builderMethod);
    }
}