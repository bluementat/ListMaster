using ListMaster.Server.Models;
using ListMaster.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Data
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        ApplicationDbContext _context;
        
        public ChatMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ChatMessage> GetAllMessages()
        {
            return _context.ChatMessages.OrderBy(m => m.ChatMessageId);
        }

        public IEnumerable<ChatMessageViewModel> GetAllMessagesForClient()
        {
            List<ChatMessageViewModel> results = new List<ChatMessageViewModel>();
            
            var messages = _context.ChatMessages.OrderBy(m => m.ChatMessageId);

            foreach(ChatMessage message in messages)
            {
                results.Add(new ChatMessageViewModel()
                {
                    MessageBody = message.MessageBody,
                    Username = message.User.UserName,
                    Kudos = message.MessageKudos.Count(),
                    CreatedDate = message.CreatedDate
                });
            }

            return results;
        }

        public bool SaveMessage(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            int result = _context.SaveChanges();

            return result == 0;
        }
    }
}
