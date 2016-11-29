using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Models
{
    public class ReporteGestanteCitaViewModel
    {
        public string GestanteNroDocumento { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaCita { get; set; }

        public int EstablecimientoId { get; set; }
    }
}