using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Models
{
    public class MenuModel
    {
        public short MenuId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte Nivel { get; set; }
        public Nullable<short> MenuPadre { get; set; }
        public bool EsBoton { get; set; }
        public string MenuImagen { get; set; }
        public byte EstadoId { get; set; }
        public string Url { get; set; }
        public Nullable<byte> Orden { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }

    }
}