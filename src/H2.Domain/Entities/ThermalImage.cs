using H2.Domain.Enums;

namespace H2.Domain.Entities
{
    public class ThermalImage
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public ImageType ImageType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
