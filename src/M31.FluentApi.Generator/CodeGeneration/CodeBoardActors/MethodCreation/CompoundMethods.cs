using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class CompoundMethods : IBuilderMethodCreator
{
    internal CompoundMethods(string fluentMethodName, IReadOnlyCollection<CompoundPart> parts)
    {
        FluentMethodName = fluentMethodName;
        Parts = parts;
    }

    internal string FluentMethodName { get; }
    internal IReadOnlyCollection<CompoundPart> Parts { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        List<BuilderMethod> compoundMethods = new List<BuilderMethod>();
        HashSet<string> requiredUsings = new HashSet<string>();

        compoundMethods.Add(CreateStandardCompoundBuilderMethod(methodCreator));

        BuilderMethod? lambdaCompoundBuilderMethod = TryCreateLambdaCompoundBuilderMethod(methodCreator);
        if (lambdaCompoundBuilderMethod != null)
        {
            compoundMethods.Add(lambdaCompoundBuilderMethod);
            requiredUsings.Add("System");
        }

        return new BuilderMethods(compoundMethods, requiredUsings);
    }

    private BuilderMethod CreateStandardCompoundBuilderMethod(MethodCreator methodCreator)
    {
        List<ComputeValueCode> computeValues = new List<ComputeValueCode>(Parts.Count);

        foreach (CompoundPart compoundPart in Parts.OrderBy(p => p.AttributeInfo.ParameterPosition))
        {
            computeValues.Add(GetStandardComputeValueCode(compoundPart));
        }

        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(FluentMethodName, computeValues);
    }

    private static ComputeValueCode GetStandardComputeValueCode(
        CompoundPart compoundPart,
        ReservedVariableNames? reservedParameterNames = null)
    {
        string parameterName = compoundPart.SymbolInfo.NameInCamelCase;
        if (reservedParameterNames != null)
        {
            parameterName = reservedParameterNames.GetNewLocalVariableName(parameterName);
        }

        Parameter parameter =
            new Parameter(compoundPart.SymbolInfo.TypeForCodeGeneration, parameterName);
        return ComputeValueCode.Create(compoundPart.SymbolInfo.Name, parameter);
    }

    private BuilderMethod? TryCreateLambdaCompoundBuilderMethod(MethodCreator methodCreator)
    {
        if (Parts.All(p => p.AttributeInfo.LambdaBuilderInfo == null))
        {
            return null;
        }

        ReservedVariableNames reservedParameterNames = new ReservedVariableNames();
        List<ComputeValueCode> computeValues = new List<ComputeValueCode>(Parts.Count);

        foreach (CompoundPart compoundPart in Parts.OrderBy(p => p.AttributeInfo.ParameterPosition))
        {
            if (compoundPart.AttributeInfo.LambdaBuilderInfo != null)
            {
                computeValues.Add(LambdaMethod.GetComputeValueCode(
                    compoundPart.SymbolInfo,
                    compoundPart.AttributeInfo.LambdaBuilderInfo,
                    reservedParameterNames));
            }
            else
            {
                computeValues.Add(GetStandardComputeValueCode(compoundPart, reservedParameterNames));
            }
        }

        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(FluentMethodName, computeValues);
    }
}