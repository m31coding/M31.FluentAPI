namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a builder method for the target field or property.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentMemberAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentMemberAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step in which the member can be set.</param>
    /// <param name="method">The name of the generated builder method.</param>
    /// <param name="parameterPosition">The parameter position of the member in the builder method; only relevant for
    /// compound builder methods.</param>
    public FluentMemberAttribute(int builderStep, string method = "With{Name}", int parameterPosition = 0)
    {
    }
}