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
    
    public partial class TipoObjeto
    {
        public TipoObjeto()
        {
            this.RegistroEvento = new HashSet<RegistroEvento>();
        }
    
        public byte IdTipoObjeto { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<RegistroEvento> RegistroEvento { get; set; }
    }
}
