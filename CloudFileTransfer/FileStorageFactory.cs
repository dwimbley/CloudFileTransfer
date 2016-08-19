using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudFileTransfer.Amazon;
using CloudFileTransfer.Core;
using CloudFileTransfer.Google;

namespace CloudFileTransfer
{
    /// <summary>
    /// Handles the building of the correct file storage provider based on provider name
    /// </summary>
    public class FileStorageFactory
    {
        private readonly Options _options = null;

        /// <summary>
        /// Instantiates a new class of the <see cref="FileStorageFactory"/> class
        /// </summary>
        /// <param name="options">Command line arguments</param>
        public FileStorageFactory(Options options)
        {
            this._options = options;
        }

        /// <summary>
        /// Get the applicable file storage provider by name
        /// </summary>
        /// <param name="providername">Name of file storage provider</param>
        /// <returns>IFileStore - storage provider</returns>
        public IFileStore Get(string providername)
        {
            var sanitized = providername.ToLower().Trim();

            switch (sanitized)
            {
                case "destination-google":
                case "destination-googlecloudstorage":
                case "dest-google":
                case "dest-googlecloudstorage":
                    return new GoogleCloudStorage(this._options.GoogleDestinationClientId, this._options.GoogleDestinationSecretKey);
                case "source-google":
                case "source-googlecloudstorage":
                    return new GoogleCloudStorage(this._options.GoogleClientIdKey, this._options.GoogleSecretKey);
                case "source-amazon":
                case "source-aws":
                case "source-s3":
                    return new S3(this._options.AwsAccessKey, this._options.AwsSecretKey, this._options.AwsSourceEndpoint);
                case "dest-amazon":
                case "dest-aws":
                case "dest-s3":
                case "destination-amazon":
                case "destination-aws":
                case "destination-s3":
                    return new S3(this._options.AwsDestinationAccessKey, this._options.AwsDestinationSecretKey, this._options.AwsDestinationEndpoint);
                default:
                    throw new NotImplementedException(String.Format(@"File storage provider {0} is not supported", providername));
            }
        }
    }
}
