namespace DSIN.DSIN.Business.Domain.Messages;

public sealed class TranscribedTicketMessage
{
    public Guid Id { get; set; }
    public Guid AgentId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid? DriverId { get; set; }

    public string PlateSnapshot { get; set; } = default!;
    public string VehicleModelSnapshot { get; set; } = default!;
    public string VehicleColorSnapshot { get; set; } = default!;
    public string? DriverNameSnapshot { get; set; }
    public string? DriverCpfSnapshot { get; set; }

    public string ViolationCode { get; set; } = default!;
    public string ViolationDescription { get; set; } = default!;

    public DateTimeOffset OccurredAt { get; set; }
    public string? Location { get; set; }
}