using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

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
        List<ComputeValueCode> computeValues = new List<ComputeValueCode>(Parts.Count);

        foreach (CompoundPart compoundPart in Parts.OrderBy(p => p.AttributeInfo.ParameterPosition))
        {
            Parameter parameter =
                new Parameter(compoundPart.SymbolInfo.TypeForCodeGeneration, compoundPart.SymbolInfo.NameInCamelCase);
            ComputeValueCode computeValueCode = ComputeValueCode.Create(compoundPart.SymbolInfo.Name, parameter);
            computeValues.Add(computeValueCode);
        }

        BuilderMethod builderMethod =
            methodCreator.BuilderMethodFactory.CreateBuilderMethod(FluentMethodName, computeValues);
        return new BuilderMethods(builderMethod);
    }
}