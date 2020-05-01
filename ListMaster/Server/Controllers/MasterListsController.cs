using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ListMaster.Server.Data;
using ListMaster.Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace ListMaster.Server.Controllers
{   
    [Authorize (Roles = "Admin")]
    [Route("[controller]")]
    public class MasterListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MasterListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MasterLists        
        [HttpGet("home")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.MasterLists.ToListAsync());
        }

        // GET: MasterLists/Details/5
        [HttpGet("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterList = await _context.MasterLists
                .FirstOrDefaultAsync(m => m.MasterListId == id);
            if (masterList == null)
            {
                return NotFound();
            }

            return View(masterList);
        }

        // GET: MasterLists/Create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MasterLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MasterListId,Name,Active")] MasterList masterList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(masterList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(masterList);
        }


        // GET: MasterLists/Edit/5
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterList = await _context.MasterLists.FindAsync(id);
            if (masterList == null)
            {
                return NotFound();
            }
            return View(masterList);
        }

        // POST: MasterLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{masterlist}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MasterListId,Name,Active")] MasterList masterList)
        {
            if (id != masterList.MasterListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(masterList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MasterListExists(masterList.MasterListId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(masterList);
        }

        // GET: MasterLists/Delete/5
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var masterList = await _context.MasterLists
                .FirstOrDefaultAsync(m => m.MasterListId == id);
            if (masterList == null)
            {
                return NotFound();
            }

            return View(masterList);
        }

        // POST: MasterLists/Delete/5
        [HttpPost, ActionName("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var masterList = await _context.MasterLists.FindAsync(id);
            _context.MasterLists.Remove(masterList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MasterListExists(int id)
        {
            return _context.MasterLists.Any(e => e.MasterListId == id);
        }
    }
}
