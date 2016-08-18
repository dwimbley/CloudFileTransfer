using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CloudFileTransfer.Core;

namespace CloudFileTransfer.Amazon
{
    public class S3 : IFileStore
    {
        private readonly string _accesskey = string.Empty;
        private readonly string _secretkey = string.Empty;
        private readonly RegionEndpoint _endpoint;

        public S3(string accesskey, string secretkey, string endpoint)
        {
            this._accesskey = accesskey;
            this._secretkey = secretkey;
            this._endpoint = Convert.ToRegionEndpoint(endpoint);
        }

        public byte[] Download(string bucketname, string folderpath, string filename)
        {
            using (WebClient client = new WebClient())
            {
                var url = this.GetSignedUrl(folderpath, filename);
                var file = client.DownloadData(url);

                return file;
            }
        }

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

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile)
        {
            var file = File.ReadAllBytes(localfile);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms, this._endpoint);
            }
        }

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms, this._endpoint);
            }
        }

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
