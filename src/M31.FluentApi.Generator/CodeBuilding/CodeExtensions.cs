using System.Globalization;

namespace M31.FluentApi.Generator.CodeBuilding;

internal static class CodeExtensions
{
    internal static string ToString(this ICode code, string newLineString)
    {
        CodeBuilder codeBuilder = new CodeBuilder(newLineString);
        code.AppendCode(codeBuilder);
        return codeBuilder.ToString();
    }

    internal static void AddGeneratedAtConstant(this Class @class)
    {
        Property property = new Property("string", "GeneratedAt");
        property.AddRightHandSide($"= \"{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}\";");
        property.AddModifiers("public const");
        @class.AddProperty(property);
    }
}