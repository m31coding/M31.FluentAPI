using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;
using static M31.FluentApi.Generator.SourceGenerators.AttributeElements.Attributes;

namespace M31.FluentApi.Generator.SourceGenerators.AttributeElements;

internal class AttributeDataExtractor
{
    private readonly ClassInfoReport classInfoReport;

    internal AttributeDataExtractor(ClassInfoReport classInfoReport)
    {
        this.classInfoReport = classInfoReport;
    }

    internal FluentApiAttributeData? GetAttributeData(ISymbol symbol)
    {
        List<AttributeDataExtended> mainAttributes = new List<AttributeDataExtended>();
        List<AttributeDataExtended> orthogonalAttributes = new List<AttributeDataExtended>();
        List<AttributeDataExtended> controlAttributes = new List<AttributeDataExtended>();

        foreach (AttributeData attributeData in symbol.GetAttributes())
        {
            AttributeDataExtended? attributeDataExtended = AttributeDataExtended.Create(attributeData);

            if (attributeDataExtended == null)
            {
                continue;
            }

            if (IsMainAttribute(attributeDataExtended.FullName))
            {
                mainAttributes.Add(attributeDataExtended);
            }
            else if (IsOrthogonalAttribute(attributeDataExtended.FullName))
            {
                orthogonalAttributes.Add(attributeDataExtended);
            }
            else if (IsControlAttribute(attributeDataExtended.FullName))
            {
                controlAttributes.Add(attributeDataExtended);
            }
        }

        if (mainAttributes.Count == 0)
        {
            foreach (AttributeDataExtended orthogonalAttribute in orthogonalAttributes)
            {
                classInfoReport.ReportDiagnostic(OrthogonalAttributeMisused.CreateDiagnostic(orthogonalAttribute));
            }

            return null;
        }

        if (mainAttributes.Count > 1)
        {
            foreach (AttributeDataExtended mainAttribute in mainAttributes.Skip(1))
            {
                classInfoReport.ReportDiagnostic(DuplicateMainAttribute.CreateDiagnostic(mainAttribute));
            }
        }

        if (mainAttributes[0].FullName == FullNames.FluentMethodAttribute)
        {
            foreach (AttributeDataExtended orthogonalAttribute in orthogonalAttributes)
            {
                classInfoReport.ReportDiagnostic(OrthogonalAttributeMisused.CreateDiagnostic(orthogonalAttribute));
            }

            return new FluentApiAttributeData(
                mainAttributes[0],
                Array.Empty<AttributeDataExtended>(),
                controlAttributes);
        }

        return new FluentApiAttributeData(mainAttributes[0], orthogonalAttributes, controlAttributes);
    }
}