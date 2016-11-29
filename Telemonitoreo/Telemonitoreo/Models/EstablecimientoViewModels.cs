using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telemonitoreo.Models
{
    public class ListaEstabViewModel
    {
        public int EstablecimientoId { get; set; }
        public int? Renaes { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Distrito { get; set; }
        public string Provincia { get; set; }
        public string Region { get; set; }
    }
}