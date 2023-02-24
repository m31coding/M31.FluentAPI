namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal static class Attributes
{
    internal static class FullNames
    {
        internal const string FluentApiAttribute = "M31.FluentApi.Attributes.FluentApiAttribute";

        internal const string FluentMemberAttribute = "M31.FluentApi.Attributes.FluentMemberAttribute";
        internal const string FluentPredicateAttribute = "M31.FluentApi.Attributes.FluentPredicateAttribute";
        internal const string FluentCollectionAttribute = "M31.FluentApi.Attributes.FluentCollectionAttribute";

        internal const string FluentNullableAttribute = "M31.FluentApi.Attributes.FluentNullableAttribute";
        internal const string FluentDefaultAttribute = "M31.FluentApi.Attributes.FluentDefaultAttribute";

        internal const string FluentMethodAttribute = "M31.FluentApi.Attributes.FluentMethodAttribute";
    }

    private static class AttributeSets
    {
        internal static readonly HashSet<string> FullNamesOfMainAttributes = new HashSet<string>()
        {
            FullNames.FluentMemberAttribute,
            FullNames.FluentPredicateAttribute,
            FullNames.FluentCollectionAttribute,
            FullNames.FluentMethodAttribute,
        };

        internal static readonly HashSet<string> FullNamesOfOrthogonalAttributes = new HashSet<string>()
        {
            FullNames.FluentNullableAttribute,
            FullNames.FluentDefaultAttribute,
        };
    }

    internal static bool IsMainAttribute(string attributeFullName)
    {
        return AttributeSets.FullNamesOfMainAttributes.Contains(attributeFullName);
    }

    internal static bool IsOrthogonalAttribute(string attributeFullName)
    {
        return AttributeSets.FullNamesOfOrthogonalAttributes.Contains(attributeFullName);
    }
}