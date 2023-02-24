using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal class AttributeDataExtended
{
    internal AttributeDataExtended(AttributeData attributeData, string fullName, string shortName)
    {
        AttributeData = attributeData;
        FullName = fullName;
        ShortName = shortName;
    }

    internal AttributeData AttributeData { get; }
    internal string FullName { get; }
    internal string ShortName { get; }

    internal static AttributeDataExtended? Create(AttributeData attributeData)
    {
        if (attributeData.AttributeClass == null)
        {
            return null;
        }

        string fullName = attributeData.AttributeClass.ToDefaultDisplayString();

        string shortName = attributeData.AttributeClass.ToDisplayString(new SymbolDisplayFormat(
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameOnly));

        if (shortName.EndsWith("Attribute"))
        {
            shortName = shortName.Substring(0, shortName.Length - "Attribute".Length);
        }

        return new AttributeDataExtended(attributeData, fullName, shortName);
    }
}