using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public interface IFileStore
    {
        byte[] Download(string bucketname, string folderpath, string filename);
        void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile);
        void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file);
    }
}
