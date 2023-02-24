namespace M31.FluentApi.Generator.CodeBuilding;

internal class Property : ICode
{
    internal Property(string type, string name)
    {
        Type = type;
        Name = name;
        Modifiers = new Modifiers();
    }

    internal string Type { get; }
    internal string Name { get; }

    internal Modifiers Modifiers { get; }
    internal string? GetAccessor { get; private set; }
    internal string? SetAccessor { get; private set; }
    internal string? RightHandSide { get; private set; }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void WithGetAccessor(string accessModifier = "")
    {
        GetAccessor = $"{AccessModifierToString(accessModifier)}get; ";
    }

    internal void WithSetAccessor(string accessModifier = "")
    {
        SetAccessor = $"{AccessModifierToString(accessModifier)}set; ";
    }

    internal void WithInitAccessor(string accessModifier = "")
    {
        SetAccessor = $"{AccessModifierToString(accessModifier)}init; ";
    }

    private static string AccessModifierToString(string accessModifier)
    {
        return accessModifier == string.Empty ? accessModifier : $"{accessModifier} ";
    }

    internal void AddRightHandSide(string code)
    {
        RightHandSide = code;
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        bool hasAccessor = GetAccessor != null || SetAccessor != null;

        if (!hasAccessor && RightHandSide == null)
        {
            throw new ArgumentException("The property needs at least one accessor or a right hand side.");
        }

        return codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append($"{Type} {Name}")
            .Append(" { ", hasAccessor)
            .Append(GetAccessor)
            .Append(SetAccessor)
            .Append("}", hasAccessor)
            .Append(RightHandSide != null ? $" {RightHandSide}" : null)
            .EndLine();
    }
}