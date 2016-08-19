using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    /// <summary>
    /// Client class to handle transfer of files between same account/storage provider, or between different storage providers
    /// </summary>
    public class Transfer
    {
        private readonly IFileStore _source = null;
        private readonly IFileStore _destination = null;
        public byte[] File { get; private set; }

        /// <summary>
        /// Instantiates a new class of the <see cref="Transfer"/> class
        /// </summary>
        /// <param name="source">Source file storage provider</param>
        /// <param name="destination">Destination file storage provider</param>
        public Transfer(IFileStore source, IFileStore destination)
        {
            this._source = source;
            this._destination = destination;
        }

        /// <summary>
        /// Copies files from one storage provider to another
        /// </summary>
        /// <param name="sourcebucketname">Source bucket name only</param>
        /// <param name="destinationbucketname">Destination bucket name only</param>
        /// <param name="folderpath">Full folder path, including bucketname, of the source file</param>
        /// <param name="destinationfolder">Full folder path, including bucketname, of the destination file</param>
        /// <param name="filename">Name of the file including extension</param>
        public void Copy(string sourcebucketname, string destinationbucketname, string folderpath, string destinationfolder, string filename)
        {
            var file = this._source.Download(sourcebucketname, folderpath, filename);
            this.File = file;
            this._destination.Upload(destinationbucketname, destinationfolder, filename, file);
        }
    }
}
