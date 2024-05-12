using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

// code generation comments are with respect to the unit test ThreeMemberClass.Student
internal class FirstStepBuilderMethod : BuilderStepMethod
{
    internal FirstStepBuilderMethod(BuilderMethod builderMethod, string returnType)
        : base(builderMethod)
    {
        ReturnType = returnType;
    }

    internal string ReturnType { get; }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info)
    {
        // public static IBornOn WithName(string name)
        Method method = CreateMethod(ReturnType, "public", "static");

        // CreateStudent<T1, T2> createStudent = new CreateStudent<T1, T2>();
        method.AppendBodyLine(
            $"{info.BuilderClassNameWithTypeParameters} {info.BuilderInstanceName} = " +
            $"new {info.BuilderClassNameWithTypeParameters}();");

        // createStudent.student.Name = name;
        CreateBody(method, $"{info.BuilderInstanceName}.");

        // return createStudent;
        CreateReturnStatement(method, $"return {info.BuilderInstanceName};");

        return method;
    }
}