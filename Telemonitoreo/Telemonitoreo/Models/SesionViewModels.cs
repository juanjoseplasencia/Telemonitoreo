using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Models
{
    public class SesionViewModel
    {
        public int RegistroEventoKey { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Accion { get; set; }
        public string Menu { get; set; }
        public int? IdRegistro { get; set; }
        public DateTime? EventoFecha { get; set; }
        public string Origen { get; set; }
    }
}