using M31.FluentApi.Generator.CodeBuilding;
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
            // private static extern Student<T1, T2> CreateStudentInstance(T1 property1, T2 property2);
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

            // student = CreateStudentInstance(default!, default!);
            ReservedVariableNames reservedVariableNames = codeBoard.ReservedVariableNames.NewLocalScope();

            CodeBuilder codeBuilder = new CodeBuilder(codeBoard.NewLineString);
            codeBuilder.Append($"{instanceName} = {unsafeAccessorName}");

            (List<string> arguments, List<string> requiredAssignments) =
                GetArguments(constructorInfo.ParameterInfos, reservedVariableNames);
            codeBuilder.Append($"({string.Join(", ", arguments)});");
            requiredAssignments.ForEach(constructor.AppendBodyLine);
            constructor.AppendBodyLine(codeBuilder.ToString());
        }
        else
        {
            // student = new Student<T1, T2>(default!, default!);
            ReservedVariableNames reservedVariableNames = codeBoard.ReservedVariableNames.NewLocalScope();
            CodeBuilder codeBuilder = new CodeBuilder(codeBoard.NewLineString);
            codeBuilder.Append($"{instanceName} = new {classNameWithTypeParameters}");

            (List<string> arguments, List<string> requiredAssignments) =
                GetArguments(constructorInfo.ParameterInfos, reservedVariableNames);
            codeBuilder.Append($"({string.Join(", ", arguments)});");
            requiredAssignments.ForEach(constructor.AppendBodyLine);
            constructor.AppendBodyLine(codeBuilder.ToString());
        }

        codeBoard.Constructor = constructor;
        codeBoard.BuilderClass.AddMethod(constructor);
    }

    private static (List<string> arguments, List<string> requiredAssignments) GetArguments(
        IReadOnlyCollection<ParameterSymbolInfo> parameterInfos,
        ReservedVariableNames reservedVariableNames)
    {
        List<string> requiredAssignments = new List<string>();
        List<string> arguments = new List<string>();

        foreach (ParameterSymbolInfo parameterInfo in parameterInfos)
        {
            string argument = CreateArgument(parameterInfo, reservedVariableNames, out string? requiredAssignment);
            arguments.Add(argument);
            if (requiredAssignment != null)
            {
                requiredAssignments.Add(requiredAssignment);
            }
        }

        return (arguments, requiredAssignments);
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
            requiredAssignment = $"{parameter.TypeForCodeGeneration} {variableName} = default!;";
            return $"ref {variableName}";
        }

        if (parameter.HasAnnotation(ParameterKinds.In))
        {
            string variableName = reservedVariableNames.GetNewLocalVariableName("v");
            requiredAssignment = $"{parameter.TypeForCodeGeneration} {variableName} = default!;";
            return $"in {variableName}";
        }

        return "default!";
    }

    private static Method CreateConstructor(string builderClassName)
    {
        // private CreateStudent()
        MethodSignature signature = MethodSignature.CreateConstructorSignature(builderClassName);
        signature.AddModifiers("private");
        return new Method(signature);
    }
}