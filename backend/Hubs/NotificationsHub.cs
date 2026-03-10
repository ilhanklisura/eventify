namespace Eventify.Backend.Hubs;

using Microsoft.AspNetCore.SignalR;

public class NotificationsHub : Hub
{
    public async Task SendToAll(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }

    public async Task SendToUser(string userName, string message)
    {
        await Clients.User(userName).SendAsync("ReceiveMessage", message);
    }

    public Task<int> Add(int a, int b) => Task.FromResult(a + b);
}

