// ReSharper disable UnusedParameter.Local

namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates builder methods for a boolean member.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentPredicateAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentPredicateAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step in which the member can be set.</param>
    /// <param name="method">The name of the builder method that sets the member's value to 'true'.
    /// </param>
    /// <param name="negatedMethod">The name of the builder method that sets the member's value to
    /// 'false'.</param>
    public FluentPredicateAttribute(int builderStep, string method = "{Name}", string negatedMethod = "Not{Name}")
    {
    }
}