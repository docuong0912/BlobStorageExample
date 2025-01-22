using AzureStorageLib.Model;

namespace AzureStorageLib.Interface
{
    public interface IAzureStorage
    {
        Task<string> UploadFileToStorageAsync(FileUploadRequestModel request);
    }
}
