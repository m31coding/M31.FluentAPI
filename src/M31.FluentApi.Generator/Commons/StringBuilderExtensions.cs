using System.Text;

namespace M31.FluentApi.Generator.Commons;

internal static class StringBuilderExtensions
{
      internal static StringBuilder Append(this StringBuilder stringBuilder, string? value, bool condition)
      {
          if (condition)
          {
              stringBuilder.Append(value);
          }

          return stringBuilder;
      }
}