using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Collections;

namespace M31.FluentApi.Generator.SourceGenerators.Collections;

internal record CollectionType
{
    internal CollectionType(GeneratedCollection collection, string genericTypeArgument)
    {
        this.Collection = collection;
        this.GenericTypeArgument = genericTypeArgument;
    }

    internal GeneratedCollection Collection { get; }
    internal string GenericTypeArgument { get; }
}