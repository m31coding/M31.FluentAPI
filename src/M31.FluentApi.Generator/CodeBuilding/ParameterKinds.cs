namespace M31.FluentApi.Generator.CodeBuilding;

[Flags]
internal enum ParameterKinds
{
    None = 0,
    Params = 1 << 0,
    Ref = 1 << 1,
    In = 1 << 2,
    Out = 1 << 3,
}