using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class CallMethodCode
{
    private readonly BuildCallMethodCode buildCallMethodCode;

    internal CallMethodCode(BuildCallMethodCode buildCallMethodCode)
    {
        this.buildCallMethodCode = buildCallMethodCode;
    }

    internal string BuildCode(string instancePrefix, IReadOnlyCollection<Parameter> outerMethodParameters)
    {
        return buildCallMethodCode(instancePrefix, outerMethodParameters);
    }

    public override string ToString()
    {
        return buildCallMethodCode(string.Empty, Array.Empty<Parameter>());
    }
}