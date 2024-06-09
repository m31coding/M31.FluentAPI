using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderMethodsGeneration;

// code generation comments are with respect to the unit test ThreeMemberClass.Student
internal class InterjacentBuilderMethod : InterfaceBuilderMethod
{
    internal InterjacentBuilderMethod(
        BuilderMethod builderMethod,
        string returnType,
        string interfaceName,
        BaseInterface? baseInterface)
        : base(builderMethod, interfaceName, baseInterface)
    {
        ReturnType = returnType;
    }

    internal string ReturnType { get; }

    internal override Method BuildMethodCode(BuilderAndTargetInfo info, ReservedVariableNames reservedVariableNames)
    {
        // public IInSemester BornOn(System.DateOnly dateOfBirth)
        Method method = CreateInterfaceMethod(InterfaceName, ReturnType, "public");

        // student.DateOfBirth = dateOfBirth;
        CreateBody(method, string.Empty, reservedVariableNames);

        // return this;
        CreateReturnStatement(method, "return this;");

        return method;
    }
}