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
            .Append("params ", Contains(ParameterKinds.Params))
            .Append("ref ", Contains(ParameterKinds.Ref))
            .Append("in ", Contains(ParameterKinds.In))
            .Append("out ", Contains(ParameterKinds.Out));
    }

    internal bool Contains(ParameterKinds parameterKinds)
    {
        return ParameterKinds.HasFlag(parameterKinds);
    }

    internal string ToCallsiteAnnotations()
    {
        if (ParameterKinds == ParameterKinds.None)
        {
            return string.Empty;
        }

        return new CodeBuilder()
            .Append("ref ", Contains(ParameterKinds.Ref))
            .Append("in ", Contains(ParameterKinds.In))
            .Append("out ", Contains(ParameterKinds.Out)).ToString();
    }
}