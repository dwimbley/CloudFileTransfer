using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudFileTransfer.Core;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;

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
            throw new NotImplementedException();
        }

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, string localfile)
        {
            throw new NotImplementedException();
        }

        public void Upload(string bucketname, string uploadfolder, string uploadfilename, byte[] file)
        {
            throw new NotImplementedException();
        }
    }
}
