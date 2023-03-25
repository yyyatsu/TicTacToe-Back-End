using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minio;

using TTT.Domain.Models;
using TTT.Domain.Services.Interfaces;

namespace TTT.WebAPI.Controllers
{
  [Authorize]
  [ApiController]
  [Route("Photo")]
  public class PhotoController : ControllerBase
  {
    private readonly IImageService imageService;
    public PhotoController(IImageService imageService)
    {
      this.imageService = imageService;
    }

    [HttpPost("PutImage")]
    public async Task<IActionResult> PutImage(IFormFile image)
    {
      await imageService.AddImageAsync(image, User.Identity.Name);

      return Ok(image);
    }

    [HttpGet("GetImage/{userName}")]
    public async Task<IActionResult> GetImage(string userName)
    {
      var photo = await imageService.GetImageAsync(userName);

      return Ok(photo);
    }

    [HttpGet("Test")]
    public async Task<IActionResult> Test()
    {
      return Ok("test");
    }
  }
}
