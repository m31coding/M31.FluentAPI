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
        GenericParameters = new GenericParameters();
        GenericConstraints = new GenericConstraints();
        Modifiers = new Modifiers();
        fields = new List<Field>();
        methods = new List<Method>();
        properties = new List<Property>();
        interfaces = new List<string>();
        definitions = new List<ICode>();
    }

    internal string Name { get; }

    internal GenericParameters GenericParameters { get; }
    internal GenericConstraints GenericConstraints { get; }
    internal Modifiers Modifiers { get; }
    internal IReadOnlyCollection<Field> Fields => fields;
    internal IReadOnlyCollection<Method> Methods => methods;
    internal IReadOnlyCollection<Property> Properties => properties;
    internal IReadOnlyCollection<string> Interfaces => interfaces;
    internal IReadOnlyCollection<ICode> Definitions => definitions;

    internal void AddGenericParameter(string parameter, IEnumerable<string> constraints)
    {
        GenericParameters.Add(parameter);
        IReadOnlyCollection<string> constraintsCollection = constraints.ToArray();
        if (constraintsCollection.Count > 0)
        {
            GenericConstraints.Add(parameter, constraintsCollection);
        }
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
            .Append(GenericParameters);

        if (interfaces.Count == 1)
        {
            codeBuilder.Append(" : ");
            codeBuilder.Append(interfaces[0]);
        }

        if (interfaces.Count > 1)
        {
            codeBuilder.Append(" :");
            codeBuilder.EndLine();
            codeBuilder.Indent();

            foreach (string @interface in interfaces.Take(interfaces.Count - 1))
            {
                codeBuilder.AppendLine($"{@interface},");
            }

            codeBuilder.AppendLine(interfaces[interfaces.Count - 1]);

            codeBuilder.Unindent();
        }

        return codeBuilder
            .EndLine()
            .Indent()
            .Append(GenericConstraints)
            .Unindent()
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