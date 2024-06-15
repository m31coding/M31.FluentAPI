using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

internal abstract class CollectionMethodCreator
{
    private readonly FluentCollectionAttributeInfo collectionAttributeInfo;
    private readonly string genericTypeArgument;
    private readonly MemberSymbolInfo symbolInfo;

    internal CollectionMethodCreator(
        FluentCollectionAttributeInfo collectionAttributeInfo,
        string genericTypeArgument,
        MemberSymbolInfo symbolInfo)
    {
        this.collectionAttributeInfo = collectionAttributeInfo;
        this.genericTypeArgument = genericTypeArgument;
        this.symbolInfo = symbolInfo;
    }

    internal BuilderMethod? CreateWithItemsMethod(MethodCreator methodCreator)
    {
        return !ShouldCreateWithItemsMethod() ? null :
            methodCreator.CreateMethod(symbolInfo, collectionAttributeInfo.WithItems);
    }

    private bool ShouldCreateWithItemsMethod()
    {
        return symbolInfo.TypeForCodeGeneration != $"{genericTypeArgument}[]" &&
               symbolInfo.TypeForCodeGeneration != $"{genericTypeArgument}[]?";
    }

    internal BuilderMethod CreateWithItemsParamsMethod(MethodCreator methodCreator)
    {
        Parameter parameter = new Parameter(
            symbolInfo.IsNullable ? $"{genericTypeArgument}[]?" : $"{genericTypeArgument}[]",
            symbolInfo.NameInCamelCase,
            null,
            null,
            new ParameterAnnotations(ParameterKinds.Params));

        return methodCreator.CreateMethodWithComputedValue(
            symbolInfo,
            collectionAttributeInfo.WithItems,
            parameter,
            p => CreateCollectionFromArray(genericTypeArgument, p));
    }

    internal BuilderMethod? CreateWithItemsLambdaParamsMethod(MethodCreator methodCreator)
    {
        if (collectionAttributeInfo.LambdaBuilderInfo == null)
        {
            return null;
        }

        ComputeValueCode lambdaCode = LambdaMethod.GetComputeValueCode(
            symbolInfo.CollectionType!.GenericTypeArgument,
            collectionAttributeInfo.SingularNameInCamelCase,
            symbolInfo.Name,
            collectionAttributeInfo.LambdaBuilderInfo);

        string questionMarkIfNullable = symbolInfo.IsNullable ? "?" : string.Empty;
        string parameterType = $"{lambdaCode.Parameter!.Type}[]{questionMarkIfNullable}";
        string parameterName = LambdaMethod.GetFullParameterName(symbolInfo.NameInCamelCase);

        Parameter parameter = new Parameter(
            parameterType,
            parameterName,
            null,
            null,
            new ParameterAnnotations(ParameterKinds.Params));

        ComputeValueCode computeValueCode = ComputeValueCode.Create(
            lambdaCode.TargetMember,
            parameter,
            p => CreateCollectionFromEnumerable(
                genericTypeArgument,
                $"{p}{questionMarkIfNullable}.Select({lambdaCode.Parameter!.Name} => {lambdaCode.Code})"));

        RequiredUsings.Add("System");
        RequiredUsings.Add("System.Linq");

        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(
            collectionAttributeInfo.WithItems,
            computeValueCode);
    }

    internal BuilderMethod CreateWithItemMethod(MethodCreator methodCreator)
    {
        Parameter parameter = new Parameter(genericTypeArgument, collectionAttributeInfo.SingularNameInCamelCase);
        return methodCreator.CreateMethodWithComputedValue(
            symbolInfo,
            collectionAttributeInfo.WithItem,
            parameter,
            p => CreateCollectionFromSingleItem(genericTypeArgument, p));
    }

    internal BuilderMethod? CreateWithItemLambdaMethod(MethodCreator methodCreator)
    {
        if (collectionAttributeInfo.LambdaBuilderInfo == null)
        {
            return null;
        }

        ComputeValueCode lambdaCode = LambdaMethod.GetComputeValueCode(
            symbolInfo.CollectionType!.GenericTypeArgument,
            collectionAttributeInfo.SingularNameInCamelCase,
            symbolInfo.Name,
            collectionAttributeInfo.LambdaBuilderInfo);

        ComputeValueCode computeValueCode = ComputeValueCode.Create(
            lambdaCode.TargetMember,
            lambdaCode.Parameter!,
            _ => CreateCollectionFromSingleItem(genericTypeArgument, lambdaCode.Code));

        RequiredUsings.Add("System");

        return methodCreator.BuilderMethodFactory.CreateBuilderMethod(collectionAttributeInfo.WithItem,
            computeValueCode);
    }

    internal BuilderMethod CreateWithZeroItemsMethod(MethodCreator methodCreator)
    {
        string collectionWithZeroItemsCode = CreateCollectionWithZeroItems(genericTypeArgument);
        return methodCreator.CreateMethodWithFixedValue(
            symbolInfo,
            collectionAttributeInfo.WithZeroItems,
            collectionWithZeroItemsCode);
    }

    protected abstract string CreateCollectionFromArray(string genericTypeArgument, string arrayParameter);
    protected abstract string CreateCollectionFromEnumerable(string genericTypeArgument, string enumerableParameter);
    protected abstract string CreateCollectionFromSingleItem(string genericTypeArgument, string itemParameter);
    protected abstract string CreateCollectionWithZeroItems(string genericTypeArgument);
    internal HashSet<string> RequiredUsings { get; } = new HashSet<string>();
}