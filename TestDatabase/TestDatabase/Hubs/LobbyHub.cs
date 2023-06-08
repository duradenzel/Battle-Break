
using Microsoft.AspNetCore.SignalR;

namespace TestDatabase.Hubs
{
    public class LobbyHub : Hub
    {

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public async Task CheckSignalRConnection()
        //{
        //    await Clients.Caller.SendAsync("SignalRConnectionChecked", "SignalR connection is working!");
        //}

        private static readonly List<string> ConnectedClients = new List<string>();

        public override Task OnConnectedAsync()
        {
            ConnectedClients.Add(Context.ConnectionId);
            Clients.Others.SendAsync("UserConnected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedClients.Remove(Context.ConnectionId);
            Clients.Others.SendAsync("UserDisconnected", Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        public List<string> GetConnectedClients()
        {
            return ConnectedClients.ToList();
        }

        public async Task RedirectUsers(string url)
        {
            await Clients.All.SendAsync("RedirectToUrl", url);
        }


    }
}
