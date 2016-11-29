using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Telemonitoreo.Business;

namespace Telemonitoreo.Controllers
{
    public abstract class BaseController : Controller
    {
        readonly SeguridadManager _seguridadManager = new SeguridadManager();
        readonly UtilManager _utilManager = new UtilManager();
        readonly UsuarioManager _usuarioManager = new UsuarioManager();

        public void ConfigurarMenues()
        {
            byte roleId = 0;
            if (User.Identity.IsAuthenticated) 
            {
                roleId = _seguridadManager.ObtenerRoleIdByUserId(User.Identity.GetUserId());
            }
            ViewBag.LeftBarMenu = _seguridadManager.ListarMenues(roleId);
        }

        public byte ObtenerPerfil()
        {
            byte roleId = 0;
            if (User.Identity.IsAuthenticated)
            {
                roleId = _seguridadManager.ObtenerRoleIdByUserId(User.Identity.GetUserId());
            }
            return roleId;
        }

        public int ObtenerEstablecimiento()
        {
            var establecimientoId = 0;
            if (!User.Identity.IsAuthenticated) return establecimientoId;

            var usuario = _usuarioManager.MostrarUsuarioLogueado(User.Identity.GetUserId());
            establecimientoId = usuario.EstablecimientoId;
            return establecimientoId;
        }
        protected int ObtenerUsuarioKeyLogeado()
        {
            var usuarioKey = 1;
            if (User.Identity.IsAuthenticated)
            {
                usuarioKey = _seguridadManager.ObtenerUsuarioKeyByUserId(User.Identity.GetUserId());
            }
            return usuarioKey;
        }

        protected int EstablecimientoRestriccion()
        {
            var usuarioDato = ((ClaimsIdentity)User.Identity).FindFirst("EstablecimientoIdRestriccion");
            var establecimientoId = usuarioDato != null ? usuarioDato.Value : string.Empty;
            if (!string.IsNullOrWhiteSpace(establecimientoId))
            {
                return Convert.ToInt32(establecimientoId);
            }
            return 0;
        }

        public void RegistrarAccion(byte accion, byte objeto, int? idRegistro)
        {
            var userLogId = "1";
            if (Request.IsAuthenticated)
            {
                userLogId = User.Identity.GetUserId();
            }
            var ip = Request.UserHostAddress;
            var resultSesion = _utilManager.AddRegistroSesion(userLogId, accion, objeto, idRegistro, ip);
        }

    }
}