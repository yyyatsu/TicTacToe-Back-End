using Microsoft.AspNetCore.Http;
using Minio;
using TTT.Data.Entities;
using TTT.Domain.Services.Interfaces;

namespace TTT.Domain.Services.Implementations
{
  public class ImageService : IImageService
  {
    private readonly MinioClient minioClient;

	public ImageService(MinioClient minioClient)
	{
	  this.minioClient = minioClient;
	}

	public async Task AddImageAsync(IFormFile image, string userName)
	{
      var stream = new MemoryStream();
      await image.CopyToAsync(stream);

      var putObjectArgs = new PutObjectArgs()
        .WithBucket("ttt")
        .WithObject(userName)
        .WithObjectSize(image.Length)
        .WithContentType(image.ContentType)
        .WithStreamData(image.OpenReadStream());

      await minioClient.PutObjectAsync(putObjectArgs);
    }

    public async Task<byte[]> GetImageAsync(string userName)
    {
      var photo = new MemoryStream();
      GetObjectArgs getObjectArgs = new GetObjectArgs()
        .WithBucket("ttt")
        .WithObject(userName)
        .WithCallbackStream((stream) =>
        {
          stream.CopyTo(photo);
        });
      await minioClient.GetObjectAsync(getObjectArgs);
      return photo.ToArray();
    }
  }
}
