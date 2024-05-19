using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators.AttributeElements;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;

internal class DuplicateMethodsChecker : ICodeBoardActor
{
    private static readonly HashSet<string> reservedMethodNames = new HashSet<string>()
    {
        "InitialStep",
    };

    public void Modify(CodeBoard codeBoard)
    {
        ReportReservedMethodNames(codeBoard);

        IReadOnlyCollection<DuplicateMethods> groupsOfDuplicateMethods
            = DuplicateMethodsFinder.FindGroupsOfDuplicateMethods(codeBoard.BuilderMethodToAttributeData.Keys);
        ReportDuplicates(groupsOfDuplicateMethods, codeBoard);
    }

    private void ReportReservedMethodNames(CodeBoard codeBoard)
    {
        foreach (KeyValuePair<BuilderMethod, AttributeDataExtended> methodAttributePair in
                 codeBoard.BuilderMethodToAttributeData)
        {
            if (reservedMethodNames.Contains(methodAttributePair.Key.MethodName))
            {
                codeBoard.ReportDiagnostic(
                    ReservedMethodName.CreateDiagnostic(methodAttributePair.Value, methodAttributePair.Key.MethodName));
            }
        }
    }

    private void ReportDuplicates(IReadOnlyCollection<DuplicateMethods> groupsOfDuplicateMethods, CodeBoard codeBoard)
    {
        foreach (DuplicateMethods duplicateMethods in groupsOfDuplicateMethods)
        {
            ReportDuplicates(duplicateMethods, codeBoard);
        }
    }

    private void ReportDuplicates(DuplicateMethods duplicateMethods, CodeBoard codeBoard)
    {
        if (reservedMethodNames.Contains(duplicateMethods.MethodName))
        {
            return;
        }

        AttributeData[] attributeData =
            duplicateMethods.Duplicates
                .Select(d => codeBoard.BuilderMethodToAttributeData[d.BuilderMethod].AttributeData)
                .ToArray();

        codeBoard.ReportDiagnostic(
            DuplicateFluentApiMethod.CreateDiagnostic(duplicateMethods.MethodName, attributeData));
    }
}