namespace M31.FluentApi.Generator.CodeBuilding;

internal class Class : ICode
{
    private readonly List<Field> fields;
    private readonly List<Method> methods;
    private readonly List<Property> properties;
    private readonly List<string> interfaces;
    private readonly List<ICode> definitions;

    internal Class(string name)
    {
        Name = name;
        GenericTypeParameters = new GenericTypeParameters();
        Modifiers = new Modifiers();
        fields = new List<Field>();
        methods = new List<Method>();
        properties = new List<Property>();
        interfaces = new List<string>();
        definitions = new List<ICode>();
    }

    internal string Name { get; }

    internal GenericTypeParameters GenericTypeParameters { get; }
    internal Modifiers Modifiers { get; }
    internal IReadOnlyCollection<Field> Fields => fields;
    internal IReadOnlyCollection<Method> Methods => methods;
    internal IReadOnlyCollection<Property> Properties => properties;
    internal IReadOnlyCollection<string> Interfaces => interfaces;
    internal IReadOnlyCollection<ICode> Definitions => definitions;

    internal void AddGenericTypeParameters(params string[] parameters)
    {
        GenericTypeParameters.Add(parameters);
    }

    internal void AddGenericTypeParameters(IEnumerable<string> parameters)
    {
        GenericTypeParameters.Add(parameters);
    }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddModifiers(IEnumerable<string> modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddField(Field field)
    {
        fields.Add(field);
    }

    internal void AddMethod(Method method)
    {
        methods.Add(method);
    }

    internal void AddProperty(Property property)
    {
        properties.Add(property);
    }

    internal void AddInterface(string interfaceName)
    {
        interfaces.Add(interfaceName);
    }

    internal void AddDefinition(ICode definition)
    {
        definitions.Add(definition);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append($"class {Name}")
            .Append(GenericTypeParameters);

        if (interfaces.Count > 0)
        {
            codeBuilder.Append(" : ");
            codeBuilder.Append(interfaces, ", ");
        }

        return codeBuilder
            .EndLine()
            .OpenBlock()
            .Append(fields)
            .BlankLine()
            .Append(properties)
            .BlankLine()
            .AppendWithBlankLines(methods)
            .AppendWithBlankLines(definitions)
            .CloseBlock();
    }
}