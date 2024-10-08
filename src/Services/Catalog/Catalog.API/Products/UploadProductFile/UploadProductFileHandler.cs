
using BuildingBlocks.Storage.Enums;
using BuildingBlocks.Storage.Helpers;
using BuildingBlocks.Storage.Interface;

namespace Catalog.API.Products.UploadProductFile
{
    public record UploadProductFileCommand(IFormFile File, FileType FileType) : ICommand<UploadProductFileResult>;

    public record UploadProductFileResult(bool Success, string FileUrl);
    public class UploadProductFileHandlerCommandHandler(IStorageService storageService) : ICommandHandler<UploadProductFileCommand, UploadProductFileResult>
    {
        public async Task<UploadProductFileResult> Handle(UploadProductFileCommand request, CancellationToken cancellationToken)
        {
            var bucketName = BucketNameResolver.GetBucketName(request.FileType);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

            using var stream = request.File.OpenReadStream();
            var success = await storageService.UploadFileAsync(bucketName, fileName, stream, request.File.ContentType);

            if (success)
            {
                var fileUrl = await storageService.GetFileUrlAsync(bucketName, fileName);
                return new UploadProductFileResult(true, fileUrl);
            }

            return new UploadProductFileResult(false, string.Empty);
        }
    }
}
