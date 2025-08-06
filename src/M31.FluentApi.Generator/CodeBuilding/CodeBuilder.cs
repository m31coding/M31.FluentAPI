using System.Text;

namespace M31.FluentApi.Generator.CodeBuilding;

internal class CodeBuilder
{
    private readonly string newLineString;
    internal int IndentationLevel { get; set; }
    private readonly StringBuilder stringBuilder;
    private bool endOfLine;
    private bool blankLine;

    internal CodeBuilder(string newLineString)
    {
        this.newLineString = newLineString;
        stringBuilder = new StringBuilder();
        endOfLine = false;
        blankLine = false;
    }

    internal CodeBuilder StartLine()
    {
        Append(GetIndentation());
        return this;
    }

    internal CodeBuilder EndLine()
    {
        endOfLine = true;
        return this;
    }

    internal CodeBuilder Space()
    {
        return Append(" ");
    }

    internal CodeBuilder BlankLine()
    {
        endOfLine = true;
        blankLine = true;
        return this;
    }

    internal CodeBuilder Indent()
    {
        IndentationLevel++;
        return this;
    }

    internal CodeBuilder Unindent()
    {
        IndentationLevel--;
        return this;
    }

    internal CodeBuilder OpenBlock()
    {
        AppendLine("{");
        IndentationLevel++;
        return this;
    }

    internal CodeBuilder CloseBlock()
    {
        IndentationLevel--;
        blankLine = false;
        AppendLine("}");
        return this;
    }

    internal CodeBuilder Append(string? code)
    {
        if (code is not null)
        {
            InsertNewLines();
            stringBuilder.Append(code);
        }

        return this;
    }

    internal CodeBuilder Append(ICode? code, bool condition)
    {
        if (condition)
        {
            Append(code);
        }

        return this;
    }

    internal CodeBuilder Append(string? code, bool condition)
    {
        if (condition)
        {
            Append(code);
        }

        return this;
    }

    internal CodeBuilder Append(Func<string?> createCode, bool condition)
    {
        if (condition)
        {
            Append(createCode());
        }

        return this;
    }

    internal CodeBuilder Append(IEnumerable<string> code, string separator)
    {
        return AppendSeparated(code, () => stringBuilder.Append(separator), c => Append(c));
    }

    internal CodeBuilder AppendNewLineSeparated(IEnumerable<string> code)
    {
        return AppendSeparated(code, () => EndLine(), c => Append(GetIndentation()).Append(c));
    }

    internal CodeBuilder Append(ICode? code)
    {
        return code?.AppendCode(this) ?? this;
    }

    internal CodeBuilder AppendLine(string code)
    {
        return Append(GetIndentation()).Append(code).EndLine();
    }

    internal CodeBuilder AppendLines(IEnumerable<string> code)
    {
        foreach (string c in code)
        {
            AppendLine(c);
        }

        return this;
    }

    internal CodeBuilder Append(IEnumerable<ICode> code)
    {
        foreach (ICode c in code)
        {
            c.AppendCode(this);
        }

        return this;
    }

    internal CodeBuilder AppendWithBlankLines(IEnumerable<ICode> code)
    {
        foreach (ICode c in code)
        {
            c.AppendCode(this);
            BlankLine();
        }

        return this;
    }

    internal CodeBuilder Append(IEnumerable<ICode> code, string separator)
    {
        return AppendSeparated(code, () => stringBuilder.Append(separator), c => c.AppendCode(this));
    }

    internal CodeBuilder AppendNewLineSeparated(IEnumerable<ICode> code)
    {
        return AppendSeparated(code, () => EndLine(), c => Append(GetIndentation()).Append(c));
    }

    private CodeBuilder AppendSeparated<T>(IEnumerable<T> code, Action separationAction, Action<T> append)
    {
        using IEnumerator<T> en = code.GetEnumerator();

        if (!en.MoveNext())
        {
            return this;
        }

        append(en.Current);

        if (!en.MoveNext())
        {
            return this;
        }

        do
        {
            separationAction();
            append(en.Current);
        }
        while (en.MoveNext());

        return this;
    }

    private string GetIndentation()
    {
        return IndentationLevel switch
        {
            0 => string.Empty,
            1 => "    ",
            2 => "        ",
            3 => "            ",
            4 => "                ",
            _ => throw new NotSupportedException($"Indentation level {IndentationLevel} is not supported.")
        };
    }

    internal const string OneLevelIndentation = "    ";

    private void InsertNewLines()
    {
        if (endOfLine)
        {
            if (blankLine)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(newLineString).Append(newLineString);
                }

                blankLine = false;
            }
            else
            {
                stringBuilder.Append(newLineString);
            }

            endOfLine = false;
        }
    }

    public override string ToString()
    {
        return stringBuilder.ToString();
    }
}