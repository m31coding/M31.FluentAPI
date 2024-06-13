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
        BuilderMethod memberBuilderMethod =
            methodCreator.CreateMethod(SymbolInfo, LambdaAttributeInfo.FluentMethodName);

        BuilderMethod lambdaBuilderMethod = CreateLambdaBuilderMethod(
            methodCreator, LambdaAttributeInfo.Method, SymbolInfo, LambdaAttributeInfo.BuilderInfo);

        return new BuilderMethods(
            new List<BuilderMethod>() { memberBuilderMethod, lambdaBuilderMethod },
            new HashSet<string>() { "System" });
    }

    public static BuilderMethod CreateLambdaBuilderMethod(
        MethodCreator methodCreator,
        string method,
        MemberSymbolInfo symbolInfo,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        string builderType = lambdaBuilderInfo.BuilderTypeForCodeGeneration;
        string builderInstanceName = lambdaBuilderInfo.BuilderInstanceName;
        string initialStepInterfaceName = lambdaBuilderInfo.InitialStepInterfaceName;

        // createAddress(Func<CreateAddress.ICreateAddress, Address> address)
        // {
        //     student.Address = address(CreateAddress.InitialStep());
        // }
        Parameter parameter =
            new Parameter(
                $"Func<{builderType}.{initialStepInterfaceName}, " +
                $"{symbolInfo.TypeForCodeGeneration}>",
                builderInstanceName);

        return methodCreator.CreateMethodWithComputedValue(
            symbolInfo,
            method,
            parameter,
            p => $"{p}({builderType}.InitialStep())");
    }
}