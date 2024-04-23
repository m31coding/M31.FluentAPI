namespace M31.FluentApi.Generator.CodeBuilding;

internal class Parameter : ICode
{
    internal Parameter(
        string type,
        string name,
        string? defaultValue = null,
        ParameterAnnotations? parameterAnnotations = null)
    {
        Type = type;
        Name = name;
        DefaultValue = defaultValue;
        ParameterAnnotations = parameterAnnotations;
    }

    internal string Type { get; }
    internal string Name { get; }
    internal string? DefaultValue { get; }
    internal ParameterAnnotations? ParameterAnnotations { get; }

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