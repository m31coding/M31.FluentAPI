using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class DefaultMethod : IBuilderMethodCreator
{
    internal DefaultMethod(MemberSymbolInfo symbolInfo, FluentDefaultAttributeInfo defaultAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        DefaultAttributeInfo = defaultAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentDefaultAttributeInfo DefaultAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        return new BuilderMethods(methodCreator.CreateMethodThatDoesNothing(DefaultAttributeInfo.Method));
    }
}