using BuildingBlocks.Storage.Configs;
using BuildingBlocks.Storage.Interface;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Security.AccessControl;
using MinioConfig = BuildingBlocks.Storage.Configs.MinioConfig;

namespace BuildingBlocks.Storage.Services
{
    public class MinioService : IStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioConfig _minioConfig;

        public MinioService(IOptions<MinioConfig> minioConfig)
        {
            _minioConfig = minioConfig.Value;
            _minioClient = new MinioClient()
                .WithEndpoint(_minioConfig.Endpoint)
                    .WithCredentials(_minioConfig.AccessKey, _minioConfig.SecretKey)
                    .WithSSL(_minioConfig.UseSSL)
                    .Build();
        }

        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                // List buckets
                var buckets = await _minioClient.ListBucketsAsync();
                return true;
            }
            catch (MinioException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UploadFileAsync(string bucketName, string fileName, Stream fileStream, string contentType)
        {
            try
            {
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithStreamData(fileStream)
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(contentType);

                await _minioClient.PutObjectAsync(putObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Stream> GetFileAsync(string bucketName, string fileName)
        {
            try
            {
                MemoryStream fileStream = new MemoryStream();

                await _minioClient.GetObjectAsync(new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithCallbackStream(stream =>
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Seek(0, SeekOrigin.Begin);  // Reset stream position
                    }));

                return fileStream;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteFileAsync(string bucketName, string fileName)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName);

                await _minioClient.RemoveObjectAsync(removeObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> FileExistsAsync(string bucketName, string fileName)
        {
            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName);

                await _minioClient.StatObjectAsync(statObjectArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetFileUrlAsync(string bucketName, string fileName, int expiryInMinutes = 60)
        {
            try
            {
                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileName)
                    .WithExpiry(expiryInMinutes * 60);

                return await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<bool> CreateBucketAsync(string bucketName)
        {
            try
            {
                var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

                await _minioClient.MakeBucketAsync(makeBucketArgs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> BucketExistsAsync(string bucketName)
        {
            try
            {
                var bucketExistsArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

                return await _minioClient.BucketExistsAsync(bucketExistsArgs);
            }
            catch
            {
                return false;
            }
        }
    }
}
