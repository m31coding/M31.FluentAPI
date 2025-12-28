using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal static class CodeBuildingHelpers
{
    internal static void AddGenericParameters(MethodSignature methodSignature, GenericInfo? genericInfo)
    {
        if (genericInfo == null)
        {
            return;
        }

        foreach (GenericTypeParameter genericTypeParameter in genericInfo.Parameters)
        {
            methodSignature.AddGenericParameter(
                genericTypeParameter.ParameterName,
                genericTypeParameter.Constraints.GetConstraintsForCodeGeneration());
        }
    }
}