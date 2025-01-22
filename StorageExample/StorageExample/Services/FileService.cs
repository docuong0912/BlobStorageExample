using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using StorageExample.Request;
using StorageExample.Services.Interface;

namespace StorageExample.Services
{
    public class FileService : IFileService
    {
        private readonly BlobClient _blobClient;
        private readonly IConfiguration _configuration;
        public FileService(BlobClient blobClient, IConfiguration configuration)
        {
            _blobClient = blobClient;
            _configuration = configuration;
        }
        public async Task<string> UploadAsync(FileRequestModel request)
        {
            var fileName = request.File.FileName;
            var idx = DateTime.Now.ToString("hhmmssddMMyyyy");
            var extent = fileName.Substring(fileName.LastIndexOf('.'), fileName.Length - fileName.LastIndexOf('.')).ToLower();
            var filteredFileName = fileName.Replace(extent, "") + idx;

            var requestModel = new FileUploadRequestModel()
            {
                FormFile = request.File,
                FileName = filteredFileName,
                FileSizeLimit = 20971520 //20MB,
            };
            var fileExtension = Path.GetExtension(requestModel.FormFile.FileName);
            string url = $"{_configuration.GetValue<string>("AzureStorageSettings:DefaultEndpointsProtocol")}://{_configuration.GetValue<string>("AzureStorageSettings:AccountName")}.blob.{_configuration.GetValue<string>("AzureStorageSettings:EndpointSuffix")}/{_configuration.GetValue<string>("AzureStorageSettings:ImageContainer")}/{requestModel.FileName}{fileExtension}";
            Uri blobUri = new(url);

            // Create StorageSharedKeyCredentials object by reading
            StorageSharedKeyCredential storageCredentials = new(_configuration.GetValue<string>("AzureStorageSettings:AccountName"), _configuration.GetValue<string>("AzureStorageSettings:AccountKey"));

            // Create the blob client.
            BlobClient blobClient = new(blobUri, storageCredentials);

            using Stream fileStream = requestModel.FormFile.OpenReadStream();
            BlobUploadOptions blobUploadOptions = new()
            {
                HttpHeaders = new() { ContentType = request.File.ContentType }
            };
            await blobClient.UploadAsync(fileStream, blobUploadOptions);
            return url;
        }
    }
}
