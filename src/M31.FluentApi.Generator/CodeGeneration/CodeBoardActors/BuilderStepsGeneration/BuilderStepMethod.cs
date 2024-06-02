using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal abstract class BuilderStepMethod : BuilderMethod
{
    protected BuilderStepMethod(BuilderMethod builderMethod)
        : base(builderMethod)
    {
    }

    internal abstract Method BuildMethodCode(BuilderAndTargetInfo info, ReservedVariableNames reservedVariableNames);

    protected Method CreateMethod(string defaultReturnType, params string[] modifiers)
    {
        MethodSignature methodSignature = CreateMethodSignature(defaultReturnType, null, modifiers);
        return new Method(methodSignature);
    }

    protected InterfaceMethod CreateInterfaceMethod(
        string interfaceName,
        BaseInterface? baseInterface,
        string defaultReturnType,
        params string[] modifiers)
    {
        MethodSignature methodSignature = CreateMethodSignature(defaultReturnType, interfaceName, modifiers);
        return new InterfaceMethod(methodSignature, interfaceName, baseInterface);
    }

    private MethodSignature CreateMethodSignature(
        string defaultReturnType,
        string? explicitInterfacePrefix,
        params string[] modifiers)
    {
        string returnType = ReturnTypeToRespect ?? defaultReturnType;

        MethodSignature signature = MethodSignature.Create(returnType, MethodName, explicitInterfacePrefix, false);
        signature.AddModifiers(modifiers);

        if (GenericInfo != null)
        {
            foreach (GenericTypeParameter genericTypeParameter in GenericInfo.Parameters)
            {
                signature.AddGenericParameter(
                    genericTypeParameter.ParameterName,
                    genericTypeParameter.Constraints.GetConstraintsForCodeGeneration());
            }
        }

        foreach (Parameter parameter in Parameters)
        {
            signature.AddParameter(parameter);
        }

        return signature;
    }

    protected void CreateBody(Method method, string instancePrefix, ReservedVariableNames reservedVariableNames)
    {
        List<string> bodyCode = BuildBodyCode(instancePrefix, reservedVariableNames, ReturnTypeToRespect);
        foreach (string bodyLine in bodyCode)
        {
            method.AppendBodyLine(bodyLine);
        }
    }

    protected void CreateReturnStatement(Method method, string defaultReturnStatement)
    {
        if (ReturnTypeToRespect == null)
        {
            method.AppendBodyLine(defaultReturnStatement);
        }
    }
}