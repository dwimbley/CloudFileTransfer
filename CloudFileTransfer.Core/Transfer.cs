using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public class Transfer
    {
        private readonly IFileStore _source = null;
        private readonly IFileStore _destination = null;
        public byte[] File { get; private set; }

        public Transfer(IFileStore source, IFileStore destination)
        {
            this._source = source;
            this._destination = destination;
        }

        public void Copy(string sourcebucketname, string destinationbucketname, string folderpath, string destinationfolder, string filename)
        {
            var file = this._source.Download(sourcebucketname, folderpath, filename);
            this.File = file;
            this._destination.Upload(destinationbucketname, destinationfolder, filename, file);
        }
    }
}
