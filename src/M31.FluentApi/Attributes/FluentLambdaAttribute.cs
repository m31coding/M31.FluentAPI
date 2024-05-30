// ReSharper disable UnusedParameter.Local

namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a builder method that accepts a lambda expression for creating the target field or property with its
/// Fluent API.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentLambdaAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentLambdaAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step in which the member can be set.</param>
    /// <param name="method">The name of the generated builder method.</param>
    public FluentLambdaAttribute(int builderStep, string method = "With{Name}")
    {
    }
}