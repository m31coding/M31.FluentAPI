namespace M31.FluentApi.Generator.CodeBuilding;

internal class ParameterAnnotations : ICode
{
    internal ParameterAnnotations(ParameterKinds parameterKinds)
    {
        ParameterKinds = parameterKinds;
    }

    internal ParameterKinds ParameterKinds { get; }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .Append("params ", ParameterKinds.HasFlag(ParameterKinds.Params))
            .Append("ref ", ParameterKinds.HasFlag(ParameterKinds.Ref))
            .Append("in ", ParameterKinds.HasFlag(ParameterKinds.In))
            .Append("out ", ParameterKinds.HasFlag(ParameterKinds.Out));
    }

    internal string ToCallsiteAnnotations()
    {
        if (ParameterKinds == ParameterKinds.None)
        {
            return string.Empty;
        }

        return new CodeBuilder()
            .Append("ref ", ParameterKinds.HasFlag(ParameterKinds.Ref))
            .Append("in ", ParameterKinds.HasFlag(ParameterKinds.In))
            .Append("out ", ParameterKinds.HasFlag(ParameterKinds.Out)).ToString();
    }
}