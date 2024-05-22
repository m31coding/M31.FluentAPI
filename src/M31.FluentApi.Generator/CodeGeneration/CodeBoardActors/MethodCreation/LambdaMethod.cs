using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class LambdaMethod : IBuilderMethodCreator
{
    internal LambdaMethod(
        MemberSymbolInfo symbolInfo,
        FluentLambdaAttributeInfo lambdaAttributeInfo)
    {
        SymbolInfo = symbolInfo;
        LambdaAttributeInfo = lambdaAttributeInfo;
    }

    internal MemberSymbolInfo SymbolInfo { get; }
    internal FluentLambdaAttributeInfo LambdaAttributeInfo { get; }

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        string builderType = LambdaAttributeInfo.BuilderInfo.BuilderTypeForCodeGeneration;
        string builderInstanceName = LambdaAttributeInfo.BuilderInfo.BuilderInstanceName;
        string initialStepInterfaceName = LambdaAttributeInfo.BuilderInfo.InitialStepInterfaceName;

        // createAddress(Func<CreateAddress.ICreateAddress, Address> address)
        // {
        //     student.Address = address(CreateAddress.InitialStep());
        // }
        Parameter parameter =
            new Parameter(
                $"Func<{builderType}.{initialStepInterfaceName}, " +
                $"{SymbolInfo.TypeForCodeGeneration}>",
                builderInstanceName);
        BuilderMethod builderMethod = methodCreator.CreateMethodWithComputedValue(
            SymbolInfo,
            LambdaAttributeInfo.Method,
            parameter,
            p => $"{p}({builderType}.InitialStep())");
        return new BuilderMethods(new List<BuilderMethod>() { builderMethod }, new HashSet<string>() { "System" });
    }
}