using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Models
{
    public class MensajeEducacionalViewModel
    {
        public int IdMensajeEducacional { get; set; }
        [Required]
        [Display(Name = "Semana de Embarazo")]
        public string SemanaEmbarazo { get; set; }
        public byte EstadoId { get; set; }
        public int UsuarioEditor { get; set; }
        public virtual ICollection<ContenidoMensajeEducacionalViewModel> Contenido { get; set; } 
    }

    public class ContenidoMensajeEducacionalViewModel
    {
        public int IdContenidoMensajeEducacional { get; set; }
        public int IdMensajeEducacional { get; set; }
        [Display(Name = "Día")]
        public byte DiaSemana { get; set; }
        [Display(Name = "Contenido")]
        public string Contenido { get; set; }
        public virtual MensajeEducacionalViewModel MensajeEducacional { get; set; } 
    }

    public class MensajeEducacionalListViewModel
    {
        [Display(Name = "ID de Registro")]
        public int IdMensajeEducacional { get; set; }
        [Display(Name = "Semana de Embarazo")]
        public string SemanaEmbarazo { get; set; }
        [Display(Name = "Configurado Por")]
        public string UsuarioConfigurador { get; set; }
        [Display(Name = "Establecimiento")]
        public string Establecimiento { get; set; }
        [Display(Name = "Fecha de Creación")]
        public DateTime? FechaCreacion { get; set; }
    }

}