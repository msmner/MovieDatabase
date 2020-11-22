namespace MovieDatabase.Services.Data
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class FilesService : IFilesService
    {
        public async Task<ImageUploadResult> UploadAsync(Cloudinary cloudinary, IFormFile file)
        {
            byte[] destinationImage;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, destinationStream),
                };
                return await cloudinary.UploadAsync(uploadParams);
            }
        }
    }
}
