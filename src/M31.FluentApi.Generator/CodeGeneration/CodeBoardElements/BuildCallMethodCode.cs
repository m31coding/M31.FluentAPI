using M31.FluentApi.Generator.CodeBuilding;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal delegate List<string> BuildCallMethodCode(
    string instancePrefix,
    IReadOnlyCollection<Parameter> outerMethodParameters,
    ReservedVariableNames reservedVariableNames,
    string? returnType);