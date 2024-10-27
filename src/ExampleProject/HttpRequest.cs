// Non-nullable member is uninitialized
#pragma warning disable CS8618
// ReSharper disable All

// Example from https://github.com/dotnet/csharplang/discussions/7325.

using System.Net.Http.Json;
using System.Text.Json;
using M31.FluentApi.Attributes;

namespace ExampleProject;

[FluentApi]
public class HttpRequest
{
    [FluentMember(0)]
    public HttpMethod Method { get; private set; }

    [FluentMember(1)]
    public string Url { get; private set; }

    [FluentCollection(2, "Header")]
    public List<(string, string)> Headers { get; private set; }

    [FluentMember(3)]
    public HttpContent Content { get; private set; }

    [FluentMethod(3)]
    public void WithJsonContent<T>(T body, Action<JsonSerializerOptions>? configureSerializer = null)
    {
        JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        configureSerializer?.Invoke(options);
        Content = new StringContent(JsonSerializer.Serialize(body));
    }

    [FluentMethod(3)]
    public void WithoutContent()
    {
    }

    [FluentMethod(4)]
    [FluentReturn]
    public HttpRequestMessage GetMessage()
    {
        HttpRequestMessage request = new HttpRequestMessage(Method, Url);
        request.Content = Content;
        Headers.ForEach(h => request.Headers.Add(h.Item1, h.Item2));
        return request;
    }

    [FluentMethod(4)]
    [FluentReturn]
    public async Task<HttpResponseMessage> SendAsync(HttpClient client)
    {
        HttpRequestMessage request = GetMessage();
        return await client.SendAsync(request);
    }
}