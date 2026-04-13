using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface IAwsS3StorageService
    {
        Task<string> StoreFile(byte[] buffer, string key, bool isPublic);
        bool StoreFiles(IDictionary<string, byte[]> dictionary);
        
        Task<byte[]> GetFile(string key);
        
        string GetLinkObjectAuth(string path, DateTime expires);
        Task<bool> DeleteFile(string path);
    }
}