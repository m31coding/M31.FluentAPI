namespace M31.FluentApi.Generator.Commons;

internal class GenerationException : Exception
{
    internal GenerationException(string message)
        : base($"Unable to generate fluent API source code: {message}")
    {
    }
}