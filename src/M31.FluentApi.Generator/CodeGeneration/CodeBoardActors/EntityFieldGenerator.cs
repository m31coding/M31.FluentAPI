using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class EntityFieldGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        // private readonly Student<T1, T2> student;
        Field field = new Field(codeBoard.Info.FluentApiClassName, codeBoard.Info.ClassInstanceName);

        if (codeBoard.Info.GenericsInfo != null)
        {
            field.AddGenericTypeParameters(codeBoard.Info.GenericsInfo.Parameters);
        }

        field.AddModifiers("private");
        codeBoard.BuilderClassFields.ReserveFieldName(field.Name);

        if (!codeBoard.Info.FluentApiTypeIsStruct)
        {
            field.AddModifiers("readonly");
        }

        codeBoard.BuilderClass.AddField(field);
    }
}