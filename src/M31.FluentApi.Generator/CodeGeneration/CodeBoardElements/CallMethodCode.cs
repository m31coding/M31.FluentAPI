using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class CallMethodCode
{
    private readonly BuildCallMethodCode buildCallMethodCode;
    private readonly string newLineString;

    internal CallMethodCode(BuildCallMethodCode buildCallMethodCode, string newLineString)
    {
        this.buildCallMethodCode = buildCallMethodCode;
        this.newLineString = newLineString;
    }

    internal List<string> BuildCode(
        string instancePrefix,
        IReadOnlyCollection<Parameter> outerMethodParameters,
        string? returnType)
    {
        return buildCallMethodCode(instancePrefix, outerMethodParameters, returnType);
    }

    public override string ToString()
    {
        return string.Join(newLineString, buildCallMethodCode(string.Empty, Array.Empty<Parameter>(), null));
    }
}