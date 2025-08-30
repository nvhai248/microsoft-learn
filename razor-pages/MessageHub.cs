namespace razor_pages;
using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub
{
    public async Task PostMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveNewMessage", message);
    }
}
