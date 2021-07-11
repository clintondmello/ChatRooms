using ChatRoomsUI.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoomsUI.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();
        private static IList<User> GroupedUsers = new List<User>();
        public async Task SendMessage(string user, string message, string group)
        {
            await Clients.Group(group).SendAsync("RecieveMessage", user, message);
        }

        public async Task NewUser(User userDetails)
        {
            users.Add(userDetails.ConnectionId, userDetails.UserName);
            await Groups.AddToGroupAsync(userDetails.ConnectionId, userDetails.GroupName);
            GroupedUsers.Add(userDetails);
            await Clients.Group(userDetails.GroupName).SendAsync("UserJoined", userDetails);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = GroupedUsers.Where(x => x.UserName == users[Context.ConnectionId]).Select(x => x.GroupName).FirstOrDefault();
            await Clients.Group(group).SendAsync("UserDisconnected", users[Context.ConnectionId]);
            users.Remove(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
