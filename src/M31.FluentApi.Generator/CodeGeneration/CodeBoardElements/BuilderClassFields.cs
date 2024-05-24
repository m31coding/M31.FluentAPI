namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class BuilderClassFields
{
    private readonly HashSet<string> fieldNames;

    internal BuilderClassFields()
    {
        this.fieldNames = new HashSet<string>();
    }

    internal string GetNewFieldName(string desiredFieldName)
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