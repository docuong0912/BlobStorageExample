using AzureExample.Common.Model;
using Microsoft.AspNetCore.Mvc;
using StorageExample.Request;
using StorageExample.Services.Interface;

namespace StorageExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<BaseResponseModel> UploadAsync([FromForm] FileRequestModel request)
        {
            try
            {
                var result = await _fileService.UploadAsync(request);
                return BaseResponseModel.ReturnData(result);
            }
            catch (Exception ex)
            {
                return BaseResponseModel.ReturnError(ex.Message);
            }
        }
    }
}
