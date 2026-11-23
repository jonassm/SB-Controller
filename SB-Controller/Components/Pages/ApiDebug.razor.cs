using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace SB_Controller.Components.Pages;

public partial class ApiDebug
{
    private static readonly JsonSerializerOptions IndentedJson = new() { WriteIndented = true };

    [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;

    private string username = string.Empty;
    private string itemName = string.Empty;
    private string category = string.Empty;
    private int? price;
    private string deleteName = string.Empty;
    private bool isSending;
    private string? requestLine;
    private string? responseStatus;
    private string? responseBody;

    private Task LoginAsync() =>
        SendAsync(HttpMethod.Post, $"api/user/login/{Uri.EscapeDataString(username)}");

    private Task ListItemsAsync() => SendAsync(HttpMethod.Get, "api/item/get");

    private Task AddItemAsync() =>
        SendAsync(HttpMethod.Post, "api/item/add", JsonContent.Create(new { name = itemName, category, price }));

    private Task DeleteItemAsync() =>
        SendAsync(HttpMethod.Delete, $"api/item/delete/{Uri.EscapeDataString(deleteName)}");

    private async Task SendAsync(HttpMethod method, string path, HttpContent? content = null)
    {
        using var client = HttpClientFactory.CreateClient("ApiDebug");
        using var request = new HttpRequestMessage(method, path) { Content = content };
        requestLine = $"{method} {new Uri(client.BaseAddress!, path)}";
        isSending = true;

        try
        {
            using var response = await client.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();
            responseStatus = $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
            responseBody = string.IsNullOrWhiteSpace(body) ? "(empty body)" : FormatBody(body);
        }
        catch (HttpRequestException exception)
        {
            responseStatus = "Request failed";
            responseBody = exception.Message;
        }
        catch (OperationCanceledException)
        {
            responseStatus = "Request timed out";
            responseBody = "The API did not respond in time.";
        }
        finally
        {
            isSending = false;
        }
    }

    private static string FormatBody(string body)
    {
        try
        {
            using var json = JsonDocument.Parse(body);
            return JsonSerializer.Serialize(json.RootElement, IndentedJson);
        }
        catch (JsonException)
        {
            return body;
        }
    }
}
