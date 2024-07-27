using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Mentoroom.DOMAIN.Interfaces;
using Mentoroom.DOMAIN.Models;
using Microsoft.AspNetCore.Http;
using System.IO.Compression;

namespace Mentoroom.INFRASTRACTURE.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly BlobContainerClient filesContainer;

        public FileRepository(BlobContainerClient filesContainer)
        {
            this.filesContainer = filesContainer;
        }

        public async Task<List<Blob>> BlobList()
        {
            List<Blob> files = new List<Blob>();

            await foreach (var file in filesContainer.GetBlobsAsync())
            {
                string uri = filesContainer.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";
                files.Add(new Blob()
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType,
                });
            }
            return files;
        }

        public async Task Delete(string fileName)
        {
            BlobClient client = filesContainer.GetBlobClient(fileName);
            await client.DeleteAsync();
        }

        public async Task<Blob?> Download(string fileUri)
        {
            var fileName = fileUri.Split(".net/files/")[1];
            BlobClient client = filesContainer.GetBlobClient(fileName);
            if (await client.ExistsAsync())
            {
                var data = await client.OpenReadAsync();
                Stream fileContent = data;
                var content = await client.DownloadContentAsync();
                string name = Path.GetFileName(client.Uri.LocalPath);
                string contentType = content.Value.Details.ContentType;
                return new Blob() { Content = fileContent, Name = name, ContentType = contentType };
            }
            return null;
        }

        public async Task<Blob> UploadFile(IFormFile file, string fileName)
        {
            BlobClient client = filesContainer.GetBlobClient($"{fileName}{System.IO.Path.GetExtension(file.FileName)}");

            await using (Stream? data = file.OpenReadStream())
            {
                await client.UploadAsync(data, true);
            }

            var blob = new Blob()
            {
                Uri = $"{filesContainer.Uri}/{fileName}{System.IO.Path.GetExtension(file.FileName)}",
                Name = fileName,
                ContentType = file.ContentType,
            };
            return blob;
        }

        public async Task<Blob> UploadFile(string path, string fileName)
        {
            BlobClient client = filesContainer.GetBlobClient($"{fileName}{System.IO.Path.GetExtension(path)}");

            var response = await client.UploadAsync(path, true);


            var blob = new Blob()
            {
                Uri = $"{filesContainer.Uri}/{fileName}{System.IO.Path.GetExtension(path)}",
                Name = fileName,
                ContentType = "application/octet-stream",
            };

            return blob;
        }

        public async Task<Blob> DownloadFolder(string folderName)
        {
            var zipStream = new MemoryStream();

            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                await DownloadFolderAsync(filesContainer, folderName, archive);
            }

            zipStream.Seek(0, SeekOrigin.Begin);

            string name = folderName.EndsWith("/") ? folderName[..^1] : folderName;
            name = name.Split("studentfiles/")[1];
            name += ".zip";
            return new Blob() { Content = zipStream, Name = name, ContentType = "application/zip" };
        }

        public async Task DownloadFolderAsync(BlobContainerClient containerClient, string folderName, ZipArchive archive)
        {
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(prefix: folderName))
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                var entryName = blobItem.Name.Substring(folderName.Length);

                if (entryName.StartsWith("studentfiles/"))
                {
                    entryName = entryName.Substring("studentfiles/".Length);
                }
                if (entryName.StartsWith("/"))
                {
                    entryName = entryName.Substring(1);
                }

                var entry = archive.CreateEntry(entryName);

                using (var entryStream = entry.Open())
                {
                    await blobClient.DownloadToAsync(entryStream);
                }
            }
        }

        public async Task DeleteByUri(string uri)
        {
            var blobName = uri.Split(".net/files/")[1];

            BlobClient client = filesContainer.GetBlobClient(blobName);

            await client.DeleteAsync();
        }
    }
}
