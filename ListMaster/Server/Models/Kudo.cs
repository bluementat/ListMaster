using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Models
{
    public class Kudo
    {

        public int KudoId { get; set; }

        public ApplicationUser User { get; set; }

        public Listoid listoid { get; set; }
    }
}
