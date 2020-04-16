using ListMaster.Server.Models;
using ListMaster.Shared.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ListoidViewModel> GetAllPurgatoryItemsForClient()
        {
            List<ListoidViewModel> results = new List<ListoidViewModel>();

            var CurrentList = GetActiveList();

            var ListOfListoids = _context.Listoids.Where(l => l.MasterList == CurrentList).OrderBy(l => l.CreateDate).Include(u => u.User);

            foreach (Listoid listoid in ListOfListoids)
            {
                int NumberOfKudos = 0;

                if (listoid.Kudos != null)
                {
                    NumberOfKudos = listoid.Kudos.Count();
                }

                if(NumberOfKudos < 3)
                {
                    results.Add(new ListoidViewModel()
                    {
                        MessageBody = listoid.MessageBody,
                        Username = listoid.User.UserName,
                        Kudos = NumberOfKudos,
                        CreateDate = listoid.CreateDate
                    });
                }                
            }

            return results;
        }

        public string GetMasterListName()
        {                        
            MasterList currentlist = _context.MasterLists.FirstOrDefault(m => m.Active);

            return currentlist.Name;
        }

        public IEnumerable<ListoidViewModel> GetAllCurrentMasterListForClient()
        {
            List<ListoidViewModel> results = new List<ListoidViewModel>();

            var CurrentList = GetActiveList();

            var ListOfListoids = _context.Listoids.Where(l => l.MasterList == CurrentList).OrderBy(l => l.CreateDate).Include(u => u.User);

            foreach (Listoid listoid in ListOfListoids)
            {
                int NumberOfKudos = 0;

                if (listoid.Kudos != null)
                {
                    NumberOfKudos = listoid.Kudos.Count();
                }

                if (NumberOfKudos >= 3)
                {
                    results.Add(new ListoidViewModel()
                    {
                        MessageBody = listoid.MessageBody,
                        Username = listoid.User.UserName,
                        Kudos = NumberOfKudos,
                        CreateDate = listoid.CreateDate
                    });
                }
            }

            return results;
        }
    }
}
