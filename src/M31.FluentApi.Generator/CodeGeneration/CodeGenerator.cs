using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration;

internal static class CodeGenerator
{
    internal static CodeGeneratorResult GenerateCode(FluentApiClassInfo classInfo, CancellationToken cancellationToken)
    {
        BuilderAndTargetInfo builderAndTargetInfo =
            new BuilderAndTargetInfo(
                classInfo.Name,
                classInfo.Namespace,
                classInfo.GenericInfo,
                classInfo.IsStruct,
                classInfo.IsInternal,
                classInfo.HasPrivateConstructor,
                classInfo.BuilderClassName);

        CodeBoard codeBoard = CodeBoard.Create(
            builderAndTargetInfo,
            classInfo.FluentApiInfos,
            classInfo.AdditionalInfo.FluentApiInfoGroups,
            classInfo.UsingStatements,
            classInfo.NewLineString,
            cancellationToken);

        List<ICodeBoardActor> actors = new List<ICodeBoardActor>()
        {
            new EntityFieldGenerator(),
            new ConstructorGenerator(),
            new InnerBodyCreator(),
            new ForkCreator(),
            new DuplicateMethodsChecker(),
            new BuilderGenerator(),
            new InterfaceGenerator(),
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
}