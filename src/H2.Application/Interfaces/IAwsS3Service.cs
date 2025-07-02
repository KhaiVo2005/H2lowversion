namespace H2.Application.Interfaces
{
    public interface IAwsS3Service
    {
        Task<string> UploadImageAsync(Stream fileStram, string fileName, string contentType, CancellationToken cancellationToken);
    }
}
