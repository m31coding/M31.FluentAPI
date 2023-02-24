using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class PredicateMethods : IBuilderMethodCreator
{
    internal PredicateMethods(MemberSymbolInfo symbolInfo, FluentPredicateAttributeInfo predicateAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        PredicateAttributeInfo = predicateAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentPredicateAttributeInfo PredicateAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        BuilderMethod builderMethod1 =
            methodCreator.CreateMethodWithDefaultValue(SymbolInfo, PredicateAttributeInfo.Method, "true");
        BuilderMethod builderMethod2 =
            methodCreator.CreateMethodWithFixedValue(SymbolInfo, PredicateAttributeInfo.NegatedMethod, "false");

        return new BuilderMethods(new List<BuilderMethod> { builderMethod1, builderMethod2 }, new HashSet<string>());
    }
}