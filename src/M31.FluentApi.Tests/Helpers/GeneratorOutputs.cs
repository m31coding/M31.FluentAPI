using System.Collections.Generic;
using System.Linq;

namespace M31.FluentApi.Tests.Helpers;

internal class GeneratorOutputs
{
    internal GeneratorOutputs(IReadOnlyCollection<GeneratorOutput> outputs)
    {
        Outputs = outputs;
    }

    internal IReadOnlyCollection<GeneratorOutput> Outputs { get; }
    internal GeneratorOutput? MainOutput => Outputs.Count > 0 ? Outputs.First() : null;
    internal IEnumerable<GeneratorOutput> OtherOutputs => Outputs.Skip(1);
}