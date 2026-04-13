using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Sampaio.Domain.Contracts.Infra;
using Sampaio.Shared.Config;

namespace Sampaio.Infra
{
    public class AwsS3StorageService : IAwsS3StorageService
    {
        private readonly IAmazonS3 _client;
        private readonly AwsS3Config _s3Config;

        public AwsS3StorageService(AwsS3Config s3Config)
        {
            _s3Config = s3Config;
            _client = new AmazonS3Client(_s3Config.AccessKeyId, _s3Config.SecretAccessKey, RegionEndpoint.GetBySystemName(_s3Config.Region));
        }

        private async Task<bool> StoreS3File(byte[] buffer,
            string key,
            bool isPublic)
        {
            using (var ms = new MemoryStream(buffer))
            {
                var request = new PutObjectRequest()
                {
                    BucketName = _s3Config.BucketName,
                    Key = key,
                    InputStream = ms,
                    AutoCloseStream = true,
                    CannedACL = isPublic ? S3CannedACL.PublicRead : S3CannedACL.Private,
                    StorageClass = S3StorageClass.Standard
                };
                
                var response = await _client.PutObjectAsync(request);
                return response.HttpStatusCode == HttpStatusCode.OK;
            }
        }

        private async Task<byte[]> GetS3File(string key)
        {
            var fileTransferUtility = new TransferUtility(_client);

            using (var ms = new MemoryStream())
            {
                var stream = await fileTransferUtility.OpenStreamAsync(_s3Config.BucketName, key);
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private async Task<bool> DeleteS3File(string key)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _s3Config.BucketName,
                Key = key
            };

            var response = await _client.DeleteObjectAsync(deleteRequest);

            return response.HttpStatusCode == HttpStatusCode.NoContent;
        }

        private async Task<bool> SetObjectToPublic(string key)
        {
            using (var s3Client = new AmazonS3Client(_s3Config.AccessKeyId,
                _s3Config.SecretAccessKey,
                RegionEndpoint.GetBySystemName(_s3Config.Region)))
            {
                var acl = new PutACLRequest
                {
                    CannedACL = S3CannedACL.PublicRead,
                    Key = key,
                    BucketName = _s3Config.BucketName,
                };

                try
                {
                    var response = await s3Client.PutACLAsync(acl);
                    return response.HttpStatusCode == HttpStatusCode.OK;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<string> StoreFile(byte[] buffer,
            string key,
            bool isPublic)
        {
            var success = await StoreS3File(buffer, key, isPublic);

            return success ? $"{_s3Config.BucketBaseUrl}/{key}" : string.Empty;
        }

        public bool StoreFiles(IDictionary<string, byte[]> dictionary)
        {
            var success = true;

            var fileTransferUtility = new TransferUtility(_client);

            Parallel.For(0, dictionary.Count, (i,
                loopState) =>
            {
                try
                {
                    var item = dictionary.ElementAt(i);

                    using (var ms = new MemoryStream(item.Value))
                    {
                        fileTransferUtility.Upload(ms, _s3Config.BucketName, item.Key);
                        ms.Close();
                    }
                }
                catch
                {
                    success = false;
                    loopState.Stop();
                }
            });

            return success;
        }

        public async Task<byte[]> GetFile(string key)
        {
            return await GetS3File(key);
        }

        public string GetLinkObjectAuth(string path,
            DateTime expires)
        {
            using (var s3Client = new AmazonS3Client(_s3Config.AccessKeyId,
                _s3Config.SecretAccessKey, RegionEndpoint.GetBySystemName(_s3Config.Region)))
            {
                var g = new GetPreSignedUrlRequest
                {
                    BucketName = _s3Config.BucketName,
                    Protocol = Protocol.HTTPS,
                    Expires = expires,
                    Key = path,
                    Verb = HttpVerb.GET
                };

                var url = s3Client.GetPreSignedURL(g);

                return url;
            }
        }

        public async Task<bool> DeleteFile(string path) => await DeleteS3File(path);
    }
}