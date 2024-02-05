using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace SoulShare_Group06
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();

        public void Send(string name, string message)
        {
            // Get the room ID from the connected user's connection ID
            var roomId = ConnectedUsers[Context.ConnectionId];
            // Broadcast the message to all clients in the same room
            Clients.Group(roomId).broadcastMessage(name, message);
        }

        public void JoinRoom(string roomId)
        {
            // Add the user to the specified room
            Groups.Add(Context.ConnectionId, roomId);
            ConnectedUsers[Context.ConnectionId] = roomId;
        }

        public void QuitRoom(string roomId)
        {
            // Remove the user from the specified room
            Groups.Remove(Context.ConnectionId, roomId);
            ConnectedUsers.Remove(Context.ConnectionId);
        }
    }
}