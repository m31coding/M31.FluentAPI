// ReSharper disable UnusedParameter.Local

namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a builder method for a nullable member. Can be used in combination with other attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentNullableAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentNullableAttribute"/> class.
    /// </summary>
    /// <param name="method">The name of the generated builder method. The builder method sets the member's value to
    /// 'null'.</param>
    public FluentNullableAttribute(string method = "Without{Name}")
    {

    }
}