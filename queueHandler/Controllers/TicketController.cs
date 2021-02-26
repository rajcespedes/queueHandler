using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using queuehandler.Models;

namespace queuehandler.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TicketController(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Json(new { data = await _db.Ticket.ToListAsync() });
        }

        public IActionResult Upsert(int? id)
        {

            Ticket = new Ticket();          

            if (id == null)
            {
               return View(Ticket);
            }

            Ticket = _db.Ticket.FirstOrDefault(u => u.Id == id);

            if (Ticket == null)
            {
                return NotFound();
            }

            return View(Ticket);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {

                if (Ticket.Id == 0)
                {
                    Ticket.Numero = _db.Ticket.Max(u => u.Numero) + 1;

                    Ticket.Fecha = DateTime.Today;

                    _db.Ticket.Add(Ticket);
                }
                else
                {
                    _db.Ticket.Update(Ticket);
                }

                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(Ticket);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Console.WriteLine(id);

            var ticketToDelete = await _db.Ticket.FirstOrDefaultAsync(u => u.Id == id);

            if (ticketToDelete == null)
            {
                return Json(new { success =  false, message = "Error while deleting" });
            }

            _db.Ticket.Remove(ticketToDelete);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete successful" });

            
        }

    }


}