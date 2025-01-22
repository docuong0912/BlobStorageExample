using StorageExample.Request;

namespace StorageExample.Services.Interface
{
    public interface IFileService
    {
        public Task<string> UploadAsync(FileRequestModel file);
    }
}
