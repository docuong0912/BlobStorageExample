using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureStorageLib.Interface;
using AzureStorageLib.Model;
using Microsoft.Extensions.Options;

namespace AzureStorageLib.Service
{
    public class AzureService : IAzureStorage
    {
        public AzureService(IOptions<AppDomainSetup> appSettings)
        {

        }
        public async Task<string> UploadFileToStorageAsync(FileUploadRequestModel request)
        {
            if (request.FormFile.Length > request.FileSizeLimit)
            {
                throw new Exception("File size is too big.");
            }

            // Create a URI to the blob
            var fileExtension = Path.GetExtension(request.FormFile.FileName);
            string url = $"{_azureStorageSettings.BlobUri}/{request.FileName}{fileExtension}";
            Uri blobUri = new(url);

            // Create StorageSharedKeyCredentials object by reading
            StorageSharedKeyCredential storageCredentials = new(_azureStorageSettings.AccountName, _azureStorageSettings.AccountKey);

            // Create the blob client.
            BlobClient blobClient = new(blobUri, storageCredentials);

            // Upload the file
            using Stream fileStream = request.FormFile.OpenReadStream();
            BlobUploadOptions blobUploadOptions = new()
            {
                HttpHeaders = new() { ContentType = request.FormFile.ContentType }
            };

            await blobClient.UploadAsync(fileStream, blobUploadOptions);

            return url;
        }
    }
}
