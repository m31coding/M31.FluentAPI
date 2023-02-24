namespace M31.FluentApi.Tests.Helpers;

internal class GeneratorOutput
{
    internal GeneratorOutput(string code, string className)
    {
        Code = code;
        ClassName = className;
    }

    internal string Code { get; }
    internal string ClassName { get; }
}