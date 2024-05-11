using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethodFactory
{
    private readonly InnerBodyCreationDelegates innerBodyCreationDelegates;

    internal BuilderMethodFactory(InnerBodyCreationDelegates innerBodyCreationDelegates)
    {
        this.innerBodyCreationDelegates = innerBodyCreationDelegates;
    }

    internal BuilderMethod CreateBuilderMethod(string methodName)
    {
        return new BuilderMethod(methodName, null, new List<Parameter>(), null, _ => new List<string>());
    }

    internal BuilderMethod CreateBuilderMethod(string methodName, ComputeValueCode computeValue)
    {
        List<Parameter> parameters = computeValue.Parameter != null
            ? new List<Parameter>() { computeValue.Parameter }
            : new List<Parameter>();

        List<string> BuildBodyCode(string instancePrefix)
        {
            return new List<string>()
            {
                innerBodyCreationDelegates.GetSetMemberCode(computeValue.TargetMember)
                    .BuildCode(instancePrefix, computeValue.Code),
            };
        }

        return new BuilderMethod(methodName, null, parameters, null, BuildBodyCode);
    }

    internal BuilderMethod CreateBuilderMethod(string methodName, List<ComputeValueCode> computeValues)
    {
        List<Parameter> parameters = computeValues.Select(v => v.Parameter).OfType<Parameter>().ToList();

        List<string> BuildBodyCode(string instancePrefix)
        {
            return computeValues
                .Select(v =>
                    innerBodyCreationDelegates.GetSetMemberCode(v.TargetMember).BuildCode(instancePrefix, v.Code))
                .ToList();
        }

        return new BuilderMethod(methodName, null, parameters, null, BuildBodyCode);
    }

    internal BuilderMethod CreateBuilderMethod(
        MethodSymbolInfo methodSymbolInfo,
        string methodName,
        bool respectReturnType)
    {
        List<Parameter> parameters = methodSymbolInfo.ParameterInfos
            .Select(i => new Parameter(
                i.TypeForCodeGeneration,
                i.ParameterName,
                i.DefaultValue,
                i.GenericTypeParameterPosition,
                new ParameterAnnotations(i.ParameterKinds)))
            .ToList();

        string? returnTypeToRespect = respectReturnType ? methodSymbolInfo.ReturnType : null;

        List<string> BuildBodyCode(string instancePrefix)
        {
            return innerBodyCreationDelegates.GetCallMethodCode(methodSymbolInfo).BuildCode(instancePrefix, parameters);
        }

        return new BuilderMethod(
            methodName,
            methodSymbolInfo.GenericInfo,
            parameters,
            returnTypeToRespect,
            BuildBodyCode);
    }
}