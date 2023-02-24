using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal abstract class LineGeneratorBase<TSymbolInfo>
    where TSymbolInfo : FluentApiSymbolInfo
{
    protected CodeBoard CodeBoard { get; }

    internal LineGeneratorBase(CodeBoard codeBoard)
    {
        this.CodeBoard = codeBoard;
        ReflectionRequired = false;
    }

    internal bool ReflectionRequired { get; private set; }

    internal void GenerateLine(TSymbolInfo symbolInfo)
    {
        if (!symbolInfo.RequiresReflection)
        {
            GenerateLineWithoutReflection(symbolInfo);
        }
        else
        {
            GenerateLineWithReflectionAndFields(symbolInfo);
            ReflectionRequired = true;
        }
    }

    protected abstract string SymbolType(TSymbolInfo symbolInfo);
    protected abstract void GenerateLineWithoutReflection(TSymbolInfo symbolInfo);
    protected abstract void GenerateLineWithReflection(TSymbolInfo symbolInfo, string infoFieldName);
    protected abstract void InitializeInfoField(string fieldName, TSymbolInfo symbolInfo);

    private void GenerateLineWithReflectionAndFields(TSymbolInfo symbolInfo)
    {
        string symbolType = SymbolType(symbolInfo);

        // semesterPropertyInfo / semesterFieldInfo / semesterMethodInfo
        string infoFieldName = $"{symbolInfo.NameInCamelCase}{symbolType}Info";
        infoFieldName = CodeBoard.BuilderClassFields.GetFieldName(symbolInfo, infoFieldName);

        GenerateInfoField(symbolType, infoFieldName);
        InitializeInfoField(infoFieldName, symbolInfo);

        GenerateLineWithReflection(symbolInfo, infoFieldName);
    }

    private void GenerateInfoField(string symbolType, string fieldName)
    {
        // private static readonly PropertyInfo semesterPropertyInfo;
        Field field = new Field($"{symbolType}Info", fieldName);
        field.AddModifiers("private", "static", "readonly");
        CodeBoard.BuilderClass.AddField(field);
    }

    protected string InfoFieldBindingFlagsArgument(TSymbolInfo symbolInfo)
    {
        string accessibilityBindingFlag = symbolInfo.Accessibility.IsPublicOrInternal()
            ? "BindingFlags.Public"
            : "BindingFlags.NonPublic";

        return $"BindingFlags.Instance | {accessibilityBindingFlag}";
    }
}