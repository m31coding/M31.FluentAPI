using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using Microsoft.CodeAnalysis;
using static M31.FluentApi.Generator.SourceAnalyzers.FluentApiDiagnostics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.DuplicateMethodsChecking;

internal class DuplicateMethodsChecker : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        IReadOnlyCollection<DuplicateMethods> groupsOfDuplicateMethods
            = DuplicateMethodsFinder.FindGroupsOfDuplicateMethods(codeBoard.BuilderMethodToAttributeData.Keys);
        ReportDuplicates(groupsOfDuplicateMethods, codeBoard);
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
        AttributeData[] attributeData =
            duplicateMethods.Duplicates
                .Select(d => codeBoard.BuilderMethodToAttributeData[d.BuilderMethod].AttributeData)
                .ToArray();

        codeBoard.ReportDiagnostic(
            DuplicateFluentApiMethod.CreateDiagnostic(duplicateMethods.MethodName, attributeData));
    }
}