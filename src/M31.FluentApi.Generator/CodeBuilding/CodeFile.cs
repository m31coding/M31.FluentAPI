namespace M31.FluentApi.Generator.CodeBuilding;

internal class CodeFile : ICode
{
    private readonly string newLineString;
    private readonly List<string> header;
    private readonly List<string> preprocessorDirectives;
    private readonly List<string> usingStatements;
    private readonly List<ICode> definitions;

    internal CodeFile(string? @namespace, string newLineString)
    {
        this.newLineString = newLineString;
        Namespace = @namespace;
        header = new List<string>();
        preprocessorDirectives = new List<string>();
        definitions = new List<ICode>();
        usingStatements = new List<string>();
    }

    internal string? Namespace { get; }
    internal IReadOnlyCollection<string> Header => header;
    internal IReadOnlyCollection<ICode> Definitions => definitions;
    internal IReadOnlyCollection<string> PreprocessorDirectives => preprocessorDirectives;
    internal IReadOnlyCollection<string> UsingStatements => usingStatements;

    internal void AddHeaderLine(string line)
    {
        header.Add(line);
    }

    internal void AddPreprocessorDirective(string preprocessorDirective)
    {
        if (!preprocessorDirectives.Contains(preprocessorDirective))
        {
            preprocessorDirectives.Add(preprocessorDirective);
        }
    }

    internal void AddUsing(string @using)
    {
        AddUsingStatement($"using {@using};");
    }

    internal void AddUsingStatement(string usingStatement)
    {
        if (!usingStatements.Contains(usingStatement))
        {
            usingStatements.Add(usingStatement);
        }
    }

    internal void AddDefinition(ICode definition)
    {
        definitions.Add(definition);
    }

    public CodeBuilder AppendCode(CodeBuilder codeBuilder)
    {
        codeBuilder
            .AppendLines(header)
            .BlankLine()
            .AppendLines(preprocessorDirectives)
            .BlankLine()
            .AppendLines(usingStatements)
            .BlankLine();

        if (Namespace != null)
        {
            codeBuilder
                .AppendLine($"namespace {Namespace};")
                .BlankLine();
        }

        codeBuilder.AppendWithBlankLines(definitions);

        return codeBuilder;
    }

    public override string ToString()
    {
        return new CodeBuilder(newLineString).Append(this).ToString();
    }
}