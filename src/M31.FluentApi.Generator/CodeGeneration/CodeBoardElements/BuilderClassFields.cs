namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class BuilderClassFields
{
    private readonly HashSet<string> fieldNames;

    internal BuilderClassFields()
    {
        this.fieldNames = new HashSet<string>();
    }

    internal void ReserveFieldName(string fieldName)
    {
        if (fieldNames.Contains(fieldName))
        {
            throw new ArgumentException($"A field with name {fieldName} already exists.");
        }

        fieldNames.Add(fieldName);
    }

    internal string GetFieldName(FluentApiSymbolInfo symbolInfo, string desiredFieldName)
    {
        string fieldName = desiredFieldName;
        int i = 2;

        while (fieldNames.Contains(fieldName))
        {
            fieldName = $"{desiredFieldName}{i++}";
        }

        fieldNames.Add(fieldName);

        return fieldName;
    }
}