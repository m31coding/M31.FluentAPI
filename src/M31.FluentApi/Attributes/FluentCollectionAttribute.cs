// ReSharper disable UnusedParameter.Local

namespace M31.FluentApi.Attributes;

/// <summary>
/// Generates builder methods for a collection.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class FluentCollectionAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentCollectionAttribute"/> class.
    /// </summary>
    /// <param name="builderStep">The builder step in which the collection can be set.</param>
    /// <param name="singularName">The singular of the collection name.</param>
    /// <param name="withItems">The name of the builder method that sets multiple items.</param>
    /// <param name="withItem">The name of the builder method that sets a single item.
    /// If set to null, the builder method will not be generated.</param>
    /// <param name="withZeroItems">The name of the builder method that sets zero items.
    /// If set to null, the builder method will not be generated.</param>
    public FluentCollectionAttribute(
        int builderStep,
        string singularName,
        string withItems = "With{Name}",
        string? withItem = "With{SingularName}",
        string? withZeroItems = "WithZero{Name}")
    {
    }
}