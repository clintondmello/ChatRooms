using ChatRoomsUI.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatRoomsUI.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        public async Task NewUser(User userDetails)
        {
            users.Add(userDetails.ConnectionId, userDetails.UserName);
            await Clients.Others.SendAsync("UserJoined", userDetails);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", users[Context.ConnectionId]);
            users.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
