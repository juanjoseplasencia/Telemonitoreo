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
    
    public partial class Establecimiento
    {
        public Establecimiento()
        {
            this.Gestante = new HashSet<Gestante>();
            this.Usuario = new HashSet<Usuario>();
            this.GestanteMedicamento = new HashSet<GestanteMedicamento>();
            this.GestanteCita = new HashSet<GestanteCita>();
            this.Gestante1 = new HashSet<Gestante>();
        }
    
        public int EstablecimientoId { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public Nullable<int> Renaes { get; set; }
        public Nullable<int> Ubigeo { get; set; }
        public string EstablecimientoEmail { get; set; }
        public string EstablecimientoTelefono { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<int> UsuarioCreacionId { get; set; }
        public Nullable<int> UsuarioActualizacionId { get; set; }
        public string EstablecimientoRUC { get; set; }
        public byte EstadoId { get; set; }
    
        public virtual ICollection<Gestante> Gestante { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<GestanteMedicamento> GestanteMedicamento { get; set; }
        public virtual ICollection<GestanteCita> GestanteCita { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual ICollection<Gestante> Gestante1 { get; set; }
    }
}
