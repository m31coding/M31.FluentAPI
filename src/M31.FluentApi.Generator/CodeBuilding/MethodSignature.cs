namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodSignature : ICode
{
    private MethodSignature(
        string? returnType,
        string methodName,
        string? explicitInterfacePrefix,
        bool isSignatureForInterface)
    {
        ReturnType = returnType;
        MethodName = methodName;
        ExplicitInterfacePrefix = explicitInterfacePrefix;
        IsSignatureForInterface = isSignatureForInterface;
        Generics = new Generics();
        Parameters = new Parameters();
        Modifiers = new Modifiers();
    }

    private MethodSignature(MethodSignature methodSignature, bool isSignatureForInterface)
    {
        ReturnType = methodSignature.ReturnType;
        MethodName = methodSignature.MethodName;
        ExplicitInterfacePrefix = methodSignature.ExplicitInterfacePrefix;
        IsSignatureForInterface = isSignatureForInterface;
        Generics = new Generics(methodSignature.Generics);
        Parameters = new Parameters(methodSignature.Parameters);
        Modifiers = new Modifiers(methodSignature.Modifiers);
    }

    internal static MethodSignature Create(
        string returnType,
        string methodName,
        string? prefix,
        bool isSignatureForInterface)
    {
        return new MethodSignature(returnType, methodName, prefix, isSignatureForInterface);
    }

    internal static MethodSignature CreateConstructorSignature(string className)
    {
        return new MethodSignature(null, className, null, false);
    }

    internal string? ReturnType { get; }
    internal string MethodName { get; }
    internal string? ExplicitInterfacePrefix { get; }
    internal bool IsSignatureForInterface { get; }
    internal Generics Generics { get; }
    internal Parameters Parameters { get; }
    internal Modifiers Modifiers { get; }
    internal bool IsSignatureForMethodBody => !IsSignatureForInterface;
    internal bool IsExplicitInterfaceImplementation => ExplicitInterfacePrefix != null;

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
            .StartLine()
            .Append(Modifiers, IsSignatureForMethodBody && !IsExplicitInterfaceImplementation)
            .Append($"{ReturnType} ", ReturnType != null)
            .Append($"{ExplicitInterfacePrefix}.", IsSignatureForMethodBody && IsExplicitInterfaceImplementation)
            .Append(MethodName)
            .Append(Generics.Parameters)
            .Append(Parameters, !(IsSignatureForMethodBody && IsExplicitInterfaceImplementation))
            .Append(Parameters.WithoutDefaultValues(), IsSignatureForMethodBody && IsExplicitInterfaceImplementation);

        if (Generics.Constraints.Count == 0 || (IsSignatureForMethodBody && IsExplicitInterfaceImplementation))
        {
            return codeBuilder.Append(IsSignatureForInterface ? ";" : null).EndLine();
        }
        else
        {
            return codeBuilder
                .EndLine()
                .Indent()
                .Append(Generics.Constraints)
                .Append(IsSignatureForInterface ? ";" : null)
                .EndLine()
                .Unindent();
        }
    }
}