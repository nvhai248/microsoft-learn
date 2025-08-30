namespace razor_pages;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public class ChatHub : Hub
{
    // handle user list online
    private static ConcurrentDictionary<string, string> OnlineUsers = new ConcurrentDictionary<string, string>();

    // When user connect
    public override async Task OnConnectedAsync()
    {
        string connectionId = Context.ConnectionId;

        // get username form header or auth
        string username = Context.GetHttpContext()?.Request.Query["username"] ?? connectionId;

        OnlineUsers[connectionId] = username;

        // send information user online to all user
        await Clients.All.SendAsync("UserConnected", username, OnlineUsers.Values);

        await base.OnConnectedAsync();
    }

    // user disconnect
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (OnlineUsers.TryRemove(Context.ConnectionId, out string? username))
        {
            // send to all clieent
            await Clients.All.SendAsync("UserDisconnected", username, OnlineUsers.Values);
        }

        await base.OnDisconnectedAsync(exception);
    }

    // Send message
    public async Task PostMessage(string message)
    {
        string username = OnlineUsers.GetValueOrDefault(Context.ConnectionId) ?? "Anonymous";

        await Clients.All.SendAsync("ReceiveNewMessage", username, message);
    }

    // Optional: Handle list user online
    public Task GetOnlineUsers()
    {
        return Clients.Caller.SendAsync("OnlineUsers", OnlineUsers.Values);
    }
}
