namespace M31.FluentApi.Generator.CodeBuilding;

internal class Parameter : ICode
{
    internal Parameter(
        string type,
        string name,
        string? defaultValue = null,
        int? genericTypeParameterPosition = null,
        ParameterAnnotations? parameterAnnotations = null)
    {
        Type = type;
        Name = name;
        DefaultValue = defaultValue;
        GenericTypeParameterPosition = genericTypeParameterPosition;
        ParameterAnnotations = parameterAnnotations;
    }

    internal Parameter WithoutDefaultValue()
    {
        return new Parameter(Type, Name, null, GenericTypeParameterPosition, ParameterAnnotations);
    }

    internal string Type { get; }
    internal string Name { get; }
    internal string? DefaultValue { get; }
    internal ParameterAnnotations? ParameterAnnotations { get; }
    internal int? GenericTypeParameterPosition { get; }
    internal bool IsGenericParameter => GenericTypeParameterPosition.HasValue;

    internal bool HasAnnotation(ParameterKinds parameterKinds)
    {
        return ParameterAnnotations != null && ParameterAnnotations.Contains(parameterKinds);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .Append(ParameterAnnotations)
            .Append(Type).Space().Append(Name).Append(() => $" = {DefaultValue}", DefaultValue != null);
    }
}