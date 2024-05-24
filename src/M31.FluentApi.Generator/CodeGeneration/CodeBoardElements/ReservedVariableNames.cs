namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class ReservedVariableNames
{
    private readonly HashSet<string> globallyReserved;
    private readonly HashSet<string> locallyReserved;

    private ReservedVariableNames(HashSet<string> globallyReserved, HashSet<string> locallyReserved)
    {
        this.globallyReserved = globallyReserved;
        this.locallyReserved = locallyReserved;
    }

    internal ReservedVariableNames()
    {
        globallyReserved = new HashSet<string>();
        locallyReserved = new HashSet<string>();
    }

    internal ReservedVariableNames NewLocalScope()
    {
        return new ReservedVariableNames(globallyReserved, new HashSet<string>());
    }

    internal void ReserveLocalVariableNames(IEnumerable<string> reservedVariableNames)
    {
        foreach (string reservedVariableName in reservedVariableNames)
        {
            locallyReserved.Add(reservedVariableName);
        }
    }

    internal string GetNewGlobalVariableName(string desiredVariableName)
    {
        string newVariableName = GetNewVariableName(desiredVariableName);
        globallyReserved.Add(newVariableName);
        return newVariableName;
    }

    internal string GetNewLocalVariableName(string desiredVariableName)
    {
        string newVariableName = GetNewVariableName(desiredVariableName);
        locallyReserved.Add(newVariableName);
        return newVariableName;
    }

    private string GetNewVariableName(string desiredVariableName)
    {
        string newVariableName = desiredVariableName;
        int i = 2;

        while (globallyReserved.Contains(newVariableName) || locallyReserved.Contains(newVariableName))
        {
            newVariableName = $"{desiredVariableName}{i++}";
        }

        return newVariableName;
    }
}