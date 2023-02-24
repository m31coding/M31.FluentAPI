namespace M31.FluentApi.Generator.CodeBuilding;

internal class Interface : ICode
{
    private readonly List<MethodSignature> methodSignatures;

    internal Interface(string accessModifier, string name)
    {
        AccessModifier = accessModifier;
        Name = name;
        methodSignatures = new List<MethodSignature>();
    }

    internal string AccessModifier { get; }
    internal string Name { get; }
    internal IReadOnlyCollection<MethodSignature> MethodSignatures => methodSignatures;

    internal void AddMethodSignature(MethodSignature methodSignature)
    {
        if (!methodSignature.IsStandAlone)
        {
            throw new ArgumentException("Expected a stand-alone method signature.");
        }

        methodSignatures.Add(methodSignature);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        return codeBuilder
            .AppendLine($"{AccessModifier} interface {Name}")
            .OpenBlock()
            .Append(methodSignatures)
            .CloseBlock();
    }
}