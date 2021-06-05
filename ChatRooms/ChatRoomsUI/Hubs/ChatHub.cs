﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatRoomsUI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        public async Task NewUser(string connectionId)
        {
            await Clients.Others.SendAsync("UserJoined", connectionId);
        }

    }
}
