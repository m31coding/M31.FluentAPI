using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

internal class BuilderMethodsCreator : IBuilderMethodCreator
{
    private readonly FluentApiInfoGroup group;
    private readonly CodeBoard codeBoard;

    internal BuilderMethodsCreator(FluentApiInfoGroup group, CodeBoard codeBoard)
    {
        this.group = group;
        this.codeBoard = codeBoard;
    }

    internal string FluentMethodName => group.FluentMethodName;
    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos => group.FluentApiInfos;

    public BuilderMethods CreateBuilderMethods(MethodCreator methodCreator)
    {
        BuilderMethods builderMethods = CreateBuilderMethodsInternal(methodCreator);
        codeBoard.GroupsToMethods[group] = builderMethods;
        return builderMethods;
    }

    private BuilderMethods CreateBuilderMethodsInternal(MethodCreator methodCreator)
    {
        if (FluentApiInfos.Count == 0)
        {
            return new BuilderMethods();
        }

        if (FluentApiInfos.Count == 1)
        {
            BuilderMethods builderMethods = CreateBuilderMethods(methodCreator, FluentApiInfos.First());
            builderMethods.Add(CreateOrthogonalBuilderMethods(methodCreator, FluentApiInfos.First()));
            return builderMethods;
        }

        if (group.IsCompoundGroup)
        {
            if (FluentApiInfos.Sum(i => i.OrthogonalAttributeInfos.Count) > 0)
            {
                throw new GenerationException("Orthogonal attribute infos are not allowed for compounds.");
            }

            return CreateCompoundMethods(methodCreator);
        }

        throw new ArgumentException(
            $"Unable to create methods for group with method name {FluentMethodName}");
    }

    private BuilderMethods CreateBuilderMethods(MethodCreator methodCreator, FluentApiInfo info)
    {
        BuilderMethods builderMethods =
            CreateMethodCreator(info).CreateBuilderMethods(methodCreator);
        AddAttributeRelationsToCodeBoard(builderMethods.Methods, info);
        return builderMethods;
    }

    private BuilderMethods CreateOrthogonalBuilderMethods(
        MethodCreator methodCreator,
        FluentApiInfo info)
    {
        FluentDefaultAttributeInfo[] defaultAttributeInfos =
            info.OrthogonalAttributeInfos.OfType<FluentDefaultAttributeInfo>().ToArray();

        FluentDefaultAttributeInfo? defaultAttributeInfo = defaultAttributeInfos.Length == 0 ? null :
            defaultAttributeInfos.Length == 1 ? defaultAttributeInfos[0] :
            throw new GenerationException("Expected at most one default attribute info.");

        FluentNullableAttributeInfo[] nullableAttributeInfos =
            info.OrthogonalAttributeInfos.OfType<FluentNullableAttributeInfo>().ToArray();

        FluentNullableAttributeInfo? nullableAttributeInfo = nullableAttributeInfos.Length == 0 ? null :
            nullableAttributeInfos.Length == 1 ? nullableAttributeInfos[0] :
            throw new GenerationException("Expected at most one nullable attribute info.");

        if (defaultAttributeInfo == null && nullableAttributeInfo == null)
        {
            return new BuilderMethods();
        }

        if (info.SymbolInfo is MethodSymbolInfo)
        {
            throw new GenerationException($"Orthogonal fluent attributes can not be applied to fluent methods.");
        }

        BuilderMethods builderMethods = new BuilderMethods();

        if (defaultAttributeInfo != null)
        {
            builderMethods.Add(CreateOrthogonalBuilderMethods(methodCreator, info, defaultAttributeInfo));
        }

        if (nullableAttributeInfo != null)
        {
            builderMethods.Add(CreateOrthogonalBuilderMethods(methodCreator, info, nullableAttributeInfo));
        }

        return builderMethods;
    }

    private BuilderMethods CreateOrthogonalBuilderMethods(
        MethodCreator methodCreator,
        FluentApiInfo info,
        OrthogonalAttributeInfoBase orthogonalAttributeInfo)
    {
        BuilderMethods builderMethods =
            CreateOrthogonalMethodCreator(orthogonalAttributeInfo, info.SymbolInfo).CreateBuilderMethods(methodCreator);
        AddAttributeRelationsToCodeBoard(builderMethods.Methods, info);
        return builderMethods;
    }

    private IBuilderMethodCreator CreateMethodCreator(FluentApiInfo info)
    {
        AttributeInfoBase attributeInfo = info.AttributeInfo;
        FluentApiSymbolInfo symbolInfo = info.SymbolInfo;

        return attributeInfo switch
        {
            FluentMemberAttributeInfo memberAttributeInfo
                => new MemberMethod((MemberSymbolInfo)symbolInfo, memberAttributeInfo),

            FluentPredicateAttributeInfo predicateAttributeInfo
                => new PredicateMethods((MemberSymbolInfo)symbolInfo, predicateAttributeInfo),

            FluentCollectionAttributeInfo collectionAttributeInfo
                => new CollectionMethods((MemberSymbolInfo)symbolInfo, collectionAttributeInfo),

            FluentLambdaAttributeInfo lambdaAttributeInfo
                => new LambdaMethod((MemberSymbolInfo)symbolInfo, lambdaAttributeInfo),

            FluentMethodAttributeInfo methodAttributeInfo
                => new FluentMethods(
                    (MethodSymbolInfo)symbolInfo,
                    methodAttributeInfo,
                    info.AdditionalInfo.FluentReturnAttributeInfo),

            _ => throw new ArgumentException($"Unknown attribute info type: {attributeInfo.GetType()}")
        };
    }

    private IBuilderMethodCreator CreateOrthogonalMethodCreator(
        OrthogonalAttributeInfoBase attributeInfo,
        FluentApiSymbolInfo symbolInfo)
    {
        return attributeInfo switch
        {
            FluentNullableAttributeInfo nullableAttributeInfo
                => new NullableMethod((MemberSymbolInfo)symbolInfo, nullableAttributeInfo),

            FluentDefaultAttributeInfo defaultAttributeInfo
                => new DefaultMethod((MemberSymbolInfo)symbolInfo, defaultAttributeInfo),

            _ => throw new ArgumentException($"Unknown orthogonal attribute info type: {attributeInfo.GetType()}")
        };
    }

    private BuilderMethods CreateCompoundMethods(MethodCreator methodCreator)
    {
        return new CompoundMethods(FluentMethodName, FluentApiInfos.Select(CreateCompoundPart).ToArray())
            .CreateBuilderMethods(methodCreator);
    }

    private static CompoundPart CreateCompoundPart(FluentApiInfo info)
    {
        if (info.AttributeInfo is not FluentMemberAttributeInfo memberAttributeInfo)
        {
            throw new GenerationException($"Compounds can only be created from member attribute infos. " +
                                          $"Either change the attribute type or choose a different method name.");
        }

        return new CompoundPart((MemberSymbolInfo)info.SymbolInfo, memberAttributeInfo);
    }

    private void AddAttributeRelationsToCodeBoard(IReadOnlyCollection<BuilderMethod> builderMethods, FluentApiInfo info)
    {
        foreach (BuilderMethod builderMethod in builderMethods)
        {
            codeBoard.BuilderMethodToAttributeData[builderMethod] = info.AdditionalInfo.MainAttributeData;
        }
    }
}