using Microsoft.AspNetCore.Http;

namespace AzureStorageLib.Model
{
    public class FileUploadRequestModel
    {
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
        public int FileSizeLimit { get; set; }
    }
}
