using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.Commons;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.InnerBodyGeneration;

internal abstract class InnerBodyGeneratorBase<TSymbolInfo>
    where TSymbolInfo : FluentApiSymbolInfo
{
    protected CodeBoard CodeBoard { get; }

    internal InnerBodyGeneratorBase(CodeBoard codeBoard)
    {
        this.CodeBoard = codeBoard;
        ReflectionRequired = false;
    }

    internal bool ReflectionRequired { get; private set; }

    internal void GenerateInnerBody(TSymbolInfo symbolInfo)
    {
        if (!symbolInfo.RequiresReflection)
        {
            GenerateInnerBodyWithoutReflection(symbolInfo);
        }
        else
        {
            GenerateInnerBodyWithReflectionAndFields(symbolInfo);
            ReflectionRequired = true;
        }
    }

    protected abstract string SymbolType(TSymbolInfo symbolInfo);
    protected abstract void GenerateInnerBodyWithoutReflection(TSymbolInfo symbolInfo);
    protected abstract void GenerateInnerBodyWithReflection(TSymbolInfo symbolInfo, string infoFieldName);
    protected abstract void InitializeInfoField(string fieldName, TSymbolInfo symbolInfo);

    private void GenerateInnerBodyWithReflectionAndFields(TSymbolInfo symbolInfo)
    {
        string symbolType = SymbolType(symbolInfo);

        // semesterPropertyInfo / semesterFieldInfo / semesterMethodInfo
        string infoFieldName = $"{symbolInfo.NameInCamelCase}{symbolType}Info";
        infoFieldName = CodeBoard.ReservedVariableNames.GetNewFieldName(infoFieldName);

        GenerateInfoField(symbolType, infoFieldName);
        InitializeInfoField(infoFieldName, symbolInfo);

        GenerateInnerBodyWithReflection(symbolInfo, infoFieldName);
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