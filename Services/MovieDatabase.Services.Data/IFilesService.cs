namespace MovieDatabase.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface IFilesService
    {
        Task<ImageUploadResult> UploadAsync(Cloudinary cloudinary, IFormFile file);
    }
}
