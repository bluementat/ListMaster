using System;
using System.Collections.Generic;
using System.Text;

namespace ListMaster.Shared.Models
{
    public class ChatMessageViewModel
    {
        public string MessageBody { get; set; }

        public string Username { get; set; }        

        public DateTime CreatedDate { get; set; }
    }
}
