using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Http;

namespace Mentoroom.DOMAIN.Interfaces
{
    public interface IFileRepository
    {
        Task<Blob> UploadFile(IFormFile file, string fileName);
        Task<Blob> UploadFile(string path, string fileName);
        Task<List<Blob>> BlobList();
        Task<Blob?> Download(string uri);
        Task Delete(string fileName);
        Task DeleteByUri(string uri);
        Task<Blob> DownloadFolder(string folderName);
    }
}
