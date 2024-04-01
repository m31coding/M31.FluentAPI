using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethodFactory
{
    private readonly Dictionary<string, SetMemberCode> memberToSetMemberCode;
    private readonly Dictionary<MethodIdentity, CallMethodCode> methodToCallMethodCode;

    internal BuilderMethodFactory(
        Dictionary<string, SetMemberCode> memberToSetMemberCode,
        Dictionary<MethodIdentity, CallMethodCode> methodToCallMethodCode)
    {
        this.memberToSetMemberCode = memberToSetMemberCode;
        this.methodToCallMethodCode = methodToCallMethodCode;
    }

    internal BuilderMethod CreateBuilderMethod(string methodName)
    {
        return new BuilderMethod(methodName, new List<Parameter>(), _ => new List<string>());
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

        return new BuilderMethod(methodName, parameters, BuildBodyCode);
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

        return new BuilderMethod(methodName, parameters, BuildBodyCode);
    }

    internal BuilderMethod CreateBuilderMethod(string methodSymbolName, string methodName, List<Parameter> parameters)
    {
        MethodIdentity methodIdentity = MethodIdentity.Create(methodSymbolName, parameters.Select(p => p.Type));

        List<string> BuildBodyCode(string instancePrefix)
        {
            return methodToCallMethodCode[methodIdentity].BuildCode(instancePrefix, parameters);
        }

        return new BuilderMethod(methodName, parameters, BuildBodyCode);
    }
}