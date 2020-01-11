using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Data;
using ChatRoom.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            // Parse message to verify if it is a regular message.
            bool isRegularMessage = true;

            if (isRegularMessage)
            {
                DateTime now = DateTime.Now;
                // Persist message into the Database.
                Chat newChat = new Chat() { UserName = user, Message = message, Date = now };
                _context.Add(newChat);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("ReceiveMessage", user, message, now.ToString("MM/dd/yyyy H:mm"));
            }

        }
    }
}
