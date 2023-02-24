using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal abstract class BuilderStepMethod : BuilderMethod
{
    protected BuilderStepMethod(BuilderMethod builderMethod)
        : base(builderMethod)
    {
    }

    internal abstract Method BuildMethodCode(BuilderAndTargetInfo info);

    protected MethodSignature CreateMethodSignature(string returnType, params string[] modifiers)
    {
        MethodSignature signature = MethodSignature.Create(returnType, MethodName, false);
        signature.AddModifiers(modifiers);

        foreach (Parameter parameter in Parameters)
        {
            signature.AddParameter(parameter);
        }

        return signature;
    }

    protected void CreateBody(Method method, string instancePrefix)
    {
        List<string> bodyCode = BuildBodyCode(instancePrefix);
        foreach (string bodyLine in bodyCode)
        {
            method.AppendBodyLine(bodyLine);
        }
    }
}