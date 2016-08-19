using System.IO;
using System.Threading;
using CloudFileTransfer.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using GoogleObject = Google.Apis.Storage.v1.Data.Object;

namespace CloudFileTransfer.Google
{
    public class GoogleCloudStorage : IFileStore
    {
        private readonly string _apikey;
        private readonly string _secretkey;

        public GoogleCloudStorage(string apikey, string secretkey)
        {
            this._apikey = apikey;
            this._secretkey = secretkey;
        }

        private StorageService CreateStorageClient()
        {
            UserCredential auth = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets {ClientId = this._apikey, ClientSecret = this._secretkey}, new[]
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

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile)
        {
            var file = File.ReadAllBytes(localfile);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms);
            }
        }

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(file, 0, file.Length);
                this.UploadFile(bucketname, uploadfolder, uploadfilename, ms);
            }
        }

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
