using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DSIN.DSIN.Business.Domain.Messages;

namespace DSIN.DSIN.Business.Infra.AI;

public sealed class OpenAITranscriber : ITranscriber
{
    private readonly HttpClient _http;
    private readonly OpenAIOptions _opt;
    private readonly ILogger<OpenAITranscriber> _logger;

    public OpenAITranscriber(IOptions<OpenAIOptions> opt, ILogger<OpenAITranscriber> logger)
    {
        _opt = opt.Value;
        _logger = logger;

        _http = new HttpClient { BaseAddress = new Uri("https://api.openai.com/") };
        var key = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? _opt.ApiKey;
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
    }

    public async Task<string> TranscribeAsync(string rawJson, CancellationToken ct = default)
    {
        var systemPrompt = """
        Você é um serviço que recebe um JSON de talão de multa (com dados brutos ou incompletos)
        e deve retornar um JSON COMPLETO e BEM FORMADO com os seguintes campos:
        Id, AgentId, VehicleId, DriverId, PlateSnapshot, VehicleModelSnapshot, VehicleColorSnapshot,
        DriverNameSnapshot, DriverCpfSnapshot, ViolationCode, ViolationDescription, OccurredAt, Location.

        Regras:
        
        -Retorne APENAS um objeto JSON válido.
        -Não adicione comentários, nem explicações.
        -Preencha campos ausentes com null.
        -Datas no formato ISO 8601.
        -Mantenha nomes de propriedades exatamente como listados acima.
        """;

        var req = new
        {
            model = _opt.Model,
            messages = new object[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = rawJson }
            },
            temperature = 0.1,
            max_tokens = _opt.MaxTokens,
            response_format = new { type = "json_object" }
        };

        var payload = JsonSerializer.Serialize(req);
        var resp = await http.PostAsync(
            "v1/chat/completions",
            new StringContent(payload, Encoding.UTF8, "application/json"),
            ct
        );

        resp.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(await resp.Content.ReadAsStringAsync(ct));
        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        using var  = JsonDocument.Parse(content!);

        _logger.LogInformation("IA retornou JSON transcrito ({Length} bytes)", content!.Length);
        return content!;
    }
}