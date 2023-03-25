using TTT.Data.Entities;

namespace TTT.Domain.Services.Interfaces
{
  public interface IRoomService
  {
    Task<Room[]> GetRooms(int page);
    Task<int> GetIsRoomExistingAsync(string roomName);
    Task<Room> AddRoomAsync(Room room);
    Task<Room> RemoveRoomAsync(string roomName);
    
    Task<Room> UpdateIsRoomFilledAsync(string roomName);
  }
}
