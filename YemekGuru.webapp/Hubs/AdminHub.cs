using Microsoft.AspNetCore.SignalR;

namespace YemekGuru.webapp.Hubs
{
    public class AdminHub:Hub
    {
        private static int clientCount = 0;
        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveTotalClientCount", clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveTotalClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
