namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates a fluent API for the target class, struct or record.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class FluentApiAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentApiAttribute"/> class.
    /// </summary>
    /// <param name="builderClassName">The name of the generated builder class.</param>
    public FluentApiAttribute(string builderClassName = "Create{Name}")
    {

    }
}