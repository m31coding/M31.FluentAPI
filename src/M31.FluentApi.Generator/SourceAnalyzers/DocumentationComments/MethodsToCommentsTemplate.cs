using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.MethodCreation;

namespace M31.FluentApi.Generator.SourceAnalyzers.DocumentationComments;

internal class MethodsToCommentsTemplate
{
    private readonly List<string> comments;

    private MethodsToCommentsTemplate()
    {
        comments = new List<string>();
    }

    internal static IReadOnlyCollection<string> CreateCommentsTemplate(BuilderMethods builderMethods)
    {
        if (builderMethods.Methods.Count == 0)
        {
            return Array.Empty<string>();
        }

        MethodsToCommentsTemplate instance = new MethodsToCommentsTemplate();

        IGrouping<string, BuilderMethod>[] groups = builderMethods.Methods.GroupBy(m => m.MethodName).ToArray();

        if (groups.Length == 1)
        {
            instance.CreateCommentsTemplateWithoutMethodNames(groups[0].ToArray());
        }
        else
        {
            foreach (IGrouping<string, BuilderMethod> group in groups)
            {
                instance.CreateCommentsTemplateWithMethodNames(group.ToArray());
            }
        }

        return instance.comments;
    }

    private void CreateCommentsTemplateWithoutMethodNames(BuilderMethod[] sameNameBuilderMethods)
    {
        comments.Add("/// <fluentSummary>");
        comments.Add("/// ...");
        comments.Add("/// </fluentSummary>");

        foreach (string parameterName in GetDistinctParameterNames(sameNameBuilderMethods))
        {
            comments.Add($"/// <fluentParam name=\"{parameterName}\">...</fluentParam>");
        }
    }

    private void CreateCommentsTemplateWithMethodNames(BuilderMethod[] sameNameBuilderMethods)
    {
        string method = sameNameBuilderMethods[0].MethodName;

        comments.Add($"/// <fluentSummary method=\"{method}\">");
        comments.Add("/// ...");
        comments.Add("/// </fluentSummary>");

        foreach (string parameterName in GetDistinctParameterNames(sameNameBuilderMethods))
        {
            comments.Add($"/// <fluentParam method=\"{method}\" name=\"{parameterName}\">...</fluentParam>");
        }
    }

    private static IEnumerable<string> GetDistinctParameterNames(BuilderMethod[] builderMethods)
    {
        return builderMethods.SelectMany(m => m.Parameters).Select(p => p.Name).Distinct();
    }
}