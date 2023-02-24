using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class CompoundPart
{
    internal CompoundPart(MemberSymbolInfo symbolInfo, FluentMemberAttributeInfo attributeInfo)
    {
        SymbolInfo = symbolInfo;
        AttributeInfo = attributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentMemberAttributeInfo AttributeInfo { get; }
}