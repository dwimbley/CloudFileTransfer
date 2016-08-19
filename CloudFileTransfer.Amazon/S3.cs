using System;
using System.IO;
using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CloudFileTransfer.Core;

namespace CloudFileTransfer.Amazon
{
    /// <summary>
    /// Implements amazon api for file storage access
    /// </summary>
    public class S3 : IFileStore
    {
        private readonly string _accesskey = string.Empty;
        private readonly string _secretkey = string.Empty;
        private readonly RegionEndpoint _endpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="S3"/> class.
        /// </summary>
        /// <param name="accesskey">AWS Access key</param>
        /// <param name="secretkey">AWS Secret key</param>
        /// <param name="endpoint">AWS Region endpoint to target</param>
        public S3(string accesskey, string secretkey, string endpoint)
        {
            this._accesskey = accesskey;
            this._secretkey = secretkey;
            this._endpoint = Convert.ToRegionEndpoint(endpoint);
        }

        /// <summary>
        /// Downloads file from S3 on AWS
        /// </summary>
        /// <param name="bucketname">Name of S3 bucket only</param>
        /// <param name="folderpath">Full folder path including bucketname</param>
        /// <param name="filename">File name/extension only</param>
        /// <returns></returns>
        public byte[] Download(string bucketname, string folderpath, string filename)
        {
            using (WebClient client = new WebClient())
            {
                var url = this.GetSignedUrl(folderpath, filename);
                var file = client.DownloadData(url);

                return file;
            }
        }

        /// <summary>
        /// Get presigned url of a private file in s3
        /// </summary>
        /// <param name="folderpath">Full folder path, including bucket name, in s3</param>
        /// <param name="filename">Filename plus extension</param>
        /// <returns>URL to access file</returns>
        private string GetSignedUrl(string folderpath, string filename)
        {
            using (IAmazonS3 client = new AmazonS3Client(this._accesskey, this._secretkey, this._endpoint))
            {
                GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
                request.BucketName = folderpath;
                request.Key = filename;
                request.Protocol = Protocol.HTTP;
                request.Expires = DateTime.Now.AddMinutes(200);

                return client.GetPreSignedURL(request);
            }
        }

        /// <summary>
        /// Upload a file to AWS S3
        /// </summary>
        /// <param name="bucketname">Name of S3 bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="localfile">Full path to local file</param>
        public void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile)
        {
            var file = File.ReadAllBytes(localfile);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms, this._endpoint);
            }
        }

        /// <summary>
        /// Upload a file to AWS S3
        /// </summary>
        /// <param name="bucketname">Name of S3 bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="file">Bytes of the file to upload</param>
        public void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms, this._endpoint);
            }
        }

        /// <summary>
        /// Upload a file to S3
        /// </summary>
        /// <param name="bucketname">AWS S3 bucket name only</param>
        /// <param name="uploadfolder">Folder, including bucketname, in s3</param>
        /// <param name="uploadfilename">Desired filename once uploaded in S3</param>
        /// <param name="file">File memory stream to upload</param>
        /// <param name="endpoint">Desired region where file will be uploaded to in S3</param>
        private void UploadFile(string bucketname, string uploadfolder, string uploadfilename, MemoryStream file, RegionEndpoint endpoint)
        {
            using (IAmazonS3 client = new AmazonS3Client(this._accesskey, this._secretkey, endpoint))
            {
                MetadataCollection meta = new MetadataCollection();
                meta["title"] = uploadfilename;
                PutObjectRequest request = new PutObjectRequest()
                {
                    InputStream = file,
                    BucketName = bucketname,
                    Key = string.Format(@"{0}/{1}", uploadfolder, uploadfilename),
                    CannedACL = S3CannedACL.Private
                };

                client.PutObject(request);
            }
        }
    }
}
