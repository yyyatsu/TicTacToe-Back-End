using Microsoft.EntityFrameworkCore;
using TTT.Data.Entities;
using TTT.Data.Repository;
using TTT.Domain.Services.Interfaces;

namespace TTT.Domain.Services.Implementations
{
  public class RoomService : IRoomService
  {
    private readonly IRepository<Room> roomRepository;

	public RoomService(IRepository<Room> roomRepository)
	{
	  this.roomRepository = roomRepository;
	}

	public async Task<int> GetIsRoomExistingAsync(string roomName)
	{
	  int boardSize = 0;
	  var room = await roomRepository.GetAll().FirstOrDefaultAsync(r => r.Name == roomName);

	  if(room is not null)

	  boardSize = room.BoardSize;

	  return boardSize;
	}

	public async Task<Room> AddRoomAsync(Room room)
	{
	  var r = await roomRepository.AddAsync(room);
	  await roomRepository.SaveChangesAsync();

	  return r;
	}

	public async Task<Room> RemoveRoomAsync(string roomName)
	{
      var room = await roomRepository.GetAll().FirstOrDefaultAsync(r => r.Name == roomName);
	  if (room is not null)
	  {
		var result = await roomRepository.RemoveAsync(room);
		await roomRepository.SaveChangesAsync();
	  }
	  return null;
	}

	public async Task<Room> UpdateIsRoomFilledAsync(string roomName)
	{
	  var room = await roomRepository.GetAll().FirstOrDefaultAsync(r => r.Name == roomName);
	  if (room is not null)
	  {
		room.IsFilled = true;
		var result = await roomRepository.UpdateAsync(room!);
		await roomRepository.SaveChangesAsync();
        return result;

      }

	  return null;
    }

    public async Task<Room[]> GetRooms(int page)
    {
	  var rooms = roomRepository.GetAll().Skip((page - 1)*4).Take(page*4);
	  return rooms.ToArray();
    }
  }
}
