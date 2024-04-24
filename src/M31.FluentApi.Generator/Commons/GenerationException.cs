namespace M31.FluentApi.Generator.Commons;

internal class GenerationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenerationException"/> class. Throw this exception only for errors
    /// that should not occur because they were already handled with diagnostics. Therefore, if this exception is
    /// actually thrown, the diagnostics need to be improved.
    /// </summary>
    /// <param name="message">The exception message.</param>
    internal GenerationException(string message)
        : base($"Unable to generate fluent API source code: {message}")
    {
    }
}