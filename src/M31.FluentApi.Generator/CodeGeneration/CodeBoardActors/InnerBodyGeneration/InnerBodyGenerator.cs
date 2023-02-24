using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal class InnerBodyGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        LineForMemberGenerator lineForMemberGenerator = new LineForMemberGenerator(codeBoard);
        LineForMethodGenerator lineForMethodGenerator = new LineForMethodGenerator(codeBoard);

        foreach (FluentApiSymbolInfo symbolInfo in codeBoard.FluentApiInfos.Select(m => m.SymbolInfo))
        {
            if (codeBoard.CancellationToken.IsCancellationRequested)
            {
                break;
            }

            switch (symbolInfo)
            {
                case MemberSymbolInfo memberInfo:
                    lineForMemberGenerator.GenerateLine(memberInfo);
                    break;

                case MethodSymbolInfo methodInfo:
                    lineForMethodGenerator.GenerateLine(methodInfo);
                    break;

                default:
                    throw new ArgumentException($"Unknown symbol info type: {symbolInfo.GetType()}");
            }
        }

        if (lineForMemberGenerator.ReflectionRequired || lineForMethodGenerator.ReflectionRequired)
        {
            ImportReflectionNamespace(codeBoard);
        }
    }

    private static void ImportReflectionNamespace(CodeBoard codeBoard)
    {
        codeBoard.CodeFile.AddUsing("System.Reflection");
    }
}