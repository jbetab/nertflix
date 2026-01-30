using Microsoft.AspNetCore.Http;

namespace Netflix.Application.Interfaces;

public interface IPhotoService
{
    Task<string> UploadPhotoAsync(IFormFile file);
}