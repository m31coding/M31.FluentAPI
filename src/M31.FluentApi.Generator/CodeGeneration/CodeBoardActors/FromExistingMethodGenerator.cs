using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class FromExistingMethodGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        // public static ICreateStudentFromAnyStep FromExisting(Student student)
        // {
        //     return new CreateStudent(student);
        // }
        BuilderAndTargetInfo info = codeBoard.Info;
        string methodName = "FromExisting";
        MethodSignature methodSignature = MethodSignature.Create(
            info.FromExistingInterfaceName,
            methodName,
            null,
            false);
        methodSignature.AddModifiers(info.DefaultAccessModifier, "static");
        Parameter parameter = new Parameter(
            info.FluentApiClassNameWithTypeParameters,
            info.ClassInstanceName);
        methodSignature.AddParameter(parameter);
        Method method = new Method(methodSignature);
        string parameterListInAngleBrackets = info.GenericInfo?.ParameterListInAngleBrackets ?? string.Empty;
        method.AppendBodyLine(
            $"return new {info.BuilderClassName}{parameterListInAngleBrackets}({info.ClassInstanceName});");
        codeBoard.BuilderClass.AddMethod(method);
    }
}