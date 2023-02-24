using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class MemberMethods : IBuilderMethodCreator
{
    internal MemberMethods(MemberSymbolInfo symbolInfo, FluentMemberAttributeInfo memberAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        MemberAttributeInfo = memberAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentMemberAttributeInfo MemberAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        BuilderMethod builderMethod = methodCreator.CreateMethod(SymbolInfo, MemberAttributeInfo.FluentMethodName);
        return new BuilderMethods(builderMethod);
    }
}