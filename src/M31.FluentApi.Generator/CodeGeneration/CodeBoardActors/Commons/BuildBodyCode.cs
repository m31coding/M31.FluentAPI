using M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.Commons;

internal delegate List<string> BuildBodyCode(
    string instancePrefix,
    ReservedVariableNames reservedVariableNames,
    string? returnType);