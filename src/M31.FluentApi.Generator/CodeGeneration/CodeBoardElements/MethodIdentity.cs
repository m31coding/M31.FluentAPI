using M31.FluentApi.Generator.CodeBuilding;
using M31.FluentApi.Generator.Commons;
using M31.FluentApi.Generator.SourceGenerators.Generics;

namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardElements;

internal class MethodIdentity
{
    private MethodIdentity(
        string methodName,
        int numberOfTypeParameters,
        IReadOnlyCollection<string> convertedParameterTypes)
    {
        MethodName = methodName;
        NumberOfTypeParameters = numberOfTypeParameters;
        ConvertedParameterTypes = convertedParameterTypes;
    }

    internal string MethodName { get; }
    internal int NumberOfTypeParameters { get; }
    internal IReadOnlyCollection<string> ConvertedParameterTypes { get; }

    internal static MethodIdentity Create(string methodName)
    {
        return new MethodIdentity(methodName, 0, Array.Empty<string>());
    }

    internal static MethodIdentity Create(MethodSymbolInfo methodSymbolInfo)
    {
        IReadOnlyCollection<ParameterType> parameters = methodSymbolInfo.ParameterInfos
            .Select(i => new ParameterType(i.TypeForCodeGeneration, i.ParameterKinds)).ToArray();
        return Create(methodSymbolInfo.Name, methodSymbolInfo.GenericInfo, parameters);
    }

    internal static MethodIdentity Create(
        string methodName,
        GenericInfo? genericInfo,
        IReadOnlyCollection<Parameter> parameters)
    {
        IReadOnlyCollection<ParameterType> parameterTypes = parameters
            .Select(p => new ParameterType(p.Type, p.ParameterAnnotations?.ParameterKinds ?? ParameterKinds.None))
            .ToArray();
        return Create(methodName, genericInfo, parameterTypes);
    }

    private static MethodIdentity Create(
        string methodName,
        GenericInfo? genericInfo,
        IReadOnlyCollection<ParameterType> parameterTypes)
    {
        Dictionary<string, int> typeParameterNameToTypeParameterPosition =
            genericInfo == null
                ? new Dictionary<string, int>()
                : genericInfo.Parameters.Select((parameter, position) => (parameter, position))
                    .ToDictionary(kvp => kvp.parameter.ParameterName, kvp => kvp.position);

        int numberOfTypeParameters = genericInfo == null ? 0 : genericInfo.Parameters.Count;
        string[] convertedParameterTypes = parameterTypes
            .Select(i => ToConvertedParameterType(i, GetGenericTypeParameterPosition(i.Type)))
            .ToArray();
        return new MethodIdentity(methodName, numberOfTypeParameters, convertedParameterTypes);

        int? GetGenericTypeParameterPosition(string parameterType)
        {
            return typeParameterNameToTypeParameterPosition.TryGetValue(
                parameterType,
                out int typeParameterPosition)
                ? typeParameterPosition
                : null;
        }
    }

    private static string ToConvertedParameterType(
        ParameterType parameterType,
        int? genericTypeParameterPosition)
    {
        bool modified = parameterType.ParameterKinds.HasFlag(ParameterKinds.In) ||
                        parameterType.ParameterKinds.HasFlag(ParameterKinds.Out) ||
                        parameterType.ParameterKinds.HasFlag(ParameterKinds.Ref);

        string typeString = genericTypeParameterPosition.HasValue
            ? $"g{genericTypeParameterPosition.Value}"
            : parameterType.Type;

        return modified ? $"mod-{typeString}" : typeString;
    }

    protected bool Equals(MethodIdentity other)
    {
        return MethodName == other.MethodName &&
               NumberOfTypeParameters == other.NumberOfTypeParameters &&
               ConvertedParameterTypes.SequenceEqual(other.ConvertedParameterTypes);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MethodIdentity)obj);
    }

    public override int GetHashCode()
    {
        return new HashCode()
            .Add(MethodName, NumberOfTypeParameters)
            .AddSequence(ConvertedParameterTypes);
    }

    public override string ToString()
    {
        return $"{MethodName}<{NumberOfTypeParameters}>({string.Join(", ", ConvertedParameterTypes)})";
    }

    private class ParameterType
    {
        public ParameterType(string type, ParameterKinds parameterKinds)
        {
            Type = type;
            ParameterKinds = parameterKinds;
        }

        internal string Type { get; }
        internal ParameterKinds ParameterKinds { get; }
    }
}