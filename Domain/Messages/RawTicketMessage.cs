namespace DSIN.DSIN.Business.Domain.Messages;

public sealed class RawTicketMessage
{
    public Guid Id { get; set; }
    public Guid AgentId { get; set; }
    public string? ImageData { get; set; }
    public string? AudioTranscription { get; set; }
    public DateTimeOffset? CapturedAt { get; set; }
    public string? Location { get; set; }
    public Dictionary<string, object>? Extras { get; set; }
}
