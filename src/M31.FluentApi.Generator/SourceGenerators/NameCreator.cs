namespace M31.FluentApi.Generator.SourceGenerators;

internal static class NameCreator
{
    internal static string CreateName(string template, string name)
    {
        return template.Replace("{Name}", name);
    }

    internal static string CreateName(string template, string name, string singularName)
    {
        return template.Replace("{Name}", name).Replace("{SingularName}", singularName);
    }
}