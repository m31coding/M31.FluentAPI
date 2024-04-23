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
        MethodSignature methodSignature = CreateMethodSignature(info.FluentApiClassNameWithTypeParameters, "public");
        Method method = new InterfaceMethod(methodSignature, InterfaceName);

        // student.Semester = semester;
        CreateBody(method, string.Empty);

        // return student;
        method.AppendBodyLine($"return {info.ClassInstanceName};");

        return method;
    }
}