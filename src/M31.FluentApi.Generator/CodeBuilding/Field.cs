namespace M31.FluentApi.Generator.CodeBuilding;

internal class Field : ICode
{
    internal Field(string type, string name)
    {
        Type = type;
        Name = name;
        Modifiers = new Modifiers();
        GenericParameters = new GenericParameters();
    }

    internal string Type { get; }
    internal string Name { get; }

    internal GenericParameters GenericParameters { get; }
    internal Modifiers Modifiers { get; }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddModifiers(IEnumerable<string> modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddGenericParameters(params string[] parameters)
    {
        GenericParameters.Add(parameters);
    }

    internal void AddGenericParameters(IEnumerable<string> parameters)
    {
        GenericParameters.Add(parameters);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append(Type)
            .Append(GenericParameters)
            .Append($" {Name};")
            .EndLine();
    }
}