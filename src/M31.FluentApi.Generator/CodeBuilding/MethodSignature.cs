namespace M31.FluentApi.Generator.CodeBuilding;

internal class MethodSignature : ICode
{
    private MethodSignature(string? returnType, string methodName, bool isStandAlone)
    {
        ReturnType = returnType;
        MethodName = methodName;
        IsStandAlone = isStandAlone;
        Parameters = new Parameters();
        Modifiers = new Modifiers();
    }

    internal static MethodSignature Create(string returnType, string methodName, bool isStandAlone)
    {
        return new MethodSignature(returnType, methodName, isStandAlone);
    }

    internal static MethodSignature CreateConstructorSignature(string className)
    {
        return new MethodSignature(null, className, false);
    }

    internal MethodSignature ToStandAloneMethodSignature()
    {
        MethodSignature newSignature = new MethodSignature(ReturnType, MethodName, true);

        foreach (Parameter parameter in Parameters.Values)
        {
            newSignature.AddParameter(parameter);
        }

        return newSignature;
    }

    internal MethodSignature ToSignatureForMethodBody()
    {
        MethodSignature newSignature = new MethodSignature(ReturnType, MethodName, false);

        foreach (Parameter parameter in Parameters.Values)
        {
            newSignature.AddParameter(parameter);
        }

        return newSignature;
    }

    internal string? ReturnType { get; }
    internal string MethodName { get; }
    internal bool IsStandAlone { get; }

    internal Parameters Parameters { get; }
    internal Modifiers Modifiers { get; }

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

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .StartLine()
            .Append(Modifiers)
            .Append($"{ReturnType} ", ReturnType != null)
            .Append(MethodName)
            .Append(Parameters).Append(IsStandAlone ? ";" : null)
            .EndLine();
    }
}