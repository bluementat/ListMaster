using ListMaster.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Data
{
    public interface IChatMessageRepository
    {
        public IEnumerable<ChatMessage> GetAllMessages();

        public bool SaveMessage(ChatMessage message);
    }
}
