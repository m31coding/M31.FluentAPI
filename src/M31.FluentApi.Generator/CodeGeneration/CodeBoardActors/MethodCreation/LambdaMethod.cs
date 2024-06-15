using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
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
        string methodName,
        MemberSymbolInfo symbolInfo,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        ComputeValueCode computeValueCode = GetComputeValueCode(symbolInfo, lambdaBuilderInfo);
        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(methodName, computeValueCode);
    }

    internal static Parameter GetParameter(
        string parameterType,
        string parameterName,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        string builderType = lambdaBuilderInfo.BuilderTypeForCodeGeneration;
        string initialStepInterfaceName = lambdaBuilderInfo.InitialStepInterfaceName;
        string fullParameterName = GetFullParameterName(parameterName);

        // Func<CreateAddress.ICreateAddress, Address> address
        return new Parameter(
            $"Func<{builderType}.{initialStepInterfaceName}, " +
            $"{parameterType}>",
            fullParameterName);
    }

    internal static string GetFullParameterName(string parameterName)
    {
        return $"create{parameterName.FirstCharToUpper()}";
    }

    internal static ComputeValueCode GetComputeValueCode(
        MemberSymbolInfo symbolInfo,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        return GetComputeValueCode(
            symbolInfo.TypeForCodeGeneration, symbolInfo.NameInCamelCase, symbolInfo.Name, lambdaBuilderInfo);
    }

    internal static ComputeValueCode GetComputeValueCode(
        string parameterType,
        string parameterName,
        string targetMember,
        LambdaBuilderInfo lambdaBuilderInfo)
    {
        // createAddress(Func<CreateAddress.ICreateAddress, Address> address)
        // {
        //     student.Address = address(CreateAddress.InitialStep());
        // }
        string builderType = lambdaBuilderInfo.BuilderTypeForCodeGeneration;
        Parameter parameter = GetParameter(
            parameterType, parameterName, lambdaBuilderInfo);
        string BuildCodeWithParameter(string p) => $"{p}({builderType}.InitialStep())";
        return ComputeValueCode.Create(targetMember, parameter, BuildCodeWithParameter);
    }
}