using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal class ComputeValueCode
{
    private ComputeValueCode(string targetMember, Parameter? parameter, string code)
    {
        TargetMember = targetMember;
        Parameter = parameter;
        Code = code;
    }

    internal string TargetMember { get; }
    internal Parameter? Parameter { get; }
    internal string Code { get; }

    internal static ComputeValueCode Create(
        string targetMember,
        Parameter parameter,
        Func<string, string> buildCodeWithParameter)
    {
        return new ComputeValueCode(targetMember, parameter, buildCodeWithParameter(parameter.Name));
    }

    internal static ComputeValueCode Create(string targetMember, Parameter parameter)
    {
        return Create(targetMember, parameter, p => p);
    }

    internal static ComputeValueCode Create(string targetMember, string valueCode)
    {
        return new ComputeValueCode(targetMember, null, valueCode);
    }

    public override string ToString() => Code;
}