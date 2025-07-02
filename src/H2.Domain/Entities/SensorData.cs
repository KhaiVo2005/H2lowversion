using H2.Domain.Enums;

namespace H2.Domain.Entities
{
    public class SensorData
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = null!;
        public double HydrogenPpm { get; set; }
        public AlertLevel AlertLevel { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
