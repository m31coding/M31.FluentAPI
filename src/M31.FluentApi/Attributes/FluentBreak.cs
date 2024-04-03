namespace M31.FluentApi.Attributes;

/// <summary>
/// After calling the builder method, the fluent API ends and the created instance is returned.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class FluentBreak : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentBreak"/> class.
    /// </summary>
    public FluentBreak()
    {

    }
}