using Microsoft.AspNetCore.SignalR;

namespace Back.Hubs;

public class NotesHub : Hub
{
    public async Task SendNoteUpdate(int noteId, string content)
    {
        await Clients.OthersInGroup($"note-{noteId}")
            .SendAsync("ReceiveNoteUpdate", content);
    }

    public async Task JoinNoteGroup(int noteId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"note-{noteId}");
    }

    public async Task LeaveNoteGroup(int noteId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"note-{noteId}");
    }
}