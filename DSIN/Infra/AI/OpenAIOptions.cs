namespace DSIN.DSIN.Business.Infra.AI;

public sealed class OpenAIOptions
{
    public string ApiKey { get; set; } = default!;
    public string Model { get; set; } = "gpt-4o-mini";
    public int MaxTokens { get; set; } = 1200;
}