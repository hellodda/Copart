using Microsoft.AspNetCore.SignalR;

namespace Copart.Api.Hubs
{
    public class BidsHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
