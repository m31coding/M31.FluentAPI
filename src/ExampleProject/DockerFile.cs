// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

// Example from https://mitesh1612.github.io/blog/2021/08/11/how-to-design-fluent-api.

using System.Text;
using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class DockerFile
{
    private readonly StringBuilder content;

    private DockerFile()
    {
        content = new StringBuilder();
    }

    [FluentMethod(0)]
    private void FromImage(string imageName)
    {
        content.AppendLine($"FROM {imageName}");
    }

    [FluentMethod(1)]
    [FluentContinueWith(1)]
    private void CopyFiles(string source, string destination)
    {
        content.AppendLine($"COPY {source} {destination}");
    }

    [FluentMethod(1)]
    [FluentContinueWith(1)]
    private void RunCommand(string command)
    {
        content.AppendLine($"RUN {command}");
    }

    [FluentMethod(1)]
    [FluentContinueWith(1)]
    private void ExposePort(int port)
    {
        content.AppendLine($"EXPOSE {port}");
    }

    [FluentMethod(1)]
    [FluentContinueWith(1)]
    private void WithEnvironmentVariable(string variableName, string variableValue)
    {
        content.AppendLine($"ENV {variableName}={variableValue}");
    }

    [FluentMethod(1)]
    private void WithCommand(string command)
    {
        var commandList = command.Split(" ");
        content.Append("CMD [ ");
        content.Append(string.Join(", ", commandList.Select(c => $"\"{c}\"")));
        content.Append(']');
    }

    public override string ToString()
    {
        return content.ToString();
    }
}