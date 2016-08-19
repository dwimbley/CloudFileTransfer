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
    public class FileStorageFactory
    {
        private readonly Options _options = null;

        public FileStorageFactory(Options options)
        {
            this._options = options;
        }

        public IFileStore Get(string providername)
        {
            var sanitized = providername.ToLower().Trim();

            switch (providername)
            {
                case "destination-google":
                case "destination-googlecloudstorage":
                case "dest-google":
                case "dest-googlecloudstorage":
                    return new GoogleCloudStorage("", "");
                case "source-google":
                case "source-googlecloudstorage":
                    return new GoogleCloudStorage("", "");
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
