using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Models
{
    public class Listoid
    {
        public int ListoidId { get; set; }

        public MasterList MasterList { get; set; }

        public ICollection<Kudo> Kudos { get; set; }

        public string MessageBody { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
