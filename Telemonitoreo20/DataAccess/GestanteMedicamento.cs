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
    
    public partial class GestanteMedicamento
    {
        public GestanteMedicamento()
        {
            this.GestanteMedicamentoDetalle = new HashSet<GestanteMedicamentoDetalle>();
        }
    
        public int GestanteMedicamentoId { get; set; }
        public int GestanteKey { get; set; }
        public string NombreMedico { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public int EstablecimientoId { get; set; }
        public bool Eliminado { get; set; }
    
        public virtual Gestante Gestante { get; set; }
        public virtual ICollection<GestanteMedicamentoDetalle> GestanteMedicamentoDetalle { get; set; }
        public virtual Establecimiento Establecimiento { get; set; }
    }
}
