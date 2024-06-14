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
        ComputeValueCode computeValueCode = GetComputeValueCode(symbolInfo, lambdaBuilderInfo);
        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(method, computeValueCode);
    }

    private static Parameter GetParameter(MemberSymbolInfo symbolInfo, LambdaBuilderInfo lambdaBuilderInfo)
    {
        string builderType = lambdaBuilderInfo.BuilderTypeForCodeGeneration;
        string builderInstanceName = lambdaBuilderInfo.BuilderInstanceName;
        string initialStepInterfaceName = lambdaBuilderInfo.InitialStepInterfaceName;

        // Func<CreateAddress.ICreateAddress, Address> address
        return new Parameter(
            $"Func<{builderType}.{initialStepInterfaceName}, " +
            $"{symbolInfo.TypeForCodeGeneration}>",
            builderInstanceName);
    }

    public static ComputeValueCode GetComputeValueCode(MemberSymbolInfo symbolInfo, LambdaBuilderInfo lambdaBuilderInfo)
    {
        // createAddress(Func<CreateAddress.ICreateAddress, Address> address)
        // {
        //     student.Address = address(CreateAddress.InitialStep());
        // }
        string builderType = lambdaBuilderInfo.BuilderTypeForCodeGeneration;
        Parameter parameter = GetParameter(symbolInfo, lambdaBuilderInfo);
        string BuildCodeWithParameter(string p) => $"{p}({builderType}.InitialStep())";
        return ComputeValueCode.Create(symbolInfo.Name, parameter, BuildCodeWithParameter);
    }
}