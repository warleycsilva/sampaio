using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Sampaio.Shared.Extensions;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Domain.ViewModels;
using Sampaio.Shared.Config;

namespace Sampaio.Infra
{
    public class StorageService : IStorageService
    {
        private readonly AzureBlobConfig _config;

        public StorageService(AzureBlobConfig config)
        {
            _config = config;
        }

        public async Task<string> UploadAsync(byte[] buffer, string fileName,  string contentType = null, bool isPublic = true)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return null;
            
            var client = account.CreateCloudBlobClient();
            
            var container = client.GetContainerReference(isPublic ? _config.Container : _config.PrivateContainer);
            
            if(!container.Exists())return null;
            
            var blob = container.GetBlockBlobReference(fileName);

            using (var ms = new MemoryStream(buffer))
            {
                blob.Properties.ContentType = contentType ?? fileName.HandleContentType();

                await blob.UploadFromStreamAsync(new MemoryStream(buffer));
                return blob.Uri.ToString();
            }
        }
        
        public async Task<byte[]> DownloadAsync(string fileName, bool isPublic = true)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return null;
            
            var client = account.CreateCloudBlobClient();
            
            var container = client.GetContainerReference(isPublic ? _config.Container : _config.PrivateContainer);
            
            if(!container.Exists())return null;
            
            var blob = container.GetBlockBlobReference(fileName);
           
            using (var ms = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(ms);
                return ms.ToArray();
            }
        }
        
        public async Task<FileResultVm> DownloadAsFileResultVmByUrlAsync(string url)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return null;

            var blob = new CloudBlockBlob(new Uri(url), account.Credentials);
           
            using (var ms = new MemoryStream())
            {
                await blob.DownloadToStreamAsync(ms);
                return new FileResultVm
                {
                    Buffer = ms.ToArray(),
                    ContentType = blob.Properties.ContentType,
                    FileName = string.Empty
                };
            }
        }

        public async Task<string> GetAuthUrlFileAsync(Uri sasUri)
        {
            var accountName = CloudStorageAccount.Parse(_config.ConnectionString);
            var cloudBlockBlob = new CloudBlockBlob(sasUri,credentials: new StorageCredentials(accountName.Credentials.AccountName, _config.Key));
            var sharedAccessBlobPolicy = new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTimeOffset.Now.AddHours(24),
                Permissions = SharedAccessBlobPermissions.Read
            };

            var sharedAcessSignature = cloudBlockBlob.GetSharedAccessSignature(sharedAccessBlobPolicy);
            return cloudBlockBlob.Uri + sharedAcessSignature;
        }

        public async Task<bool> RemoveByUrlAsync(string url)
        {
            if (!CloudStorageAccount.TryParse(_config.ConnectionString, out var account)) return false;

            var blob = new CloudBlockBlob(new Uri(url), account.Credentials);

            await blob.DeleteAsync(CancellationToken.None);

            return true;
        }
    }
}