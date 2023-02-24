namespace M31.FluentApi.Generator.CodeBuilding;

internal class Parameter : ICode
{
    internal Parameter(string type, string name, string? defaultValue = null)
    {
        Type = type;
        Name = name;
        DefaultValue = defaultValue;
    }

    internal string Type { get; }
    internal string Name { get; }
    internal string? DefaultValue { get; }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder.Append(Type).Space().Append(Name).Append($" = {DefaultValue}", DefaultValue != null);
    }
}