using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class BuilderGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        IReadOnlyCollection<BuilderStepMethod> builderStepMethods =
            BuilderStepMethodCreator.CreateBuilderStepMethods(codeBoard.Forks);

        foreach (BuilderStepMethod builderStepMethod in builderStepMethods)
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            Method method = builderStepMethod.BuildMethodCode(codeBoard.Info);
            codeBoard.BuilderClass.AddMethod(method);
        }
    }
}