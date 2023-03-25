using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TTT.Data.Entities;
using TTT.Domain.Models;
using TTT.Domain.Services.Interfaces;

namespace TTT.Domain.Hubs
{
  [Authorize]
  public class LobbyHub : Hub
  {
    private readonly IDictionary<string, Connection> connections;
    private readonly IRoomService roomService;

    public LobbyHub(IDictionary<string, Connection> connections, IRoomService roomService)
    {
      this.connections = connections;
      this.roomService = roomService;
    }

    public async Task LeaveRoom(string roomName)
    {
      connections.Remove(Context.ConnectionId);
      await roomService.RemoveRoomAsync(roomName);
    }

    public async override Task OnDisconnectedAsync(Exception? exception)
    {
      if (connections.TryGetValue(Context.ConnectionId, out Connection? connection))
        await roomService.RemoveRoomAsync(connection.RoomName);
      connections.Remove(Context.ConnectionId);
    }

    public async Task JoinRoom(Connection connection, int? boardSize)
    {
      if (!IsRoomFilled(connection.RoomName))
      {
        if (connections.Values.Count(r => r.RoomName == connection.RoomName) == 0)
        {
          await roomService.AddRoomAsync(new Room { Name = connection.RoomName, BoardSize = (int)boardSize!, IsFilled = false });
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.RoomName);

        connections[Context.ConnectionId] = connection;

        await SendStartGameInformation(connection.RoomName);
      }
    }

    public bool IsRoomFilled(string roomName)
    {
      return connections.Values.Count(c => c.RoomName == roomName) == 2;
    }

    public async Task SendStartGameInformation(string roomName)
    {
      if (IsRoomFilled(roomName))
      {
        await roomService.UpdateIsRoomFilledAsync(roomName);
        string firstPlayerName = connections.Values.First().UserName;
        string secondPlayerName = connections.Values.Last().UserName;

        StartGameInformation firstPlayerGameInformation = new(true, "x", secondPlayerName);
        StartGameInformation secondPlayerGameInformation = new(false, "o", firstPlayerName);

        await Clients.OthersInGroup(roomName).SendAsync("ReceiveStartGameInformation", firstPlayerGameInformation);
        await Clients.Caller.SendAsync("ReceiveStartGameInformation", secondPlayerGameInformation);
      }
    }

    public async Task SendMove(string[] values)
    {
      if (connections.TryGetValue(Context.ConnectionId, out Connection? connection))
      {
        await Clients.OthersInGroup(connection.RoomName).SendAsync("ReceiveMove", values);
      }
    }

    public async Task SendGameResult(int[]? line)
    {
      if (connections.TryGetValue(Context.ConnectionId, out Connection? connection))
      {
        if (line is null)
        {
          await Clients.Group(connection.RoomName).SendAsync("ReceiveGameResult", "Draw");
          return;
        }

        await Clients.Caller.SendAsync("ReceiveGameResult", line, "Win");
        await Clients.OthersInGroup(connection.RoomName).SendAsync("ReceiveGameResult", line, "Lose");

        connections.Clear();
      }
    }
  }
}
