namespace M31.FluentApi.Attributes;

/// <summary>
/// After calling the builder method, the fluent API continues with the given step.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class FluentContinueWith : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentContinueWith"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step to continue with.</param>
    public FluentContinueWith(int builderStep)
    {

    }
}