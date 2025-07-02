using Amazon.S3;
using Amazon.S3.Transfer;
using H2.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace H2_Infrastructure.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        public AwsS3Service(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AwsS3:BucketName"] ?? throw new ArgumentNullException("AWS:BucketName is not configured");
        }
        public async Task<string> UploadImageAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = fileStream,
                ContentType = contentType,
            };
            var transferUtility = new Amazon.S3.Transfer.TransferUtility(_s3Client);
            await transferUtility.UploadAsync(uploadRequest, cancellationToken);
            return $"https://{_bucketName}.s3.amazonaws.com/thermal-images/{fileName}";
        }
    }
}
