using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Application.Service_Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(String publicId);
    }
}
