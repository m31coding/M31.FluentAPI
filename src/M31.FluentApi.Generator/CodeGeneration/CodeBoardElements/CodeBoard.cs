using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation.Forks;
using M31.FluentApi.Generator.SourceAnalyzers;
using M31.FluentApi.Generator.SourceGenerators;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using M31.FluentApi.Generator.SourceGenerators.Generics;
using Microsoft.CodeAnalysis;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

// black board approach
internal class CodeBoard
{
    private readonly List<Diagnostic> diagnostics;

    private CodeBoard(
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos,
        IReadOnlyCollection<FluentApiInfoGroup> fluentApiInfoGroups,
        BuilderAndTargetInfo builderAndTargetInfo,
        CodeFile codeFile,
        Class builderClass,
        string newLineString,
        CancellationToken cancellationToken)
    {
        FluentApiInfos = fluentApiInfos;
        FluentApiInfoGroups = fluentApiInfoGroups;
        Info = builderAndTargetInfo;
        CodeFile = codeFile;
        BuilderClass = builderClass;
        Constructor = null;
        StaticConstructor = null;
        InnerBodyCreationDelegates = new InnerBodyCreationDelegates();
        BuilderMethodToAttributeData = new Dictionary<BuilderMethod, AttributeDataExtended>();
        Forks = new List<Fork>();
        BuilderClassFields = new BuilderClassFields();
        diagnostics = new List<Diagnostic>();
        NewLineString = newLineString;
        CancellationToken = cancellationToken;
    }

    internal IReadOnlyCollection<FluentApiInfo> FluentApiInfos { get; }
    internal IReadOnlyCollection<FluentApiInfoGroup> FluentApiInfoGroups { get; }
    internal BuilderAndTargetInfo Info { get; }
    internal CodeFile CodeFile { get; }
    internal Class BuilderClass { get; }
    internal Method? Constructor { get; set; }
    internal Method? StaticConstructor { get; set; }
    internal InnerBodyCreationDelegates InnerBodyCreationDelegates { get; }
    internal Dictionary<BuilderMethod, AttributeDataExtended> BuilderMethodToAttributeData { get; }
    internal IReadOnlyList<Fork> Forks { get; set; }
    internal BuilderClassFields BuilderClassFields { get; }
    internal IReadOnlyCollection<Diagnostic> Diagnostics => diagnostics;
    internal string NewLineString { get; }
    internal CancellationToken CancellationToken { get; }
    internal bool CancellationRequested => CancellationToken.IsCancellationRequested;
    internal bool HasErrors => diagnostics.HaveErrors();
    internal bool HasInterfaceMethods => BuilderClass.Methods.OfType<InterfaceMethod>().Any();

    internal static CodeBoard Create(
        BuilderAndTargetInfo builderAndTargetInfo,
        IReadOnlyCollection<FluentApiInfo> fluentApiInfos,
        IReadOnlyCollection<FluentApiInfoGroup> fluentApiInfoGroups,
        IReadOnlyCollection<string> usingStatements,
        string newLineString,
        CancellationToken cancellationToken)
    {
        CodeFile codeFile = new CodeFile(builderAndTargetInfo.Namespace, newLineString);
        Class builderClass = new Class(builderAndTargetInfo.BuilderClassName);
        CodeBoard codeBoard = new CodeBoard(
            fluentApiInfos,
            fluentApiInfoGroups,
            builderAndTargetInfo,
            codeFile,
            builderClass,
            newLineString,
            cancellationToken);

        CreateHeader(codeFile);
        codeFile.AddPreprocessorDirective(
            "#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member");
        codeFile.AddPreprocessorDirective("#nullable enable");

        if (builderAndTargetInfo.GenericInfo != null)
        {
            foreach (GenericTypeParameter genericTypeParameter in builderAndTargetInfo.GenericInfo.Parameters)
            {
                builderClass.AddGenericParameter(
                    genericTypeParameter.ParameterName,
                    genericTypeParameter.Constraints.GetConstraintsForCodeGeneration());
            }
        }

        builderClass.AddModifiers(builderAndTargetInfo.DefaultAccessModifier);
        codeFile.AddDefinition(builderClass);

        foreach (string usingStatement in usingStatements)
        {
            codeFile.AddUsingStatement(usingStatement);
        }

        return codeBoard;
    }

    private static void CreateHeader(CodeFile codeFile)
    {
        codeFile.AddHeaderLine("// <auto-generated/>");
        codeFile.AddHeaderLine("// This code was generated by the library M31.FluentAPI.");
        codeFile.AddHeaderLine("// Changes to this file may cause incorrect behavior and will be lost if the code is " +
                               "regenerated.");
    }

    internal CodeBuilder NewCodeBuilder()
    {
        return new CodeBuilder(NewLineString);
    }

    internal void ReportDiagnostic(Diagnostic diagnostic)
    {
        diagnostics.Add(diagnostic);
    }
}