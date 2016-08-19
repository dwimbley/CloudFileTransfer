using System.IO;
using System.Threading;
using CloudFileTransfer.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using GoogleObject = Google.Apis.Storage.v1.Data.Object;

namespace CloudFileTransfer.Google
{
    /// <summary>
    /// Implements google API for cloud storage access
    /// </summary>
    public class GoogleCloudStorage : IFileStore
    {
        private readonly string _apikey;
        private readonly string _secretkey;

        /// <summary>
        /// Instantiates a new instance of the <see cref="GoogleCloudStorage"/> class
        /// </summary>
        /// <param name="apikey">Google cloud Client Id key/token</param>
        /// <param name="secretkey">Google cloud Secret Key</param>
        public GoogleCloudStorage(string apikey, string secretkey)
        {
            this._apikey = apikey;
            this._secretkey = secretkey;
        }

        /// <summary>
        /// Create google auth object for accessing google cloud storage
        /// </summary>
        /// <returns>StorageService</returns>
        private StorageService CreateStorageClient()
        {
            UserCredential auth = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = this._apikey, ClientSecret = this._secretkey}, new[]
            {
                StorageService.Scope.DevstorageFullControl

            }, "CloudFileStorage", CancellationToken.None).Result;

            var credentials = GoogleCredential.GetApplicationDefaultAsync().Result;

            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(new[] { StorageService.Scope.DevstorageFullControl });
            }
            
            var serviceInitializer = new BaseClientService.Initializer()
            {
                ApplicationName = "CloudFileTransfer",
                HttpClientInitializer = auth,
            };

            return new StorageService(serviceInitializer);
        }

        /// <summary>
        /// Downloads file from google cloud storage
        /// </summary>
        /// <param name="bucketname">Name of Cloud storage bucket only</param>
        /// <param name="folderpath">Full folder path including bucketname</param>
        /// <param name="filename">File name/extension only</param>
        /// <returns>File bytes</returns>
        public byte[] Download(string bucketname, string folderpath, string filename)
        {
            StorageService storage = CreateStorageClient();

            using (var stream = new MemoryStream())
            {
                storage.Objects.Get(bucketname, filename).Download(stream);
                var content = stream.GetBuffer();

                return content;
            }
        }

        /// <summary>
        /// Upload a file to Google cloud storage
        /// </summary>
        /// <param name="bucketname">Name of Google cloud storage bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="localfile">Full path to local file</param>
        public void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile)
        {
            var file = File.ReadAllBytes(localfile);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms);
            }
        }

        /// <summary>
        /// Upload a file to Google cloud storage
        /// </summary>
        /// <param name="bucketname">Name of Google cloud storage bucket only</param>
        /// <param name="uploadfolder">Full folder path, including bucketname</param>
        /// <param name="uploadfilename">Desired Filename once uploaded, including extension</param>
        /// <param name="file">Bytes of the file to upload</param>
        public void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms);
            }
        }

        /// <summary>
        /// Upload a file to google cloud storage
        /// </summary>
        /// <param name="bucketname">google cloud storage bucket name only</param>
        /// <param name="uploadfolder">Folder, including bucketname, in google cloud storage</param>
        /// <param name="uploadfilename">Desired filename once uploaded in google cloud storage</param>
        /// <param name="file">File memory stream to upload</param>
        private void UploadFile(string bucketname, string uploadfolder, string uploadfilename, MemoryStream file)
        {
            StorageService storage = CreateStorageClient();

            storage.Objects.Insert(
                bucket: bucketname,
                stream: file,
                contentType: Path.GetExtension(uploadfilename).ToContentType(),
                body: new GoogleObject { Name = uploadfilename }
            ).Upload();
        }
    }
}
