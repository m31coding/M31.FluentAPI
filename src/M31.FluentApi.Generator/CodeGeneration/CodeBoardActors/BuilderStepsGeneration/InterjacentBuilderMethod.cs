using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

// code generation comments are with respect to the unit test ThreeMemberClass.Student
internal class InterjacentBuilderMethod : BuilderStepMethod
{
    internal InterjacentBuilderMethod(
        BuilderMethod builderMethod,
        string returnType,
        string interfaceName,
        BaseInterface? baseInterface)
        : base(builderMethod)
    {
        ReturnType = returnType;
        InterfaceName = interfaceName;
        BaseInterface = baseInterface;
    }

    internal string ReturnType { get; }
    internal string InterfaceName { get; }
    internal BaseInterface? BaseInterface { get; }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info, ReservedVariableNames reservedVariableNames)
    {
        // public IInSemester BornOn(System.DateOnly dateOfBirth)
        InterfaceMethod method = CreateInterfaceMethod(InterfaceName, BaseInterface, ReturnType, "public");

        // student.DateOfBirth = dateOfBirth;
        CreateBody(method, string.Empty, reservedVariableNames);

        // return this;
        CreateReturnStatement(method, "return this;");

        return method;
    }
}