using System;

namespace CloudFileTransfer.Core
{
    public enum StorageProvider
    {
        Amazon = 1,
        Google = 2,
        Local = 3
    }

    public static class ConvertSp
    {
        public static StorageProvider ToStorageProvider(string value)
        {
            var sanitized = value.ToLower().Trim();

            switch (sanitized)
            {
                case "destination-google":
                case "destination-googlecloudstorage":
                case "dest-google":
                case "dest-googlecloudstorage":
                    return StorageProvider.Google;
                case "source-google":
                case "source-googlecloudstorage":
                    return StorageProvider.Google;
                case "source-amazon":
                case "source-aws":
                case "source-s3":
                    return StorageProvider.Amazon;
                case "dest-amazon":
                case "dest-aws":
                case "dest-s3":
                case "destination-amazon":
                case "destination-aws":
                case "destination-s3":
                    return StorageProvider.Amazon;
                default:
                    throw new NotImplementedException(String.Format(@"File storage provider {0} is not supported", value));
            }
        }
    }
}