namespace H2.Application.DTOs
{
    public class UploadImageDTO
    {
        public Stream Stream { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = "image/jpg";
        public string DeviceId { get; set; } = null!;
        public string ImageType { get; set; } = "normal";
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
