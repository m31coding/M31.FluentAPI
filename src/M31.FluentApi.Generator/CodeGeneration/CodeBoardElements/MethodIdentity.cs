namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal record MethodIdentity
{
    private MethodIdentity(string methodName, string parameterTypes)
    {
        MethodName = methodName;
        ParameterTypes = parameterTypes;
    }

    internal string MethodName { get; }
    internal string ParameterTypes { get; }

    internal static MethodIdentity Create(string methodName)
    {
        return new MethodIdentity(methodName, string.Empty);
    }

    internal static MethodIdentity Create(string methodName, IEnumerable<string> parameterTypes)
    {
        return new MethodIdentity(methodName, string.Join("_", parameterTypes));
    }
}