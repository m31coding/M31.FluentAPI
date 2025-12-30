// ReSharper disable ParameterHidesMember

namespace M31.FluentApi.Generator.CodeBuilding;

internal class Interface : ICode
{
    private readonly List<CommentedMethodSignature> methodSignatures;
    private readonly List<string> baseInterfaces;

    internal Interface(string accessModifier, string name)
    {
        AccessModifier = accessModifier;
        Name = name;
        methodSignatures = new List<CommentedMethodSignature>();
        baseInterfaces = new List<string>();
    }

    internal string AccessModifier { get; }
    internal string Name { get; }
    internal IReadOnlyCollection<CommentedMethodSignature> MethodSignatures => methodSignatures;
    internal IReadOnlyCollection<string> BaseInterfaces => baseInterfaces;

    internal void AddMethodSignature(CommentedMethodSignature methodSignature)
    {
        if (!methodSignature.MethodSignature.IsStandaloneSignature)
        {
            throw new ArgumentException("Expected a stand-alone method signature.");
        }

        methodSignatures.Add(methodSignature);
    }

    internal void AddBaseInterface(string baseInterface)
    {
        baseInterfaces.Add(baseInterface);
    }

    internal void AddBaseInterfaces(IEnumerable<string> baseInterfaces)
    {
        this.baseInterfaces.AddRange(baseInterfaces);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .StartLine()
            .Append($"{AccessModifier} interface {Name}")
            .Append($" : {string.Join(", ", baseInterfaces)}", baseInterfaces.Count > 0)
            .EndLine()
            .OpenBlock()
            .AppendWithBlankLines(methodSignatures)
            .CloseBlock();
    }
}