using ListMaster.Server.Data;
using ListMaster.Server.Models;
using ListMaster.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IChatMessageRepository _messagerepo;

        public ChatHub(IChatMessageRepository messagerepo, UserManager<ApplicationUser> userManager)
        {
            _messagerepo = messagerepo;
            _userManager = userManager;
        }

        public async Task GetCurrentMessages(string connectionid)
        {
            await Clients.Client(connectionid).SendAsync("ReceiveCurrentMessages", _messagerepo.GetAllMessagesForClient());
        }

        public async Task SendMessage(ChatMessageViewModel message)
        {
            var user = await _userManager.FindByNameAsync(message.Username);

            var messageToSave = new ChatMessage()
            {
                MessageBody = message.MessageBody,
                MessageKudos = new List<Kudo>(),
                User = user,
                CreatedDate = message.CreatedDate,
            };
            
            // Store message in repository
            _messagerepo.SaveMessage(messageToSave);

            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
