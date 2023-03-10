using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

// code generation comments are with respect to the unit test ThreeMemberClass.Student
internal class InterjacentBuilderMethod : BuilderStepMethod
{
    internal InterjacentBuilderMethod(BuilderMethod builderMethod, string returnType, string interfaceName)
        : base(builderMethod)
    {
        ReturnType = returnType;
        InterfaceName = interfaceName;
    }

    internal string ReturnType { get; }
    internal string InterfaceName { get; }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info)
    {
        // public IInSemester BornOn(System.DateOnly dateOfBirth)
        MethodSignature methodSignature = CreateMethodSignature(ReturnType, "public");
        Method method = new InterfaceMethod(methodSignature, InterfaceName);

        // student.DateOfBirth = dateOfBirth;
        CreateBody(method, string.Empty);

        // return this;
        method.AppendBodyLine($"return this;");

        return method;
    }
}