using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Netflix.Application.Interfaces;

namespace Netflix.Infrastructure.Services;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;

    public PhotoService(IConfiguration config)
    {
        // Leemos las credenciales desde appsettings.json
        var acc = new Account(
            config["CloudinarySettings:CloudName"],
            config["CloudinarySettings:ApiKey"],
            config["CloudinarySettings:ApiSecret"]
        );

        _cloudinary = new Cloudinary(acc);
    }

    public async Task<string> UploadPhotoAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill") // Avatar cuadrado
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

            return uploadResult.SecureUrl.ToString();
        }

        return string.Empty;
    }
}