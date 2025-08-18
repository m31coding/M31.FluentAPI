namespace M31.FluentApi.Attributes;

/// <summary>
/// The builder method can be skipped.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class FluentSkippableAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentSkippableAttribute"/> class.
    /// </summary>
    public FluentSkippableAttribute()
    {
    }
}