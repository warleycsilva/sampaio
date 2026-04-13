using System;
using System.Threading.Tasks;
using Sampaio.Domain.ViewModels;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface IStorageService
    {
        Task<string> UploadAsync(byte[] buffer, string fileName, string contentType = null, bool isPublic = true);
        Task<byte[]> DownloadAsync(string fileName, bool isPublic = true);
        Task<FileResultVm> DownloadAsFileResultVmByUrlAsync(string fileName);
        Task<string> GetAuthUrlFileAsync(Uri sasUri);
        Task<bool> RemoveByUrlAsync(string url);
    }
}