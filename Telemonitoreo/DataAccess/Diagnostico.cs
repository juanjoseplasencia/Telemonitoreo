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
    
    public partial class Diagnostico
    {
        public Diagnostico()
        {
            this.Gestante = new HashSet<Gestante>();
            this.Gestante1 = new HashSet<Gestante>();
            this.Gestante2 = new HashSet<Gestante>();
            this.Gestante3 = new HashSet<Gestante>();
        }
    
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Id10 { get; set; }
        public string Grupo { get; set; }
    
        public virtual ICollection<Gestante> Gestante { get; set; }
        public virtual ICollection<Gestante> Gestante1 { get; set; }
        public virtual ICollection<Gestante> Gestante2 { get; set; }
        public virtual ICollection<Gestante> Gestante3 { get; set; }
    }
}
