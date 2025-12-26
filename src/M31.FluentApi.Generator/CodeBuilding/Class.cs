namespace M31.FluentApi.Generator.CodeBuilding;

internal class Class : ICode
{
    private readonly List<Field> fields;
    private readonly List<Method> methods;
    private readonly List<MethodSignature> methodSignatures;
    private readonly List<Property> properties;
    private readonly List<string> interfaces;
    private readonly List<ICode> definitions;

    internal Class(string name)
    {
        Name = name;
        Generics = new Generics();
        Modifiers = new Modifiers();
        fields = new List<Field>();
        methods = new List<Method>();
        methodSignatures = new List<MethodSignature>();
        properties = new List<Property>();
        interfaces = new List<string>();
        definitions = new List<ICode>();
    }

    internal string Name { get; }

    internal Generics Generics { get; }
    internal Modifiers Modifiers { get; }
    internal IReadOnlyCollection<Field> Fields => fields;
    internal IReadOnlyCollection<Method> Methods => methods;
    internal IReadOnlyCollection<MethodSignature> MethodSignatures => methodSignatures;
    internal IReadOnlyCollection<Property> Properties => properties;
    internal IReadOnlyCollection<string> Interfaces => interfaces;
    internal IReadOnlyCollection<ICode> Definitions => definitions;

    internal void AddGenericParameter(string parameter, IEnumerable<string> constraints)
    {
        Generics.AddGenericParameter(parameter, constraints);
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

    internal void AddMethodSignature(MethodSignature methodSignature)
    {
        methodSignatures.Add(methodSignature);
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
            .Append(Generics.Parameters);

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
            .Append(Generics.Constraints)
            .EndLine()
            .Unindent()
            .OpenBlock()
            .Append(fields)
            .BlankLine()
            .Append(properties)
            .BlankLine()
            .AppendWithBlankLines(methods)
            .AppendWithBlankLines(definitions)
            .AppendWithBlankLines(methodSignatures)
            .CloseBlock();
    }
}