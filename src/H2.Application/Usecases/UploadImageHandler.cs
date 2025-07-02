using H2.Application.DTOs;
using H2.Application.Interfaces;
using H2.Domain.Enums;

namespace H2.Application.Usecases
{
    public class UploadImageHandler
    {
        private readonly IAwsS3Service _awsS3Service;
        private readonly IThermalImageRepository _thermalImageRepository;
        public UploadImageHandler( IAwsS3Service awsS3Service, IThermalImageRepository thermalImageRepository)
        {
            _awsS3Service = awsS3Service ?? throw new ArgumentNullException(nameof(awsS3Service));
            _thermalImageRepository = thermalImageRepository ?? throw new ArgumentNullException(nameof(thermalImageRepository));
        }
        public async Task<string> HandleAsync(UploadImageDTO dto, CancellationToken cancellationToken)
        {
            var imageUrl = await _awsS3Service.UploadImageAsync(dto.Stream, dto.FileName, dto.ContentType, cancellationToken);
            var image = new Domain.Entities.ThermalImage
            {
                Id = Guid.NewGuid(),
                DeviceId = dto.DeviceId,
                ImageUrl = imageUrl,
                Timestamp = DateTime.UtcNow,
                ImageType = ImageType.Anomaly
            };
            await _thermalImageRepository.AddAsync(image, cancellationToken);
            return imageUrl;
        }
    }
}
