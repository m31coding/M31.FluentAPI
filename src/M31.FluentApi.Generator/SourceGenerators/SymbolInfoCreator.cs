using System.Text.RegularExpressions;
using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements.FluentApiComments;
using M31.FluentApi.Generator.SourceGenerators.Collections;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace M31.FluentApi.Generator.SourceGenerators;

internal static class SymbolInfoCreator
{
    internal static FluentApiSymbolInfo Create(ISymbol symbol, string declaringClassNameWithTypeParameters)
    {
        return symbol switch
        {
            IPropertySymbol propertySymbol => CreateMemberSymbolInfo(
                propertySymbol,
                declaringClassNameWithTypeParameters),
            IFieldSymbol fieldSymbol => CreateMemberSymbolInfo(
                fieldSymbol,
                declaringClassNameWithTypeParameters),
            IMethodSymbol methodSymbol => CreateMethodSymbolInfo(
                methodSymbol,
                declaringClassNameWithTypeParameters),
            _ => throw new ArgumentException($"Unexpected symbol type: {symbol.GetType()}."),
        };
    }

    private static MemberSymbolInfo CreateMemberSymbolInfo(
        IFieldSymbol fieldSymbol,
        string declaringClassNameWithTypeParameters)
    {
        return new MemberSymbolInfo(
            fieldSymbol.Name,
            fieldSymbol.Type.ToString(),
            declaringClassNameWithTypeParameters,
            fieldSymbol.DeclaredAccessibility,
            CodeTypeExtractor.GetTypeForCodeGeneration(fieldSymbol.Type),
            fieldSymbol.NullableAnnotation == NullableAnnotation.Annotated,
            false,
            CollectionInference.InferCollectionType(fieldSymbol.Type),
            GetFluentSymbolComments(fieldSymbol));
    }

    private static MemberSymbolInfo CreateMemberSymbolInfo(
        IPropertySymbol propertySymbol,
        string declaringClassNameWithTypeParameters)
    {
        return new MemberSymbolInfo(
            propertySymbol.Name,
            propertySymbol.Type.ToString(),
            declaringClassNameWithTypeParameters,
            propertySymbol.DeclaredAccessibility,
            CodeTypeExtractor.GetTypeForCodeGeneration(propertySymbol.Type),
            propertySymbol.NullableAnnotation == NullableAnnotation.Annotated,
            true,
            CollectionInference.InferCollectionType(propertySymbol.Type),
            GetFluentSymbolComments(propertySymbol));
    }

    private static MethodSymbolInfo CreateMethodSymbolInfo(
        IMethodSymbol methodSymbol,
        string declaringClassNameWithTypeParameters)
    {
        GenericInfo? genericInfo = GetGenericInfo(methodSymbol);
        Dictionary<string, int> typeParameterNameToTypeParameterPosition = genericInfo == null
            ? new Dictionary<string, int>()
            : genericInfo.Parameters.ToDictionary(p => p.ParameterName, p => p.ParameterPosition);

        IReadOnlyCollection<ParameterSymbolInfo> parameterInfos =
            methodSymbol.Parameters.Select(p => CreateParameterSymbolInfo(p, typeParameterNameToTypeParameterPosition))
                .ToArray();

        return new MethodSymbolInfo(
            methodSymbol.Name,
            declaringClassNameWithTypeParameters,
            methodSymbol.DeclaredAccessibility,
            genericInfo,
            parameterInfos,
            CodeTypeExtractor.GetTypeForCodeGeneration(methodSymbol.ReturnType),
            GetFluentSymbolComments(methodSymbol));
    }

    private static GenericInfo? GetGenericInfo(IMethodSymbol methodSymbol)
    {
        return methodSymbol.IsGenericMethod ? GenericInfo.Create(methodSymbol.TypeParameters) : null;
    }

    private static ParameterSymbolInfo CreateParameterSymbolInfo(
        IParameterSymbol parameterSymbol,
        Dictionary<string, int> typeParameterNameToTypeParameterPosition)
    {
        string typeForCodeGeneration = CodeTypeExtractor.GetTypeForCodeGeneration(parameterSymbol.Type);
        int? genericTypeParameterPosition =
            typeParameterNameToTypeParameterPosition.TryGetValue(typeForCodeGeneration, out int parameterPosition)
                ? parameterPosition
                : null;

        return new ParameterSymbolInfo(
            parameterSymbol.Name,
            typeForCodeGeneration,
            parameterSymbol.NullableAnnotation == NullableAnnotation.Annotated,
            parameterSymbol.Type.IsReferenceType,
            GetDefaultValueAsCode(parameterSymbol),
            genericTypeParameterPosition,
            GetParameterKinds(parameterSymbol));
    }

    private static string? GetDefaultValueAsCode(IParameterSymbol parameterSymbol)
    {
        if (!parameterSymbol.HasExplicitDefaultValue)
        {
            return null;
        }

        if (parameterSymbol.ExplicitDefaultValue == null && parameterSymbol.Type.IsReferenceType)
        {
            return "null";
        }

        if (parameterSymbol.ExplicitDefaultValue == null && parameterSymbol.Type.IsValueType)
        {
            return "default";
        }

        if (parameterSymbol.ExplicitDefaultValue == null &&
            parameterSymbol.Type is { IsReferenceType: false, IsValueType: false }) // unconstrained type parameter
        {
            return "default";
        }

        return parameterSymbol.ExplicitDefaultValue! switch
        {
            string s => $"\"{s}\"",
            { } o => o.ToString(),
        };
    }

    private static ParameterKinds GetParameterKinds(IParameterSymbol parameterSymbol)
    {
        ParameterKinds parameterKinds = ParameterKinds.None;

        if (parameterSymbol.IsParams)
        {
            parameterKinds |= ParameterKinds.Params;
        }

        switch (parameterSymbol.RefKind)
        {
            case RefKind.None:
                break;
            case RefKind.Ref:
                parameterKinds |= ParameterKinds.Ref;
                break;
            case RefKind.Out:
                parameterKinds |= ParameterKinds.Out;
                break;
            case RefKind.In:
                parameterKinds |= ParameterKinds.In;
                break;
            default:
                throw new ArgumentException($"RefKind {parameterSymbol.RefKind} is not handled.");
        }

        return parameterKinds;
    }

    private static readonly Regex fluentApiCommentStart = new Regex(@"^\s*////(?!/)", RegexOptions.Compiled);
    private static Comments GetFluentSymbolComments(ISymbol symbol)
    {
        SyntaxReference? syntaxRef = symbol.DeclaringSyntaxReferences.FirstOrDefault();
        if (syntaxRef == null)
        {
            return new Comments(Array.Empty<Comment>());
        }

        SyntaxNode syntaxNode = syntaxRef.GetSyntax();
        SyntaxTriviaList leadingTrivia = syntaxNode.GetLeadingTrivia();

        List<string> commentLines = new List<string>();

        foreach (SyntaxTrivia syntaxTrivia in leadingTrivia)
        {
            if (!syntaxTrivia.IsKind(SyntaxKind.SingleLineCommentTrivia))
            {
                continue;
            }

            string str = syntaxTrivia.ToString();
            if (fluentApiCommentStart.IsMatch(str))
            {
                commentLines.Add(str.TrimStart('/', ' '));
            }
        }

        string comments = string.Join(SourceGenerator.GeneratorConfig.NewLineString, commentLines);
        return FluentCommentsParser.Parse(comments);
    }
}