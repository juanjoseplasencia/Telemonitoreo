using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Telemonitoreo.Models;

namespace Telemonitoreo.Business
{
    public class SeguridadManager
    {
        readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public byte ObtenerRoleIdByUserId(string userId)
        {
            var entityObject = _db.AspNetRoles.FirstOrDefault(E => E.AspNetUsers.Any(U => U.Id == userId));
            return (entityObject != null) ? 
                                _db.Roles.FirstOrDefault(E => E.AspNetRoleId == entityObject.Id).RolId 
                                : (byte) 0;
        }

        public int ObtenerUsuarioKeyByUserId(string userId)
        {
            var entityObject = _db.Usuarios.FirstOrDefault(u => u.Id == userId);
            return (entityObject != null) ? entityObject.UsuarioKey : 1;
        }

        public List<MenuModel> ListarMenues(byte rolId)
        {
            var queryObject = _db.Menus.Where(E => E.Rol.Any(R => R.RolId == rolId));
            return queryObject.Project().To<MenuModel>().ToList();
        }
    }
}