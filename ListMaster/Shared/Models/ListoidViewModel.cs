using System;
using System.Collections.Generic;
using System.Text;

namespace ListMaster.Shared.Models
{
    public class ListoidViewModel
    {
        public int ListoidId { get; set; }

        public int MasterListId { get; set; }

        public int Kudos { get; set; }

        public string MessageBody { get; set; }

        public string Username { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
