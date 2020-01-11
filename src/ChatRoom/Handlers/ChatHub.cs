using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Data;
using ChatRoom.Entities;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom.Handlers
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
            DateTime now = DateTime.Now;
            string parsedMessage = CommandParsing.ParseMessage(message);
            if (!string.IsNullOrEmpty(parsedMessage))
            {
                await Clients.All.SendAsync("ReceiveMessage", "Bot", parsedMessage, now.ToString("MM/dd/yyyy H:mm"));
            }
            else
            {
                // Persist message into the Database.
                try
                {
                    Chat newChat = new Chat() { UserName = user, Message = message, Date = now };
                    _context.Add(newChat);
                    await _context.SaveChangesAsync();
                    await Clients.All.SendAsync("ReceiveMessage", user, message, now.ToString("MM/dd/yyyy H:mm"));
                }
                catch
                {
                    await Clients.All.SendAsync("ReceiveMessage", "Bot", "Error saving post, please try again.", now.ToString("MM/dd/yyyy H:mm"));
                }
            }
        }
    }
}
