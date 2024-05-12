using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

// code generation comments are with respect to the unit test ThreeMemberClass.Student
internal class LastStepBuilderMethod : BuilderStepMethod
{
    internal LastStepBuilderMethod(BuilderMethod builderMethod, string interfaceName)
        : base(builderMethod)
    {
        InterfaceName = interfaceName;
    }

    internal string InterfaceName { get; }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info)
    {
        // public Student<T1, T2> InSemester(int semester)
        Method method = CreateInterfaceMethod(InterfaceName, info.FluentApiClassNameWithTypeParameters, "public");

        // student.Semester = semester;
        CreateBody(method, string.Empty);

        // return student;
        CreateReturnStatement(method, $"return {info.ClassInstanceName};");

        return method;
    }
}