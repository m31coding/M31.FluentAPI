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
        // public static Student<T1, T2> InSemester(int semester)
        MethodSignature methodSignature =
            CreateMethodSignature(info.FluentApiClassNameWithTypeParameters, "public", "static");
        Method method = new Method(methodSignature);

        // CreateStudent<T1, T2> createStudent = new CreateStudent<T1, T2>();
        method.AppendBodyLine(
            $"{info.BuilderClassNameWithTypeParameters} {info.BuilderInstanceName} = " +
            $"new {info.BuilderClassNameWithTypeParameters}();");

        // createStudent.student.Semester = semester;
        CreateBody(method, $"{info.BuilderInstanceName}.");

        // return createStudent.student;
        method.AppendBodyLine($"return {info.BuilderInstanceName}.{info.ClassInstanceName};");

        return method;
    }
}