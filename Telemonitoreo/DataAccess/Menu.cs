//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Menu
    {
        public Menu()
        {
            this.Rol = new HashSet<Rol>();
        }
    
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
    
        public virtual ICollection<Rol> Rol { get; set; }
    }
}
