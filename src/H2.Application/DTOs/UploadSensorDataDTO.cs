namespace H2.Application.DTOs
{
    public class UploadSensorDataDTO
    {
        public string DeviceId { get; set; } = null!;
        public double HydrogenPpm { get; set; }
    }
}
