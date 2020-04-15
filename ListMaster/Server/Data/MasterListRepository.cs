using ListMaster.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Data
{
    public class MasterListRepository : IMasterListRepository
    {
        ApplicationDbContext _context;

        public MasterListRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public MasterList GetActiveList()
        {
            return _context.MasterLists.FirstOrDefault(m => m.Active == true);
        }

        public bool AddListoidToList(Listoid listoid)
        {
            _context.Listoids.Add(listoid);
            int result = _context.SaveChanges();

            return result == 0;
        }
    }
}
