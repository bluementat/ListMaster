using ListMaster.Server.Models;
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

        public bool SaveMessage(ChatMessage message)
        {
            _context.ChatMessages.Add(message);
            int result = _context.SaveChanges();

            return result == 0;
        }
    }
}
