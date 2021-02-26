using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace queuehandler.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        public string Estado { get; set; }

        [BindProperty]
        public string TipoServicio { get; set; }

        //public string[] TipoServicios = new[] { "Embarazada", "Envejeciente", "Priorizado" };
        public string Prioridad { get; set; }

        public DateTime Fecha { get; set; }
    }
}
