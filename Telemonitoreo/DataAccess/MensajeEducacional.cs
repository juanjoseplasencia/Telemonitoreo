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
    
    public partial class MensajeEducacional
    {
        public MensajeEducacional()
        {
            this.ContenidoMensajeEducacional = new HashSet<ContenidoMensajeEducacional>();
        }
    
        public int IdMensajeEducacional { get; set; }
        public string SemanaEmbarazo { get; set; }
        public byte EstadoId { get; set; }
        public int UsuarioEditor { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public bool Eliminado { get; set; }
    
        public virtual ICollection<ContenidoMensajeEducacional> ContenidoMensajeEducacional { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
