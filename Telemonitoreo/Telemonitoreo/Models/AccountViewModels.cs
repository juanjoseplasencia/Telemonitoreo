using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Telemonitoreo.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordar mi usuario")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El número de D.N.I. es requerido.")]
        [Display(Name = "Número de D.N.I")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "La clave de usuario es requerida.")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave de Usuario")]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirmación de Clave")]
        [Compare("Password", ErrorMessage = "La clave y confirmación de clave ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "el nombre o nombres es requerido.")]
        [Display(Name = "Nombre(s)")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido.")]
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido.")]
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }

        [Required(ErrorMessage = "El establecimiento de salud es requerido.")]
        [Display(Name = "Establecimiento de Salud")]
        public int EstablecimientoId { get; set; }

        [Required(ErrorMessage = "El rol asignado es requerido.")]
        [Display(Name = "Rol Asignado")]
        public string RoleName { get; set; }
        
        [Display(Name = "Dirección")]
        public string UsuarioDireccion { get; set; }

        [Required(ErrorMessage = "El estado de usuario es requerido.")]
        [Display(Name = "Estado de Usuario")]
        public byte EstadoId { get; set; }

        [Display(Name = "¿Recibe Alarmas?")]
        public byte RecibeAlertas { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        
        [Phone]
        [Display(Name = "Número de Celular")]
        public string PhoneNumber { get; set; }
    }

    public class EditRegisterViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int UsuarioKey { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Número de D.N.I")]
        public string UserName { get; set; }

        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Clave de Usuario")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmación de Clave")]
        [Compare("Password", ErrorMessage = "La clave y confirmación de clave ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Nombre(s)")]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }

        [Required]
        [Display(Name = "Establecimiento de Salud")]
        public int EstablecimientoId { get; set; }

        [Required]
        [Display(Name = "Rol Asignado")]
        public string RoleName { get; set; }

        [Display(Name = "Dirección")]
        public string UsuarioDireccion { get; set; }

        [Required]
        [Display(Name = "Estado de Usuario")]
        public byte EstadoId { get; set; }

        [Display(Name = "¿Recibe Alarmas?")]
        public byte RecibeAlertas { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de Celular")]
        public string PhoneNumber { get; set; }
    }

    public class EditPersonalViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int UsuarioKey { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Número de D.N.I")]
        public string UserName { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Nombre(s)")]
        public string Nombres { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }

        [Required]
        [Editable(false)]
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }

        [Required]
        [Display(Name = "Establecimiento de Salud")]
        public int EstablecimientoId { get; set; }

        //[Required]
        //[Display(Name = "Rol Asignado")]
        //public string RoleName { get; set; }

        [Display(Name = "Dirección")]
        public string UsuarioDireccion { get; set; }

        //[Required]
        //[Display(Name = "Estado de Usuario")]
        //public byte EstadoId { get; set; }

        [Display(Name = "¿Recibe Alarmas?")]
        public byte RecibeAlertas { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Número de Celular")]
        public string PhoneNumber { get; set; }
    }

    public class UsuarioListaModel
    {
        public int UsuarioKey { get; set; }
        [Required]
        [Display(Name = "Número de D.N.I")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Nombre(s)")]
        public string Nombres { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string APaterno { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public string AMaterno { get; set; }

        [Required]
        public int EstablecimientoId { get; set; }
        public string Establecimiento { get; set; }
        
        [Required]
        [Display(Name = "Rol Asignado")]
        public string RoleName { get; set; }
        
        [Display(Name = "Estado de Usuario")]
        public string Estado { get; set; }
    }

    public class CambiarPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Ingrese su contraseña actual")]
        public string PasswordAnterior { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Ingrese su nueva contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme su nueva contraseña")]
        [Compare("Password", ErrorMessage = "La clave y confirmación de clave ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ReniecViewModel
    {
        public string NumDni { get; set; }        
        public string Nombres { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Direccion { get; set; }
        public int GeneroId { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CodRetorno { get; set; }
    }

    public class RolMenuViewModel
    {
        public short MenuId { get; set; }
        public string Nombre { get; set; }
        public byte Nivel { get; set; }
        public short? MenuPadre { get; set; }
        public bool EsBoton { get; set; }
        public byte? Orden { get; set; }
        public bool? AccesoAdministrador { get; set; }
        public bool? AccesoPersonal { get; set; }
        public bool? AccesoAnalista { get; set; }
        public bool? AccesoGestante { get; set; }
    }

}
