// ReSharper disable UnusedParameter.Local

namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a builder method that calls the target method.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class FluentMethodAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentMethodAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step in which the method can be called.</param>
    /// <param name="method">The name of the generated builder method. The builder method calls the decorated method.
    /// </param>
    public FluentMethodAttribute(int builderStep, string method = "{Name}")
    {
    }
}