using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class EntityFieldGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        string fieldName = codeBoard.BuilderClassFields.GetNewFieldName(codeBoard.Info.ClassInstanceName);

        // private readonly Student<T1, T2> student;
        Field field = new Field(codeBoard.Info.FluentApiClassName, fieldName);

        if (codeBoard.Info.GenericInfo != null)
        {
            field.AddGenericParameters(codeBoard.Info.GenericInfo.ParameterStrings);
        }

        field.AddModifiers("private");

        if (!codeBoard.Info.FluentApiTypeIsStruct)
        {
            field.AddModifiers("readonly");
        }

        codeBoard.BuilderClass.AddField(field);
    }
}