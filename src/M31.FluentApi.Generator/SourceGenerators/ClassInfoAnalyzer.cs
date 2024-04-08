// Opening parenthesis should not be preceded by a space
#pragma warning disable SA1008

using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeInfo;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.SourceGenerators;

internal class ClassInfoAnalyzer
{
    private readonly SemanticModel semanticModel;
    private readonly ClassInfoReport report;

    internal ClassInfoAnalyzer(
        SemanticModel semanticModel,
        ClassInfoReport report)
    {
        this.semanticModel = semanticModel;
        this.report = report;
    }

    internal void Analyze(FluentApiClassInfo classInfo, CancellationToken cancellationToken)
    {
        AnalyzeFluentApiInfosIndividually(classInfo.FluentApiInfos, cancellationToken);
        if (cancellationToken.IsCancellationRequested) return;
        IReadOnlyCollection<FluentApiInfoGroup> groups = FluentApiInfoGroup.CreateGroups(classInfo.FluentApiInfos);
        AnalyzeGroups(groups, cancellationToken);
    }

    private void AnalyzeFluentApiInfosIndividually(
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos,
        CancellationToken cancellationToken)
    {
        foreach (FluentApiInfo info in fluentApiInfos)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            AnalyzeInfo(info);
        }

        void AnalyzeInfo(FluentApiInfo info)
        {
            if (info.AttributeInfo is FluentCollectionAttributeInfo)
            {
                AnalyzeFluentCollection(info);
            }

            if (info.AttributeInfo is FluentPredicateAttributeInfo)
            {
                AnalyzeFluentPredicate(info);
            }

            if (info.SymbolInfo is MemberSymbolInfo { IsProperty: true })
            {
                AnalyzeProperty(info);
            }

            if (info.SymbolInfo is MethodSymbolInfo)
            {
                AnalyzeMethod(info);
            }

            foreach (OrthogonalAttributeInfoBase orthogonalInfo in info.OrthogonalAttributeInfos)
            {
                if (orthogonalInfo is FluentNullableAttributeInfo)
                {
                    AnalyzeFluentNullable(info);
                }
            }
        }
    }

    private void AnalyzeFluentCollection(FluentApiInfo info)
    {
        MemberSymbolInfo memberInfo = (MemberSymbolInfo)info.SymbolInfo;

        if (memberInfo.CollectionType == null)
        {
            ISymbol symbol = info.AdditionalInfo.Symbol;
            TypeSyntax typeSyntax = GetTypeSyntax(symbol);
            report.ReportDiagnostic(UnsupportedFluentCollectionType.CreateDiagnostic(typeSyntax));
        }
    }

    private void AnalyzeFluentPredicate(FluentApiInfo info)
    {
        MemberSymbolInfo memberInfo = (MemberSymbolInfo)info.SymbolInfo;

        if (memberInfo.Type is not ("bool" or "bool?"))
        {
            ISymbol symbol = info.AdditionalInfo.Symbol;
            TypeSyntax typeSyntax = GetTypeSyntax(symbol);
            report.ReportDiagnostic(InvalidFluentPredicateType.CreateDiagnostic(typeSyntax));
        }
    }

    private void AnalyzeProperty(FluentApiInfo info)
    {
        IPropertySymbol propertySymbol = (IPropertySymbol)info.AdditionalInfo.Symbol;

        if (propertySymbol.SetMethod == null)
        {
            report.ReportDiagnostic(MissingSetAccessor.CreateDiagnostic(propertySymbol));
        }
    }

    private void AnalyzeMethod(FluentApiInfo info)
    {
        IMethodSymbol methodSymbol = (IMethodSymbol)info.AdditionalInfo.Symbol;

        if (!methodSymbol.ReturnsVoid)
        {
            TypeSyntax typeSyntax = GetTypeSyntax(methodSymbol);
            report.ReportDiagnostic(InvalidFluentMethodReturnType.CreateDiagnostic(typeSyntax));
        }
    }

    private void AnalyzeFluentNullable(FluentApiInfo info)
    {
        MemberSymbolInfo memberInfo = (MemberSymbolInfo)info.SymbolInfo;
        ISymbol symbol = info.AdditionalInfo.Symbol;
        bool isReferenceType = false;
        bool isValueType = false;

        if (symbol is IPropertySymbol propertySymbol)
        {
            isReferenceType = propertySymbol.Type.IsReferenceType;
            isValueType = propertySymbol.Type.IsValueType;
        }

        if (symbol is IFieldSymbol fieldSymbol)
        {
            isReferenceType = fieldSymbol.Type.IsReferenceType;
            isValueType = fieldSymbol.Type.IsValueType;
        }

        if (!isReferenceType && !isValueType) // unconstrained type parameter
        {
            TypeSyntax typeSyntax = GetTypeSyntax(symbol);
            report.ReportDiagnostic(InvalidFluentNullableType.CreateDiagnostic(typeSyntax));
        }

        if (isValueType && !memberInfo.IsNullable)
        {
            TypeSyntax typeSyntax = GetTypeSyntax(symbol);
            report.ReportDiagnostic(InvalidFluentNullableType.CreateDiagnostic(typeSyntax));
        }

        if (isReferenceType && !memberInfo.IsNullable)
        {
            TypeSyntax typeSyntax = GetTypeSyntax(symbol);
            NullableContext nullableContext = semanticModel.GetNullableContext(typeSyntax.SpanStart);

            bool nullableContextEnabled =
                (nullableContext & NullableContext.Enabled) == NullableContext.Enabled;

            if (nullableContextEnabled)
            {
                report.ReportDiagnostic(FluentNullableTypeWithoutNullableAnnotation.CreateDiagnostic(typeSyntax));
            }
        }
    }

    private TypeSyntax GetTypeSyntax(ISymbol symbol)
    {
        SyntaxNode node = symbol.DeclaringSyntaxReferences[0].GetSyntax();
        return node.DescendantNodes(n => n is not AttributeSyntax).OfType<TypeSyntax>().First();
    }

    private void AnalyzeGroups(IReadOnlyCollection<FluentApiInfoGroup> groups, CancellationToken cancellationToken)
    {
        foreach (FluentApiInfoGroup group in groups)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            ReportErrorsForOrthogonalAttributesOnCompounds(group);
        }
    }

    private void ReportErrorsForOrthogonalAttributesOnCompounds(FluentApiInfoGroup group)
    {
        if (group.IsCompoundGroup)
        {
            foreach (FluentApiInfo info in group.FluentApiInfos)
            {
                foreach (OrthogonalAttributeInfoBase orthogonalAttributeInfo in info.OrthogonalAttributeInfos)
                {
                    AttributeDataExtended orthogonalAttributeData =
                        info.AdditionalInfo.OrthogonalAttributeData[orthogonalAttributeInfo];

                    report.ReportDiagnostic(
                        OrthogonalAttributeMisusedWithCompound.CreateDiagnostic(orthogonalAttributeData));
                }
            }
        }
    }
}