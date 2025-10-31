namespace DSIN.DSIN.Business.Domain.Messages;

/// <summary>
/// Representa o JSON bruto recebido do Kafka antes de ser processado pela IA.
/// </summary>
public sealed class RawTicketMessage
{
    /// <summary>
    /// Identificador único da mensagem (geralmente vem do Kafka ou do front-end).
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identificador do agente (quem capturou a multa).
    /// </summary>
    public Guid AgentId { get; set; }

    /// <summary>
    /// Dados brutos da imagem capturada (opcional).
    /// Pode ser Base64, URL ou caminho temporário.
    /// </summary>
    public string? ImageData { get; set; }

    /// <summary>
    /// Transcrição de voz obtida do áudio (texto puro).
    /// </summary>
    public string? AudioTranscription { get; set; }

    /// <summary>
    /// Data e hora da captura.
    /// </summary>
    public DateTimeOffset? CapturedAt { get; set; }

    /// <summary>
    /// Localização GPS ou endereço textual.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Qualquer dado adicional enviado pelo front-end.
    /// Exemplo: tipo de infração reconhecida, confiança do OCR, etc.
    /// </summary>
    public Dictionary<string, object>? Extras { get; set; }
}