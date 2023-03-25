using Microsoft.AspNetCore.Mvc;
using TTT.Domain.Services.Interfaces;

namespace TTT.WebAPI.Controllers
{
  [ApiController]
  [Route("Room")]
  public class RoomController : ControllerBase
  {
    private readonly IRoomService roomService;

    public RoomController(IRoomService roomService)
    {
      this.roomService = roomService;
    }

    [HttpGet("GetIsRoomExisting/{room}")]
    public async Task<IActionResult> GetIsRoomExisting(string room)
    {
      return Ok(await roomService.GetIsRoomExistingAsync(room));
    }

    [HttpGet("GetRooms/{page}")]
    public async Task<IActionResult> GetRooms(int page)
    {
      return Ok(await roomService.GetRooms(page));
    }
  }
}

 