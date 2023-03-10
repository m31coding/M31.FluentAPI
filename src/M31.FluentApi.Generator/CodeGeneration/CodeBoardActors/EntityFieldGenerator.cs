using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class EntityFieldGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        // private readonly Student student;
        Field field = new Field(codeBoard.Info.FluentApiClassName, codeBoard.Info.ClassInstanceName);
        field.AddModifiers("private");
        codeBoard.BuilderClassFields.ReserveFieldName(field.Name);

        if (!codeBoard.Info.FluentApiTypeIsStruct)
        {
            field.AddModifiers("readonly");
        }

        codeBoard.BuilderClass.AddField(field);
    }
}