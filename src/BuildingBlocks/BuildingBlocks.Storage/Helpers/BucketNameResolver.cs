using BuildingBlocks.Storage.Enums;

namespace BuildingBlocks.Storage.Helpers
{
    public static class BucketNameResolver
    {
        public static string GetBucketName(FileType fileType)
        {
            return fileType switch
            {
                FileType.ProductImage => "product-images",
                FileType.UserAvatar => "user-avatars",
                FileType.Document => "documents",
                FileType.Video => "videos",
                FileType.Audio => "audio-files",
                _ => "temp"
            };
        }
    }
}
