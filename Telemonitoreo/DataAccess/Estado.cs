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
    
    public partial class Estado
    {
        public Estado()
        {
            this.Usuario = new HashSet<Usuario>();
            this.Establecimiento = new HashSet<Establecimiento>();
        }
    
        public byte Id { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<Establecimiento> Establecimiento { get; set; }
    }
}
