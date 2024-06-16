using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class MemberMethod : IBuilderMethodCreator
{
    internal MemberMethod(MemberSymbolInfo symbolInfo, FluentMemberAttributeInfo memberAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        MemberAttributeInfo = memberAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentMemberAttributeInfo MemberAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        List<BuilderMethod> builderMethods = new List<BuilderMethod>();
        HashSet<string> requiredUsings = new HashSet<string>();

        BuilderMethod builderMethod = methodCreator.CreateMethod(SymbolInfo, MemberAttributeInfo.FluentMethodName);
        builderMethods.Add(builderMethod);

        if (MemberAttributeInfo.LambdaBuilderInfo != null)
        {
            BuilderMethod lambdaBuilderMethod = LambdaMethod.CreateLambdaBuilderMethod(
                methodCreator, MemberAttributeInfo.Method, SymbolInfo, MemberAttributeInfo.LambdaBuilderInfo);
            builderMethods.Add(lambdaBuilderMethod);
            requiredUsings.Add("System");
        }

        return new BuilderMethods(builderMethods, requiredUsings);
    }
}