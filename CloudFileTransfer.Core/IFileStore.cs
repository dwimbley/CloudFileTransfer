using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFileTransfer.Core
{
    public interface IFileStore
    {
        /// <summary>
        /// Downloads file from cloud storage
        /// </summary>
        /// <param name="bucketname">Name of Cloud storage bucket only</param>
        /// <param name="folderpath">Full folder path including bucketname</param>
        /// <param name="filename">File name/extension only</param>
        /// <returns>File bytes</returns>
        byte[] Download(string bucketname, string folderpath, string filename);

        /// <summary>
        /// Upload a file to cloud storage
        /// </summary>
        /// <param name="bucketname">Name of cloud storage bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="localfile">Full path to local file</param>
        void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile);

        /// <summary>
        /// Upload a file to cloud storage
        /// </summary>
        /// <param name="bucketname">Name of cloud storage bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="file">Bytes of the file to upload</param>
        void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file);
    }
}
