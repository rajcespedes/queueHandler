using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using queuehandler.Models;

namespace queuehandler.Controllers
{
    public class TurnoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TurnoController(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Ticket Ticket { get; set; }
        public IActionResult Index()
        {
            //var tempNum = _db.Ticket.Max(u => u.Numero) + 1;

            return View();
        }

        [HttpPost]
        public IActionResult OnPost()
        {

            if(ModelState.IsValid)
            {

                if(_db.Ticket.FirstOrDefault() == null)
                {
                    Ticket.Numero = 1;
                } else
                {
                    Ticket.Numero = _db.Ticket.Max(u => u.Numero) + 1;
                }

                    

                Ticket.Fecha = DateTime.Today;

                Ticket.Estado = "Pendiente";

                _db.Ticket.Add(Ticket);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            

            return View(Ticket);

        }

    }
}