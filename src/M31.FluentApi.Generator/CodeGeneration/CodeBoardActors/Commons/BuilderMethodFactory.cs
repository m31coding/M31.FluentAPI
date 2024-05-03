using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethodFactory
{
    private readonly IReadOnlyDictionary<string, SetMemberCode> memberToSetMemberCode;
    private readonly CodeBoard codeBoard;

    internal BuilderMethodFactory(
        IReadOnlyDictionary<string, SetMemberCode> memberToSetMemberCode,
        CodeBoard codeBoard)
    {
        this.memberToSetMemberCode = memberToSetMemberCode;
        this.codeBoard = codeBoard;
    }

    internal BuilderMethod CreateBuilderMethod(string methodName)
    {
        return new BuilderMethod(methodName, null, new List<Parameter>(), _ => new List<string>());
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
                memberToSetMemberCode[computeValue.TargetMember].BuildCode(instancePrefix, computeValue.Code),
            };
        }

        return new BuilderMethod(methodName, null, parameters, BuildBodyCode);
    }

    internal BuilderMethod CreateBuilderMethod(string methodName, List<ComputeValueCode> computeValues)
    {
        List<Parameter> parameters = computeValues.Select(v => v.Parameter).OfType<Parameter>().ToList();

        List<string> BuildBodyCode(string instancePrefix)
        {
            return computeValues
                .Select(v => memberToSetMemberCode[v.TargetMember].BuildCode(instancePrefix, v.Code))
                .ToList();
        }

        return new BuilderMethod(methodName, null, parameters, BuildBodyCode);
    }

    internal BuilderMethod CreateBuilderMethod(MethodSymbolInfo methodSymbolInfo, string methodName)
    {
        List<Parameter> parameters = methodSymbolInfo.ParameterInfos
            .Select(i => new Parameter(
                i.TypeForCodeGeneration,
                i.ParameterName,
                i.DefaultValue,
                new ParameterAnnotations(i.ParameterKinds)))
            .ToList();

        List<string> BuildBodyCode(string instancePrefix)
        {
            return codeBoard.GetCallMethodCode(methodSymbolInfo).BuildCode(instancePrefix, parameters);
        }

        return new BuilderMethod(methodName, methodSymbolInfo.GenericInfo, parameters, BuildBodyCode);
    }
}