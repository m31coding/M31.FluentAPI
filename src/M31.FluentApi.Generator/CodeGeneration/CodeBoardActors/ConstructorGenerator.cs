using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class ConstructorGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        string instanceName = codeBoard.Info.ClassInstanceName;
        string classNameWithTypeParameters = codeBoard.Info.FluentApiClassNameWithTypeParameters;

        if (codeBoard.FluentApiInfos.Any(i => i.SymbolInfo.RequiresReflection))
        {
            Method staticConstructor = CreateStaticConstructor(codeBoard.Info.BuilderClassName);
            codeBoard.StaticConstructor = staticConstructor;
            codeBoard.BuilderClass.AddMethod(staticConstructor);
        }

        Method constructor = CreateConstructor(codeBoard.Info.BuilderClassName);

        if (codeBoard.Info.FluentApiTypeHasPrivateConstructor)
        {
            // student = (Student<T1, T2>) Activator.CreateInstance(typeof(Student<T1, T2>), true)!;
            constructor.AppendBodyLine(
                $"{instanceName} = ({classNameWithTypeParameters}) " +
                $"Activator.CreateInstance(typeof({classNameWithTypeParameters}), true)!;");

            codeBoard.CodeFile.AddUsing("System");
        }
        else
        {
            // student = new Student<T1, T2>();
            constructor.AppendBodyLine($"{instanceName} = new {classNameWithTypeParameters}();");
        }

        codeBoard.Constructor = constructor;
        codeBoard.BuilderClass.AddMethod(constructor);
    }

    private static Method CreateStaticConstructor(string builderClassName)
    {
        // static CreateStudent()
        MethodSignature signature = MethodSignature.CreateConstructorSignature(builderClassName);
        signature.AddModifiers("static");
        return new Method(signature);
    }

    private static Method CreateConstructor(string builderClassName)
    {
        // private CreateStudent()
        MethodSignature signature = MethodSignature.CreateConstructorSignature(builderClassName);
        signature.AddModifiers("private");
        return new Method(signature);
    }
}