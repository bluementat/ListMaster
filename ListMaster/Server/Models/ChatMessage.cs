using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Models
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }

        public string MessageBody { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
