namespace M31.FluentApi.Attributes;

/// <summary>
/// After calling the builder method, the fluent API ends and the created instance is returned.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class FluentBreakAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentBreakAttribute"/> class.
    /// </summary>
    public FluentBreakAttribute()
    {

    }
}