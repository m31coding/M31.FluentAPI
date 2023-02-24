using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

// code generation comments are with respect to the unit test OneMemberClass.Student
internal class SingleStepBuilderMethod : BuilderStepMethod
{
    internal SingleStepBuilderMethod(BuilderMethod builderMethod)
        : base(builderMethod)
    {
    }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info)
    {
        // public static Student InSemester(int semester)
        MethodSignature methodSignature = CreateMethodSignature(info.FluentApiClassName, "public", "static");
        Method method = new Method(methodSignature);

        // CreateStudent createStudent = new CreateStudent();
        method.AppendBodyLine(
            $"{info.BuilderClassName} {info.BuilderInstanceName} = new {info.BuilderClassName}();");

        // createStudent.student.Semester = semester;
        CreateBody(method, $"{info.BuilderInstanceName}.");

        // return createStudent.student;
        method.AppendBodyLine($"return {info.BuilderInstanceName}.{info.ClassInstanceName};");

        return method;
    }
}