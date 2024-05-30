using System.IO;
using System.Linq;
using M31.FluentApi.Generator.CodeBuilding;
using Xunit;

namespace M31.FluentApi.Tests.Components.CodeBuilding;

public class CodeBuildingTests
{
    [Fact]
    public void CanBuildRobotsFile()
    {
        CodeFile codeFile = CreateRobotsCodeFile();
        string code = codeFile.ToString();
        string basePath = Path.Join("..", "..", "..", "Components", "CodeBuilding");
        File.WriteAllText(Path.Join(basePath, "Robots.g.cs"), code);
        string expectedCode = File.ReadAllText(Path.Join(basePath, "Robots.expected.txt"));
        Assert.Equal(expectedCode, code);
    }

    private static CodeFile CreateRobotsCodeFile()
    {
        Interface moveInterface = CreateIMoveInterface();
        Class robot = CreateRobotClass(moveInterface);
        CodeFile codeFile = new CodeFile("M31.FluentApi.Tests.Components", "\n");
        codeFile.AddUsing("System.Collections.Generic");
        codeFile.AddDefinition(robot);
        codeFile.AddDefinition(moveInterface);
        return codeFile;

        static Interface CreateIMoveInterface()
        {
            Interface @interface = new Interface("public", "IMove");
            MethodSignature moveSignature = MethodSignature.Create("void", "Move", null, true);
            moveSignature.AddParameter("double", "deltaX");
            moveSignature.AddParameter("double", "deltaY");
            @interface.AddMethodSignature(moveSignature);
            return @interface;
        }

        static Class CreateRobotClass(Interface moveInterface)
        {
            Class robot = new Class("Robot");
            robot.AddModifiers("public");
            robot.AddInterface(moveInterface.Name);
            Method constructor = CreateConstructor();
            robot.AddMethod(constructor);
            Method moveMethod = CreateMoveMethod(moveInterface.MethodSignatures.First());
            robot.AddMethod(moveMethod);
            Method assignTaskMethod = CreateAssignTaskMethod();
            robot.AddMethod(assignTaskMethod);
            AddFieldsAndProperties(robot);
            return robot;
        }

        static void AddFieldsAndProperties(Class robot)
        {
            Field tasksField = new Field("List<string>", "tasks");
            tasksField.AddModifiers("private", "readonly");
            robot.AddField(tasksField);

            Property name = new Property("string", "Name");
            name.AddModifiers("public");
            name.WithGetAccessor();
            robot.AddProperty(name);

            Property yearOfManufacture = new Property("int", "YearOfManufacture");
            yearOfManufacture.AddModifiers("public");
            yearOfManufacture.WithGetAccessor();
            robot.AddProperty(yearOfManufacture);

            Property posX = new Property("double", "PosX");
            posX.AddModifiers("public");
            posX.WithGetAccessor();
            posX.WithSetAccessor("private");
            robot.AddProperty(posX);

            Property posY = new Property("double", "PosY");
            posY.AddModifiers("public");
            posY.WithGetAccessor();
            posY.WithSetAccessor("private");
            robot.AddProperty(posY);

            Property tasks = new Property("IReadOnlyCollection<string>", "Tasks");
            tasks.AddModifiers("public");
            tasks.AddRightHandSide("=> tasks;");
            robot.AddProperty(tasks);
        }

        static Method CreateMoveMethod(MethodSignature interfaceMethodSignature)
        {
            MethodSignature signature = interfaceMethodSignature.ToSignatureForMethodBody();
            signature.AddModifiers("public");
            Method method = new Method(signature);
            method.AppendBodyLine("PosX += deltaX;");
            method.AppendBodyLine("PosY += deltaY;");
            return method;
        }

        static Method CreateAssignTaskMethod()
        {
            MethodSignature signature = MethodSignature.Create("void", "AssignTask", null, false);
            signature.AddModifiers("public");
            signature.AddParameter("string", "task");
            Method method = new Method(signature);
            method.AppendBodyLine("tasks.Add(task);");
            return method;
        }

        static Method CreateConstructor()
        {
            MethodSignature signature = MethodSignature.CreateConstructorSignature("Robot");
            signature.AddModifiers("public");
            signature.AddParameter("string", "name");
            signature.AddParameter("int", "yearOfManufacture");
            signature.AddParameter(new Parameter("double", "posX", "0"));
            signature.AddParameter(new Parameter("double", "posY", "0"));
            Method constructor = new Method(signature);
            constructor.AppendBodyLine("Name = name;");
            constructor.AppendBodyLine("YearOfManufacture = yearOfManufacture;");
            constructor.AppendBodyLine("tasks = new List<string>();");
            constructor.AppendBodyLine("PosX = posX;");
            constructor.AppendBodyLine("PosY = posY;");
            return constructor;
        }
    }
}