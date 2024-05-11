namespace M31.FluentApi.Attributes;

/// <summary>
/// The builder stops and the return value of the builder method is returned.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class FluentReturnAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentReturnAttribute"/> class.
    /// </summary>
    public FluentReturnAttribute()
    {

    }
}