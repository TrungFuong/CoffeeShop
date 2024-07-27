using Microsoft.AspNetCore.SignalR;
public class OrderHub : Hub
{
    public async Task NotifyAdmin(string message)
    {
        await Clients.All.SendAsync("ReceiveOrderNotification", message);
    }
}
