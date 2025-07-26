using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.DocumentationComments;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class BuilderMethodFactory
{
    private readonly InnerBodyCreationDelegates innerBodyCreationDelegates;
    private readonly TransformedComments transformedComments;

    internal BuilderMethodFactory(InnerBodyCreationDelegates innerBodyCreationDelegates, TransformedComments transformedComments)
    {
        this.innerBodyCreationDelegates = innerBodyCreationDelegates;
        this.transformedComments = transformedComments;
    }

    internal BuilderMethod CreateEmptyBuilderMethod(MemberSymbolInfo memberInfo, string methodName)
    {
        MemberCommentKey key = new MemberCommentKey(memberInfo.Name, methodName);
        Comments comments = transformedComments.GetMemberComments(key);
        return new BuilderMethod(methodName, null, new List<Parameter>(), null, (_, _, _) => new List<string>(), comments);
    }

    internal BuilderMethod CreateBuilderMethod(string methodName, ComputeValueCode computeValue)
    {
        List<Parameter> parameters = computeValue.Parameter != null
            ? new List<Parameter>() { computeValue.Parameter }
            : new List<Parameter>();

        List<string> BuildBodyCode(
            string instancePrefix,
            ReservedVariableNames reservedVariableNames,
            string? returnType)
        {
            return new List<string>()
            {
                innerBodyCreationDelegates.GetSetMemberCode(computeValue.TargetMember)
                    .BuildCode(instancePrefix, computeValue.Code),
            };
        }

        MemberCommentKey key = new MemberCommentKey(computeValue.TargetMember, methodName);
        Comments comments = transformedComments.GetMemberComments(key);
        return new BuilderMethod(methodName, null, parameters, null, BuildBodyCode, comments);
    }

    internal BuilderMethod CreateBuilderMethod(string methodName, List<ComputeValueCode> computeValues)
    {
        List<Parameter> parameters = computeValues.Select(v => v.Parameter).OfType<Parameter>().ToList();

        List<string> BuildBodyCode(
            string instancePrefix,
            ReservedVariableNames reservedVariableNames,
            string? returnType)
        {
            return computeValues
                .Select(v =>
                    innerBodyCreationDelegates.GetSetMemberCode(v.TargetMember).BuildCode(instancePrefix, v.Code))
                .ToList();
        }

        Comments comments = GetCompoundComments(methodName, computeValues.Select(v => v.TargetMember).ToArray());
        return new BuilderMethod(methodName, null, parameters, null, BuildBodyCode, comments);
    }

    private Comments GetCompoundComments(string methodName, IReadOnlyCollection<string> memberNames)
    {
        return new Comments(memberNames
            .SelectMany(n => transformedComments.GetMemberComments(new MemberCommentKey(n, methodName)).List)
            .ToArray());
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

        List<string> BuildBodyCode(
            string instancePrefix,
            ReservedVariableNames reservedVariableNames,
            string? returnType)
        {
            return innerBodyCreationDelegates.GetCallMethodCode(methodSymbolInfo)
                .BuildCode(instancePrefix, parameters, reservedVariableNames, returnType);
        }

        Comments comments = transformedComments.GetMethodComments(methodSymbolInfo);

        return new BuilderMethod(
            methodName,
            methodSymbolInfo.GenericInfo,
            parameters,
            returnTypeToRespect,
            BuildBodyCode,
            comments);
    }
}