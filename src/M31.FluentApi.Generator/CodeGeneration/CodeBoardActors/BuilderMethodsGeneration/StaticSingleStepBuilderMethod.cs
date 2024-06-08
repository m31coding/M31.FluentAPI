using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

// code generation comments are with respect to the unit test OneMemberClass.Student
internal class StaticSingleStepBuilderMethod : BuilderStepMethod
{
    internal StaticSingleStepBuilderMethod(BuilderMethod builderMethod)
        : base(builderMethod)
    {
    }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info, ReservedVariableNames reservedVariableNames)
    {
        // public static Student<T1, T2> InSemester(int semester)
        Method method = CreateMethod(info.FluentApiClassNameWithTypeParameters, "public", "static");

        // CreateStudent<T1, T2> createStudent = new CreateStudent<T1, T2>();
        string builderInstanceVariableName = reservedVariableNames.GetNewLocalVariableName(info.BuilderInstanceName);
        method.AppendBodyLine(
            $"{info.BuilderClassNameWithTypeParameters} {builderInstanceVariableName} = " +
            $"new {info.BuilderClassNameWithTypeParameters}();");

        // createStudent.student.Semester = semester;
        CreateBody(method, $"{builderInstanceVariableName}.", reservedVariableNames);

        // return createStudent.student;
        CreateReturnStatement(method, $"return {builderInstanceVariableName}.{info.ClassInstanceName};");

        return method;
    }
}