using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using queuehandler.Models;

namespace queuehandler.Controllers
{
    public class CajeroController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CajeroController(ApplicationDbContext db)
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
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Ticket.Where(u => u.TipoServicio == "Caja" && u.Estado == "Pendiente").ToList() });
        }

        [HttpPost]
        public IActionResult Estado() {

            if (ModelState.IsValid)
            {

                var ticketFound = _db.Ticket.FirstOrDefault( u => u.Id == Ticket.Id);

                ticketFound.Estado = Ticket.Estado;

                _db.SaveChanges();

            }

            return RedirectToAction("Index", "Cajero");
        }

    }
}