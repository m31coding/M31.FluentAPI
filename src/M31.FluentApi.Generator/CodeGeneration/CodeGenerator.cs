using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.FluentApiComments;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators;
using BuilderMethods = M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.BuilderMethods;

namespace M31.FluentApi.Generator.CodeGeneration;

internal static class CodeGenerator
{
    internal static CodeGeneratorResult GenerateCode(FluentApiClassInfo classInfo, CancellationToken cancellationToken)
    {
        BuilderAndTargetInfo builderAndTargetInfo = CreateBuilderAndTargetInfo(classInfo);
        CodeBoard codeBoard = CreateCodeBoard(classInfo, cancellationToken, builderAndTargetInfo);

        List<ICodeBoardActor> actors = new List<ICodeBoardActor>()
        {
            new CommentsGenerator(),
            new EntityFieldGenerator(),
            new ConstructorGenerator(),
            new InnerBodyCreator(),
            new ForkCreator(),
            new DuplicateMethodsChecker(),
            new InitialStepMethodGenerator(),
            new BuilderGenerator(),
        };

        foreach (ICodeBoardActor actor in actors)
        {
            if (cancellationToken.IsCancellationRequested || codeBoard.HasErrors)
            {
                break;
            }

            actor.Modify(codeBoard);
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return CodeGeneratorResult.Cancelled();
        }

        if (codeBoard.HasErrors)
        {
             return CodeGeneratorResult.WithErrors(codeBoard.Diagnostics);
        }

        return new CodeGeneratorResult(codeBoard.CodeFile.ToString(), codeBoard.Diagnostics);
    }

    private static BuilderAndTargetInfo CreateBuilderAndTargetInfo(FluentApiClassInfo classInfo)
    {
        BuilderAndTargetInfo builderAndTargetInfo =
            new BuilderAndTargetInfo(
                classInfo.Name,
                classInfo.Namespace,
                classInfo.GenericInfo,
                classInfo.IsStruct,
                classInfo.IsInternal,
                classInfo.ConstructorInfo,
                classInfo.BuilderClassName);
        return builderAndTargetInfo;
    }

    private static CodeBoard CreateCodeBoard(FluentApiClassInfo classInfo, CancellationToken cancellationToken,
        BuilderAndTargetInfo builderAndTargetInfo)
    {
        CodeBoard codeBoard = CodeBoard.Create(
            builderAndTargetInfo,
            classInfo.FluentApiInfos,
            classInfo.AdditionalInfo.FluentApiInfoGroups,
            classInfo.UsingStatements,
            classInfo.NewLineString,
            cancellationToken);
        return codeBoard;
    }

    internal static Dictionary<FluentApiInfoGroup, BuilderMethods> GenerateBuilderMethods(
        FluentApiClassInfo classInfo,
        CancellationToken cancellationToken)
    {
        BuilderAndTargetInfo builderAndTargetInfo = CreateBuilderAndTargetInfo(classInfo);
        CodeBoard codeBoard = CreateCodeBoard(classInfo, cancellationToken, builderAndTargetInfo);

        List<ICodeBoardActor> actors = new List<ICodeBoardActor>()
        {
            new EntityFieldGenerator(),
            new ConstructorGenerator(),
            new InnerBodyCreator(),
            new ForkCreator(),
        };

        foreach (ICodeBoardActor actor in actors)
        {
            if (cancellationToken.IsCancellationRequested || codeBoard.HasErrors)
            {
                break;
            }

            actor.Modify(codeBoard);
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return new Dictionary<FluentApiInfoGroup, BuilderMethods>();
        }

        if (codeBoard.HasErrors)
        {
            return new Dictionary<FluentApiInfoGroup, BuilderMethods>();
        }

        return codeBoard.GroupsToMethods;
    }
}