using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ListMaster.Server.Models
{
    public class MasterList
    {
        public int MasterListId { get; set; }
        
        public string Name { get; set; }

        public ICollection<Listoid> Listoids { get; set; }

        public bool Active { get; set; }
    }
}
