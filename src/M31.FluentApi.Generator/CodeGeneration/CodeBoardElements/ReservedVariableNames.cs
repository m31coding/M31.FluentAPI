namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ReservedVariableNames
{
    private readonly HashSet<string> reservedFieldNames;

    internal ReservedVariableNames()
    {
        reservedFieldNames = new HashSet<string>();
    }

    internal string GetNewFieldName(string desiredFieldName)
    {
        string fieldName = desiredFieldName;
        int i = 2;

        while (reservedFieldNames.Contains(fieldName))
        {
            fieldName = $"{desiredFieldName}{i++}";
        }

        reservedFieldNames.Add(fieldName);

        return fieldName;
    }
}