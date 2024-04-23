namespace M31.FluentApi.Generator.CodeBuilding;

internal class Field : ICode
{
    internal Field(string type, string name)
    {
        Type = type;
        Name = name;
        Modifiers = new Modifiers();
        GenericTypeParameters = new GenericTypeParameters();
    }

    internal string Type { get; }
    internal string Name { get; }

    internal GenericTypeParameters GenericTypeParameters { get; }
    internal Modifiers Modifiers { get; }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddModifiers(IEnumerable<string> modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddGenericTypeParameters(params string[] parameters)
    {
        GenericTypeParameters.Add(parameters);
    }

    internal void AddGenericTypeParameters(IEnumerable<string> parameters)
    {
        GenericTypeParameters.Add(parameters);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append(Type)
            .Append(GenericTypeParameters)
            .Append($" {Name};")
            .EndLine();
    }
}