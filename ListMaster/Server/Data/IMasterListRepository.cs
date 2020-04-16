using ListMaster.Server.Models;
using ListMaster.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListMaster.Server.Data
{
    public interface IMasterListRepository
    {
        public MasterList GetActiveList();

        public bool AddListoidToList(Listoid listoid);

        public IEnumerable<ListoidViewModel> GetAllPurgatoryItemsForClient();

        // public IEnumerable<Listoid> GetListoidsFromMasterList(MasterList masterlist);
    }
}
