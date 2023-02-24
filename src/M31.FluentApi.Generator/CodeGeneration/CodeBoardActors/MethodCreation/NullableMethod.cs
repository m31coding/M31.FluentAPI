using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class NullableMethod : IBuilderMethodCreator
{
    internal NullableMethod(MemberSymbolInfo symbolInfo, FluentNullableAttributeInfo nullableAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        NullableAttributeInfo = nullableAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentNullableAttributeInfo NullableAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        BuilderMethod builderMethod =
            methodCreator.CreateMethodWithFixedValue(SymbolInfo, NullableAttributeInfo.Method, "null");
        return new BuilderMethods(builderMethod);
    }
}