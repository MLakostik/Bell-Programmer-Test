using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITSupportTickets.Data;
using ITSupportTickets.Models;

namespace ITSupportTickets.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITSupportTicketsContext _context;

        public TicketsController(ITSupportTicketsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ticket.ToListAsync());
        }

        //GET: Tickets/Search
        public async Task<IActionResult> Search()
        {
            return View();
        }
        //POST: Tickets/Results
        public async Task<IActionResult> SearchResults(String SearchFilter, String ProjectSearch, String DepartmentSearch, DateTime DateTimeSearch, String EmployeeSearch, String DescriptionSearch)
        {
            switch (SearchFilter)
            {
                case "projectname":
                    return View("Index", (await _context.Ticket.Where(i => i.ProjectName.Contains(ProjectSearch)).ToListAsync()));
                case "department":
                    return View("Index", (await _context.Ticket.Where(i => i.DepartmentName.Contains(DepartmentSearch)).ToListAsync()));
                case "datetime":
                    return View("Index", (await _context.Ticket.Where(i => ((i.SubmittedTimeStamp).Date).Equals(DateTimeSearch.Date)).ToListAsync()));
                case "requestedby":
                    return View("Index", (await _context.Ticket.Where(i => (i.RequestedBy).Contains(EmployeeSearch)).ToListAsync()));
                case "description":
                    return View("Index", (await _context.Ticket.Where(i => (i.Description).Contains(DescriptionSearch)).ToListAsync()));
                default:
                    return View("Index", await _context.Ticket.ToListAsync());
            }
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectName,DepartmentName,RequestedBy,Description,SubmittedTimeStamp")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.SubmittedTimeStamp = DateTime.Now;
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectName,DepartmentName,RequestedBy,Description,SubmittedTimeStamp")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
