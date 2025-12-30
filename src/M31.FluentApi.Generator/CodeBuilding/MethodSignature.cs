namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodSignature : ICode
{
    private MethodSignature(
        string? returnType,
        string methodName,
        string? explicitInterfacePrefix,
        bool isStandaloneSignature)
    {
        ReturnType = returnType;
        MethodName = methodName;
        ExplicitInterfacePrefix = explicitInterfacePrefix;
        IsStandaloneSignature = isStandaloneSignature;
        Generics = new Generics();
        Parameters = new Parameters();
        Modifiers = new Modifiers();
        Attributes = new List<string>();
    }

    private MethodSignature(MethodSignature methodSignature, bool isStandaloneSignature)
    {
        ReturnType = methodSignature.ReturnType;
        MethodName = methodSignature.MethodName;
        ExplicitInterfacePrefix = methodSignature.ExplicitInterfacePrefix;
        IsStandaloneSignature = isStandaloneSignature;
        Generics = new Generics(methodSignature.Generics);
        Parameters = new Parameters(methodSignature.Parameters);
        Modifiers = new Modifiers(methodSignature.Modifiers);
        Attributes = new List<string>(methodSignature.Attributes);
    }

    internal static MethodSignature Create(
        string returnType,
        string methodName,
        string? prefix,
        bool isStandaloneSignature)
    {
        return new MethodSignature(returnType, methodName, prefix, isStandaloneSignature);
    }

    internal static MethodSignature CreateConstructorSignature(string className)
    {
        return new MethodSignature(null, className, null, false);
    }

    internal string? ReturnType { get; }
    internal string MethodName { get; }
    internal string? ExplicitInterfacePrefix { get; }
    internal bool IsStandaloneSignature { get; }
    internal Generics Generics { get; }
    internal Parameters Parameters { get; }
    internal Modifiers Modifiers { get; }
    internal List<string> Attributes { get; }
    internal bool IsSignatureForMethodBody => !IsStandaloneSignature;
    internal bool IsExplicitInterfaceImplementation => ExplicitInterfacePrefix != null;
    internal bool IsConstructor => ReturnType == null;

    internal void AddGenericParameter(string parameter, IEnumerable<string> constraints)
    {
        Generics.AddGenericParameter(parameter, constraints);
    }

    internal void AddParameter(string type, string name)
    {
        AddParameter(new Parameter(type, name));
    }

    internal void AddParameter(Parameter parameter)
    {
        Parameters.AddParameter(parameter);
    }

    internal void AddModifiers(params string[] modifiers)
    {
        Modifiers.Add(modifiers);
    }

    internal void AddAttribute(string attribute)
    {
        Attributes.Add(attribute);
    }

    internal MethodSignature ToSignatureForInterface()
    {
        return new MethodSignature(this, true);
    }

    internal MethodSignature ToSignatureForMethodBody()
    {
        return new MethodSignature(this, false);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        codeBuilder
            .AppendLines(Attributes)
            .StartLine()
            .Append(
                Modifiers,
                Modifiers.Contains("extern") || (IsSignatureForMethodBody && !IsExplicitInterfaceImplementation))
            .Append($"{ReturnType} ", ReturnType != null)
            .Append($"{ExplicitInterfacePrefix}.", IsSignatureForMethodBody && IsExplicitInterfaceImplementation)
            .Append(MethodName)
            .Append(Generics.Parameters)
            .Append(Parameters, !(IsSignatureForMethodBody && IsExplicitInterfaceImplementation))
            .Append(Parameters.WithoutDefaultValues(), IsSignatureForMethodBody && IsExplicitInterfaceImplementation);

        if (Generics.Constraints.Count == 0 || (IsSignatureForMethodBody && IsExplicitInterfaceImplementation))
        {
            return codeBuilder.Append(IsStandaloneSignature ? ";" : null).EndLine();
        }
        else
        {
            return codeBuilder
                .EndLine()
                .Indent()
                .Append(Generics.Constraints)
                .Append(IsStandaloneSignature ? ";" : null)
                .EndLine()
                .Unindent();
        }
    }
}