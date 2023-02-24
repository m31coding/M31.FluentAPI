using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class MethodCreator
{
    internal BuilderMethodFactory BuilderMethodFactory { get; }

    internal MethodCreator(BuilderMethodFactory builderMethodFactory)
    {
        this.BuilderMethodFactory = builderMethodFactory;
    }

    internal BuilderMethod CreateMethod(MemberSymbolInfo symbolInfo, string methodName)
    {
        return BuilderMethodFactory
            .CreateBuilderMethod(methodName, ComputeValueCode.Create(symbolInfo.Name, GetStandardParameter(symbolInfo)));
    }

    internal BuilderMethod CreateMethodWithDefaultValue(
        MemberSymbolInfo symbolInfo,
        string methodName,
        string defaultValue)
    {
        return BuilderMethodFactory.CreateBuilderMethod(
            methodName,
            ComputeValueCode.Create(symbolInfo.Name, GetStandardParameter(symbolInfo, defaultValue)));
    }

    internal BuilderMethod CreateMethodWithFixedValue(MemberSymbolInfo symbolInfo, string methodName, string fixedValue)
    {
        return BuilderMethodFactory
            .CreateBuilderMethod(methodName, ComputeValueCode.Create(symbolInfo.Name, fixedValue));
    }

    internal BuilderMethod CreateMethodWithComputedValue(
        MemberSymbolInfo symbolInfo,
        string methodName,
        Parameter parameter,
        Func<string, string> buildCodeWithParameter)
    {
        return BuilderMethodFactory
            .CreateBuilderMethod(methodName, ComputeValueCode.Create(symbolInfo.Name, parameter, buildCodeWithParameter));
    }

    internal BuilderMethod CreateMethodThatDoesNothing(string methodName)
    {
        return BuilderMethodFactory
            .CreateBuilderMethod(methodName);
    }

    private Parameter GetStandardParameter(MemberSymbolInfo symbolInfo, string? defaultValue = null)
    {
        return new Parameter(symbolInfo.TypeForCodeGeneration, symbolInfo.NameInCamelCase, defaultValue);
    }
}