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
    
    public partial class Rol
    {
        public Rol()
        {
            this.Menu = new HashSet<Menu>();
        }
    
        public byte RolId { get; set; }
        public string Nombre { get; set; }
        public byte EstadoId { get; set; }
        public string AspNetRoleId { get; set; }
    
        public virtual ICollection<Menu> Menu { get; set; }
    }
}