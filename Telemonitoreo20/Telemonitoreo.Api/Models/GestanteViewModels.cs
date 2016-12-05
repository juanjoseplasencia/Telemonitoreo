using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Api.Models
{
    public class GestanteModel
    {
        public int GestanteKey { get; set; }
        [Required(ErrorMessage = "El número de DNI es requerido.")]
        [Display(Name = "Nro Documento")]
        public string GestanteNroDocumento { get; set; }
        [Required]        
        public string Nombres { get; set; }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }
        [Required]
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string GestanteId { get; set; }
        [Required]
        [Display(Name = "Establecimiento de Salud")]
        public int EstablecimientoId { get; set; }
        [Display(Name = "Establecimiento de Salud")]
        public string Establecimiento { get; set; }
        [Required]
        [Display(Name = "Establecimiento de Notificación")]
        public int EstablecimientoNotificacionId { get; set; }
        [Display(Name = "Establecimiento de Notificación")]
        public string EstablecimientoNotificacion { get; set; }
        [Required]        
        [Display(Name = "Fecha de Ultima Regla (FUR)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaUltimaRegla { get; set; }
        [Display(Name = "Fecha Probable de Parto (FPP)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FechaProbableParto { get; set; }
        [Required]        
        [Display(Name = "Presión Sistólica Base")]        
        public int PresionSistolicaBase { get; set; }
        [Required]        
        [Display(Name = "Presión Diastólica Base")]        
        public int PresionDiastolicaBase { get; set; }
        [Required]
        [Display(Name = "Diagnóstico de Ingreso")]
        public Nullable<int> DiagnosticoIngreso { get; set; }
        [Display(Name = "Diagnóstico de Ingreso")]        
        public string DiagIngreso { get; set; }
        public string DiagIngresoCie10 { get; set; }        
        [Display(Name = "Diagnóstico Intermedio 1")]
        public Nullable<int> DiagnosticoIntermedio1 { get; set; }
        [Display(Name = "Diagnóstico Intermedio 1")]
        public string DiagIntermedio1 { get; set; }
        public string DiagIntermedio1Cie10 { get; set; }
        [Display(Name = "Diagnóstico Intermedio 2")]
        public Nullable<int> DiagnosticoIntermedio2 { get; set; }
        [Display(Name = "Diagnóstico Intermedio 2")]
        public string DiagIntermedio2 { get; set; }
        public string DiagIntermedio2Cie10 { get; set; }
        [Display(Name = "Diagnóstico de Egreso")]
        public Nullable<int> DiagnosticoEgreso { get; set; }
        [Display(Name = "Diagnóstico de Egreso")]
        public string DiagEgreso { get; set; }
        public string DiagEgresoCie10 { get; set; }
        [Required]
        [Display(Name = "Dirección")]
        public string GestanteDireccion { get; set; }
        [Required]        
        [Display(Name = "Correo Electrónico")]        
        public string GestanteEmail { get; set; }
        [Required]
        [Display(Name = "Número Celular")]
        public string GestanteTelefono { get; set; }
        [Required]
        [Display(Name = "Horario para Mensajes")]
        public byte HorarioMensaje { get; set; }
        [Required]
        [Display(Name = "Distrito de Procedencia")]
        public string DistritoId { get; set; }
        [Display(Name = "Distrito de Procedencia")]
        public string Distrito { get; set; }
        [Display(Name = "Provincia de Procedencia")]
        public string ProvinciaId { get; set; }
        [Display(Name = "Provincia de Procedencia")]
        public string Provincia { get; set; }
        [Display(Name = "Región de Procedencia")]
        public string RegionId { get; set; }
        [Display(Name = "Región de Procedencia")]
        public string Region { get; set; }
        public int UsuarioEditor { get; set; }
    }

    public class GestanteListaModel
    {
        public int GestanteKey { get; set; }
        public string GestanteNroDocumento { get; set; }
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public int EstablecimientoId { get; set; }
        public string Establecimiento { get; set; }
        public Nullable<System.DateTime> FechaProbableParto { get; set; }
        public string GestanteTelefono { get; set; }
    }

    public class GestanteCitaModel
    {
        public int GestanteCitaId { get; set; }
        [Required]
        [Display(Name = "Número de DNI")]
        public string GestanteNroDocumento { get; set; }
        public int GestanteKey { get; set; }
        [Required]
        [Display(Name = "Fecha de Cita")]
        public Nullable<System.DateTime> FechaCita { get; set; }
        [Required]        
        [Display(Name = "Hora de Cita")]        
        public string HoraCita { get; set; }
        [Display(Name = "Médico Programado")]
        public string NombreMedico { get; set; }
        [Required]
        [Display(Name = "Establecimiento de Salud")]
        public Nullable<int> EstablecimientoId { get; set; }
        public string Establecimiento { get; set; }
        public int UsuarioEditor { get; set; }
    }

    public class GestanteCitaListaModel
    {
        public string GestanteCitaId { get; set; }
        public string GestanteNroDocumento { get; set; }
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public string HoraCita { get; set; }
        public int EstablecimientoId { get; set; }
        public string Establecimiento { get; set; }
    }

    public class GestanteMonitoreoModel
    {
        public string GestanteNroDocumento { get; set; }
        public int GestanteKey { get; set; }
        public Nullable<int> PresionSistolica { get; set; }
        public Nullable<int> PresionDiastolica { get; set; }
        public Nullable<int> Proteinuria { get; set; }
        public Nullable<int> MovimientosFetales { get; set; }
        public string SignosAlarma { get; set; }
    }

    public class GestanteMonitoreoListaModel
    {
        public string GestanteMonitoreoId { get; set; }
        public string GestanteNroDocumento { get; set; }
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public Nullable<int> PresionSistolica { get; set; }
        public Nullable<int> PresionDiastolica { get; set; }
        public Nullable<int> Proteinuria { get; set; }
        public Nullable<int> MovimientosFetales { get; set; }
        public string SignosAlarma { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }

    public class GestanteDataViewModel
    {
        public int GestanteKey { get; set; }
        [Display(Name = "Nro Documento")]
        public string GestanteNroDocumento { get; set; }
        public string Nombres { get; set; }
        [Display(Name = "A. Paterno")]
        public string APaterno { get; set; }
        [Display(Name = "A. Materno")]
        public string AMaterno { get; set; }
    }

    public class GestanteMedicamentoViewModel
    {
        public int GestanteMedicamentoId { get; set; }
        [Required]
        public int GestanteKey { get; set; }
        [Display(Name = "DNI")]
        public string GestanteDni { get; set; }
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }
        [Required]
        [Display(Name = "Fecha de Medicación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Fecha { get; set; }
        [Display(Name = "Medico que Receta")]
        public string NombreMedico { get; set; }
        [Required]
        [Display(Name = "Establecimiento de Salud")]
        public int EstablecimientoId { get; set; }
        public virtual ICollection<GestanteMedicamentoDetalleViewModel> Medicamentos { get; set; } 
    }

    public class GestanteMedicamentoDetalleViewModel
    {
        public int GestanteMedicamentoDetalleId { get; set; }
        public int GestanteMedicamentoId { get; set; }
        public int MedicamentoId { get; set; }
        public string Descripcion { get; set; }
        public string Dosis { get; set; }
        public int? Dias { get; set; }
        public string Cantidad { get; set; }
        public string Instrucciones { get; set; }
        public virtual GestanteMedicamentoViewModel GestanteMedicamento { get; set; } 
    }

    public class MedicamentoViewModel
    {
        public int MedicamentoId { get; set; }
        public string Descripcion { get; set; }
        public string Concentracion { get; set; }
        public string Formato { get; set; }
        public string Presentacion { get; set; }
    }

    public class GestanteMedListViewModel
    {
        public int GestanteMedicamentoId { get; set; }
        public string GestanteDni { get; set; }
        public string GestanteNombres { get; set; }
        public string GestanteAPaterno { get; set; }
        public string GestanteAMaterno { get; set; }
        public string Establecimiento { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class GestanteMedDetalleListViewModel
    {
        public int GestanteMedicamentoDetalleId { get; set; }
        public string Medicamento { get; set; }
        public string Dosis { get; set; }
    }

    public class DiagnosticoViewModel
    {
        public int DiagnosticoId { get; set; }
        public string Cie10 { get; set; }
        public string Descripcion { get; set; }
    }

}