using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Storage.Interface
{
    public interface IStorageService
    {
        Task<bool> UploadFileAsync(string bucketName, string fileName, Stream fileStream, string contentType);
        Task<Stream> GetFileAsync(string bucketName, string fileName);
        Task<bool> DeleteFileAsync(string bucketName, string fileName);
        Task<bool> FileExistsAsync(string bucketName, string fileName);
        Task<string> GetFileUrlAsync(string bucketName, string fileName, int expiryInMinutes = 60);
        Task<bool> CreateBucketAsync(string bucketName);
        Task<bool> BucketExistsAsync(string bucketName);

        Task<bool> CheckConnectionAsync();
    }
}
