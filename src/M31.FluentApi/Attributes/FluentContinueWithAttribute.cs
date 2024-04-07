namespace M31.FluentApi.Attributes;

/// <summary>
/// After calling the builder method, the fluent API continues with the given step.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class FluentContinueWithAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentContinueWithAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step to continue with.</param>
    public FluentContinueWithAttribute(int builderStep)
    {

    }
}