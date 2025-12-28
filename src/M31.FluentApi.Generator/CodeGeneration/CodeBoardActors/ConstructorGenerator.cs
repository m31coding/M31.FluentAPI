using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;
using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;
using M31.FluentApi.Generator.SourceGenerators;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors;

internal class ConstructorGenerator : ICodeBoardActor
{
    public void Modify(CodeBoard codeBoard)
    {
        string instanceName = codeBoard.Info.ClassInstanceName;
        string classNameWithTypeParameters = codeBoard.Info.FluentApiClassNameWithTypeParameters;

        Method constructor = CreateConstructor(codeBoard.Info.BuilderClassName);
        ConstructorInfo constructorInfo = codeBoard.Info.FluentApiTypeConstructorInfo;

        if (codeBoard.Info.FluentApiTypeConstructorInfo.ConstructorIsNonPublic)
        {
            string unsafeAccessorName = $"Create{codeBoard.Info.FluentApiClassName}Instance";
            MethodSignature unsafeAccessorSignature = MethodSignature.Create(
                classNameWithTypeParameters,
                unsafeAccessorName,
                null,
                true);

            List<Parameter> parameters = constructorInfo.ParameterInfos // todo: extract x3
                .Select(i => new Parameter(
                    i.TypeForCodeGeneration,
                    i.ParameterName,
                    i.DefaultValue,
                    i.GenericTypeParameterPosition,
                    new ParameterAnnotations(i.ParameterKinds)))
                .ToList();

            parameters.ForEach(unsafeAccessorSignature.AddParameter);

            unsafeAccessorSignature.AddModifiers("private", "static", "extern");
            unsafeAccessorSignature.AddAttribute("[UnsafeAccessor(UnsafeAccessorKind.Constructor)]");
            codeBoard.BuilderClass.AddMethodSignature(unsafeAccessorSignature);
            codeBoard.CodeFile.AddUsing("System.Runtime.CompilerServices");

            ReservedVariableNames reservedVariableNames = codeBoard.ReservedVariableNames.NewLocalScope();
            List<string> requiredAssignments = new List<string>();
            List<string> arguments = new List<string>();
            CodeBuilder codeBuilder = new CodeBuilder(codeBoard.NewLineString);
            codeBuilder
                .Append($"{instanceName} = {unsafeAccessorName}");

            foreach (ParameterSymbolInfo parameterInfo in constructorInfo.ParameterInfos)
            {
                string argument = CreateArgument(parameterInfo, reservedVariableNames, out string? requiredAssignment);
                arguments.Add(argument);
                if (requiredAssignment != null)
                {
                    requiredAssignments.Add(requiredAssignment);
                }
            }

            codeBuilder.Append($"({string.Join(", ", arguments)});");
            requiredAssignments.ForEach(constructor.AppendBodyLine);
            constructor.AppendBodyLine(codeBuilder.ToString());
            // todo: test constructor in generic class.
        }
        else
        {
            // student = new Student<T1, T2>(default!, default!);
            string parameters = string.Join(", ",
                Enumerable.Repeat("default!", constructorInfo.NumberOfParameters));
            constructor.AppendBodyLine($"{instanceName} = new {classNameWithTypeParameters}({parameters});");
        }

        codeBoard.Constructor = constructor;
        codeBoard.BuilderClass.AddMethod(constructor);
    }

    private static string CreateArgument(
        ParameterSymbolInfo parameter,
        ReservedVariableNames reservedVariableNames,
        out string? requiredAssignment)
    {
        requiredAssignment = null;

        if (parameter.HasAnnotation(ParameterKinds.Out))
        {
            return "out _";
        }

        if (parameter.HasAnnotation(ParameterKinds.Ref))
        {
            string variableName = reservedVariableNames.GetNewLocalVariableName("v");
            requiredAssignment = $"var {variableName} = default!;";
            return $"ref {variableName}";
        }

        if(parameter.HasAnnotation(ParameterKinds.In))
        {
            string variableName = reservedVariableNames.GetNewLocalVariableName("v");
            requiredAssignment = $"var {variableName} = default!;";
            return $"in {variableName}";
        }

        return "default!";
        // todo: test constructor with ref, in, out parameter
    }

    private static Method CreateConstructor(string builderClassName)
    {
        // private CreateStudent()
        MethodSignature signature = MethodSignature.CreateConstructorSignature(builderClassName);
        signature.AddModifiers("private");
        return new Method(signature);
    }
}