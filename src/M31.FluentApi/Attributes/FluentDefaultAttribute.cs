namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a builder method for the initial value of a member. Can be used in combination with other attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentDefaultAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentDefaultAttribute"/> class.
    /// </summary>
    /// <param name="method">The name of the generated builder method. When called the member will keep its initial
    /// value.
    /// </param>
    public FluentDefaultAttribute(string method = "WithDefault{Name}")
    {

    }
}