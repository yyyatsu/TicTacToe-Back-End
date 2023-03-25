using Microsoft.AspNetCore.Http;

namespace TTT.Domain.Services.Interfaces
{
  public interface IImageService
  {
    Task AddImageAsync(IFormFile image, string userName);
    Task<byte[]> GetImageAsync(string userName);
  }
}
