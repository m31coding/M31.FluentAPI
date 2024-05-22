using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeInfo;

internal record LambdaBuilderInfo
{
    internal LambdaBuilderInfo(string builderClassName, string builderTypeForCodeGeneration)
    {
        BuilderClassName = builderClassName; // "CreateAddress"
        BuilderTypeForCodeGeneration = builderTypeForCodeGeneration; // Namespace.CreateAddress
        BuilderInstanceName = builderClassName.FirstCharToLower(); // createAddress
        InitialStepInterfaceName = $"I{BuilderClassName}"; // ICreateAddress
    }

    internal string BuilderClassName { get; }
    internal string BuilderTypeForCodeGeneration { get; }
    internal string BuilderInstanceName { get; }
    internal string InitialStepInterfaceName { get; }
}