using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using DataAccess;
using Telemonitoreo.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq.Expressions;

namespace Telemonitoreo.Business
{
    public class UsuarioManager
    {
        private TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public List<UsuarioListaModel> ListarUsuarios(string numDni, string aPaterno, string aMaterno, string estado, string establecimiento, string rol, 
                                                      string campoOrden, string direccionOrden)
        {

            var usuariosFiltro = _db.Usuarios.Where(u => (numDni == "" || u.AspNetUsers.UserName == numDni) && (aPaterno == "" || u.APaterno.Contains(aPaterno))
                                             && (aMaterno == "" || u.AMaterno.Contains(aMaterno)) && (estado == "" || u.EstadoId.ToString() == estado)
                                             && (establecimiento == "" || u.EstablecimientoId.ToString() == establecimiento)
                                             && (rol == "" || u.AspNetUsers.AspNetRoles.FirstOrDefault().Name == rol) && (u.Eliminado == false));

            if (direccionOrden.ToLower() == "asc")
            {
                switch (campoOrden)
                {
                    case "UserName":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.AspNetUsers.UserName);
                        break;
                    case "Nombres":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.Nombres);
                        break;
                    case "APaterno":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.APaterno);
                        break;
                    case "AMaterno":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.AMaterno);
                        break;
                    case "Establecimiento":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.Establecimiento.Descripcion);
                        break;
                    case "RoleName":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.AspNetUsers.AspNetRoles.FirstOrDefault().Name);
                        break;
                    case "Estado":
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.Estado.Descripcion);
                        break;
                    default:
                        usuariosFiltro = usuariosFiltro.OrderBy(u => u.UsuarioKey);
                        break;
                }
            }
            else
            {
                switch (campoOrden)
                {
                    case "UserName":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.AspNetUsers.UserName);
                        break;
                    case "Nombres":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.Nombres);
                        break;
                    case "APaterno":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.APaterno);
                        break;
                    case "AMaterno":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.AMaterno);
                        break;
                    case "Establecimiento":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.Establecimiento.Descripcion);
                        break;
                    case "RoleName":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.AspNetUsers.AspNetRoles.FirstOrDefault().Name);
                        break;
                    case "Estado":
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.Estado.Descripcion);
                        break;
                    default:
                        usuariosFiltro = usuariosFiltro.OrderByDescending(u => u.UsuarioKey);
                        break;
                }
            }

            var usuariosOrden = usuariosFiltro.Project().To<UsuarioListaModel>();
            return usuariosOrden.ToList();
        }

        public EditRegisterViewModel MostrarUsuario(int id)
        {
            var usuario = _db.Usuarios.Find(id);

            if (usuario != null)
            {
                var usuarioModel = new EditRegisterViewModel
                {
                    UsuarioKey = usuario.UsuarioKey,
                    UserName = usuario.AspNetUsers.UserName,
                    Nombres = usuario.Nombres,
                    APaterno = usuario.APaterno,
                    AMaterno = usuario.AMaterno,
                    EstablecimientoId = usuario.EstablecimientoId,
                    RoleName = usuario.AspNetUsers.AspNetRoles.FirstOrDefault().Name,
                    UsuarioDireccion = usuario.UsuarioDireccion,
                    EstadoId = usuario.EstadoId,
                    Email = usuario.AspNetUsers.Email,
                    PhoneNumber = usuario.AspNetUsers.PhoneNumber,
                    RecibeAlertas = usuario.RecibeAlertas
                };


                return usuarioModel;
            }
            else
                return null;
        }

        public EditPersonalViewModel MostrarUsuarioLogueado(string userId)
        {
            var usuarioLog = _db.Usuarios.FirstOrDefault(u => u.Id == userId);

            if (usuarioLog == null) return null;
            
            var keyUsuario = usuarioLog.UsuarioKey;

            var usuario = _db.Usuarios.Find(keyUsuario);

            if (usuario == null) return null;
            var usuarioModel = new EditPersonalViewModel
            {
                UsuarioKey = usuario.UsuarioKey,
                UserName = usuario.AspNetUsers.UserName,
                Nombres = usuario.Nombres,
                APaterno = usuario.APaterno,
                AMaterno = usuario.AMaterno,
                EstablecimientoId = usuario.EstablecimientoId,
                UsuarioDireccion = usuario.UsuarioDireccion,
                Email = usuario.AspNetUsers.Email,
                PhoneNumber = usuario.AspNetUsers.PhoneNumber,
                RecibeAlertas = usuario.RecibeAlertas
            };
            return usuarioModel;
        }

        public bool GrabarUsuario(EditRegisterViewModel usuario, string loggedUser)
        {
            var usuarioActual = _db.Usuarios.Find(usuario.UsuarioKey);

            if (usuarioActual == null) return false;
            usuarioActual.Nombres = usuario.Nombres;
            usuarioActual.APaterno = usuario.APaterno;
            usuarioActual.AMaterno = usuario.AMaterno;
            usuarioActual.EstadoId = usuario.EstadoId;
            usuarioActual.RecibeAlertas = usuario.RecibeAlertas;
            usuarioActual.EstablecimientoId = usuario.EstablecimientoId;
            usuarioActual.UsuarioDireccion = usuario.UsuarioDireccion;
            usuarioActual.UsuarioEditor = 1;

            if (loggedUser != "1")
            {
                var usuarioLog = _db.Usuarios.FirstOrDefault(u => u.Id == loggedUser);
                if (usuarioLog != null) usuarioActual.UsuarioEditor = usuarioLog.UsuarioKey;
            }

            _db.Usuarios.AddOrUpdate(usuarioActual);
            _db.SaveChanges();
            return true;
        }

        public bool GrabarUsuarioLogueado(EditPersonalViewModel usuario)
        {
            var usuarioActual = _db.Usuarios.Find(usuario.UsuarioKey);

            if (usuarioActual == null) return false;
            usuarioActual.EstablecimientoId = usuario.EstablecimientoId;
            usuarioActual.UsuarioDireccion = usuario.UsuarioDireccion;
            usuarioActual.RecibeAlertas = usuario.RecibeAlertas;
            usuarioActual.UsuarioEditor = usuario.UsuarioKey;

            _db.Usuarios.AddOrUpdate(usuarioActual);
            _db.SaveChanges();
            return true;
        }

        public int AddUsuario(Usuario usuario, string loggedUser)
        {
            if (loggedUser != "1")
            {
                var usuarioLog = _db.Usuarios.FirstOrDefault(u => u.Id == loggedUser);
                if (usuarioLog != null) usuario.UsuarioEditor = usuarioLog.UsuarioKey;
            }

            _db.Usuarios.Add(usuario);
            _db.SaveChanges();
            
            return usuario.UsuarioKey;
        }

        public void EliminarUsuarios(List<int> ids)
        {
            var queryEliminar = from u in _db.Usuarios where ids.Contains(u.UsuarioKey)
                                select u;

            if (!queryEliminar.Any()) return;

            foreach (var item in queryEliminar)
            {
                item.Eliminado = true;
            }
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        public List<RolMenuViewModel> ListaMenuRol()
        {
            var menusRoles = _db.sproc_GetAllMenuOptions();
            var listaMenu = (from m in menusRoles
                        select new RolMenuViewModel()
                        {
                           MenuId = m.MenuId,
                           Nombre = m.Nombre,
                           Nivel = m.Nivel,
                           MenuPadre = m.MenuPadre,
                           EsBoton = m.EsBoton,
                           Orden = m.Orden,
                           AccesoAdministrador = m.AccesoAdministrador,
                           AccesoPersonal = m.AccesoPersonal,
                           AccesoAnalista = m.AccesoAnalista,
                           AccesoGestante = m.AccesoGestante
                        }).ToList();

            return listaMenu;
        }
    }
}