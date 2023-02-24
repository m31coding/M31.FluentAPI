namespace M31.FluentApi.Generator.CodeBuilding;

internal class Field : ICode
{
    internal Field(string type, string name)
    {
        Type = type;
        Name = name;
        Modifiers = new Modifiers();
    }

    internal string Type { get; }
    internal string Name { get; }

    internal Modifiers Modifiers { get; }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    private static string AccessModifierToString(string accessModifier)
    {
        return accessModifier == string.Empty ? accessModifier : $"{accessModifier} ";
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append($"{Type} {Name};")
            .EndLine();
    }
}