using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class InitialStepMethodGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        if (codeBoard.Forks.Count == 0)
        {
            return;
        }

        // public static ICreateStudent InitialStep()
        // {
        //     return new CreateStudent();
        // }
        MethodSignature methodSignature = MethodSignature.Create(
            codeBoard.Info.InitialStepInterfaceName,
            "InitialStep",
            null,
            false);
        methodSignature.AddModifiers(codeBoard.Info.DefaultAccessModifier, "static");
        Method method = new Method(methodSignature);
        string parameterListInAngleBrackets = codeBoard.Info.GenericInfo?.ParameterListInAngleBrackets ?? string.Empty;
        method.AppendBodyLine($"return new {codeBoard.Info.BuilderClassName}{parameterListInAngleBrackets}();");
        codeBoard.BuilderClass.AddMethod(method);
    }
}