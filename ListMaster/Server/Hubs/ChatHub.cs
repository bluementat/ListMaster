using ListMaster.Server.Data;
using ListMaster.Server.Models;
using ListMaster.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Hubs
{
    public class ChatHub : Hub
    {
        private IChatMessageRepository _messagerepo;

        public ChatHub(IChatMessageRepository messagerepo)
        {
            _messagerepo = messagerepo;
        }

        public async Task GetCurrentMessages(string connectionid)
        {
            await Clients.Client(connectionid).SendAsync("ReceiveCurrentMessages", _messagerepo.GetAllMessages());
        }

        public async Task SendMessage(string username, ChatMessageViewModel message)
        {
            var messageToSave = new ChatMessage()
            {
                MessageBody = message.MessageBody,
                CreatedDate = message.CreatedDate,
                // NEED USER NAME
            };
            
            // Store message in repository
            _messagerepo.SaveMessage(messageToSave);

            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
