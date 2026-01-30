using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netflix.Application.DTOs;
using Netflix.Application.Interfaces;
using Netflix.Domain.Entities;
using Netflix.Domain.Interfaces;
using System.Security.Claims;

namespace Netflix.Api.Controllers;

[Authorize] // Solo usuarios con JWT válido pueden entrar
[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly IRepository<Profile> _profileRepo;
    private readonly IPhotoService _photoService;

    public ProfileController(IRepository<Profile> profileRepo, IPhotoService photoService)
    {
        _profileRepo = profileRepo;
        _photoService = photoService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromForm] CreateProfileDto profileDto)
    {
        // 1. Obtener el ID del usuario desde el Token
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized("Usuario no identificado en el token");
        
        int userId = int.Parse(userIdClaim.Value);

        // 2. Subir imagen a Cloudinary
        var imageUrl = await _photoService.UploadPhotoAsync(profileDto.AvatarFile);
        if (string.IsNullOrEmpty(imageUrl)) return BadRequest("Error al subir el avatar");

        // 3. Crear el perfil y guardarlo
        var profile = new Profile
        {
            Name = profileDto.Name,
            Avatar = imageUrl,
            UserId = userId
        };

        await _profileRepo.AddAsync(profile);
        await _profileRepo.SaveAsync();

        return Ok(new { 
            message = "Perfil creado con éxito", 
            profileId = profile.Id,
            avatarUrl = profile.Avatar 
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetMyProfiles()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        int userId = int.Parse(userIdClaim!.Value);

        var allProfiles = await _profileRepo.GetAllAsync();
        var myProfiles = allProfiles.Where(p => p.UserId == userId).ToList();

        return Ok(myProfiles);
    }
}