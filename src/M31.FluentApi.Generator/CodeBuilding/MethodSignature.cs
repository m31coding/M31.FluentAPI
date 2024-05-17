namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodSignature : ICode
{
    private MethodSignature(string? returnType, string methodName, string? explicitInterfacePrefix, bool isStandAlone)
    {
        ReturnType = returnType;
        MethodName = methodName;
        ExplicitInterfacePrefix = explicitInterfacePrefix;
        IsStandAlone = isStandAlone;
        Generics = new Generics();
        Parameters = new Parameters();
        Modifiers = new Modifiers();
    }

    private MethodSignature(MethodSignature methodSignature, bool isStandAlone)
    {
        ReturnType = methodSignature.ReturnType;
        MethodName = methodSignature.MethodName;
        ExplicitInterfacePrefix = isStandAlone ? null : methodSignature.ExplicitInterfacePrefix;
        IsStandAlone = isStandAlone;
        Generics = new Generics(methodSignature.Generics);
        Parameters = new Parameters(methodSignature.Parameters);
        Modifiers = isStandAlone ? new Modifiers() : new Modifiers(methodSignature.Modifiers);
    }

    internal static MethodSignature Create(string returnType, string methodName, string? prefix, bool isStandAlone)
    {
        return new MethodSignature(returnType, methodName, prefix, isStandAlone);
    }

    internal static MethodSignature CreateConstructorSignature(string className)
    {
        return new MethodSignature(null, className, null, false);
    }

    internal string? ReturnType { get; }
    internal string MethodName { get; }
    internal string? ExplicitInterfacePrefix { get; }
    internal bool IsStandAlone { get; }
    internal Generics Generics { get; }
    internal Parameters Parameters { get; }
    internal Modifiers Modifiers { get; }
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

    internal MethodSignature ToStandAloneMethodSignature()
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
            .Append(Modifiers, !IsExplicitInterfaceImplementation)
            .Append($"{ReturnType} ", ReturnType != null)
            .Append($"{ExplicitInterfacePrefix}.", IsExplicitInterfaceImplementation)
            .Append(MethodName)
            .Append(Generics.Parameters)
            .Append(Parameters);

        if (Generics.Constraints.Count == 0)
        {
            return codeBuilder.Append(IsStandAlone ? ";" : null).EndLine();
        }
        else
        {
            return codeBuilder
                .EndLine()
                .Indent()
                .Append(Generics.Constraints)
                .Append(IsStandAlone ? ";" : null)
                .EndLine()
                .Unindent();
        }
    }
}