namespace M31.FluentApi.Generator.SourceGenerators;

internal class ClassInfoResult
{
    internal ClassInfoResult(
        FluentApiClassInfo? classInfo,
        ClassInfoReport classInfoReport)
    {
        ClassInfo = classInfo;
        ClassInfoReport = classInfoReport;
    }

    internal ClassInfoResult(ClassInfoReport classInfoReport)
    {
        ClassInfo = null;
        ClassInfoReport = classInfoReport;
    }

    internal FluentApiClassInfo? ClassInfo { get; }
    internal ClassInfoReport ClassInfoReport { get; }
}