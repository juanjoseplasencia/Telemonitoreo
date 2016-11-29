using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAccess;
using System.Data.SqlClient;
using AutoMapper.QueryableExtensions;
using Telemonitoreo.Models;

namespace Telemonitoreo.Business
{
    public class SesionManager
    {
        readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public List<SesionViewModel> ListarSesiones(string numDni, string aPaterno, string aMaterno, string menu, string fechaIni, string fechaFin, string campoOrden, string direccionOrden)
        {
            var fInicial = DateTime.Now;
            var fFinal = DateTime.Now;

            if (!string.IsNullOrEmpty(fechaIni) && !string.IsNullOrEmpty(fechaFin))
            {
                fInicial = Convert.ToDateTime(fechaIni);
                fFinal = Convert.ToDateTime(fechaFin);
            }
            else if (!string.IsNullOrEmpty(fechaIni) || !string.IsNullOrEmpty(fechaFin))
            {
                if (!string.IsNullOrEmpty(fechaIni))
                { 
                    fInicial = Convert.ToDateTime(fechaIni);
                    fFinal = Convert.ToDateTime(fechaIni);
                    fechaFin = fechaIni;
                }

                if (!string.IsNullOrEmpty(fechaFin))
                {
                    fInicial = Convert.ToDateTime(fechaFin);
                    fFinal = Convert.ToDateTime(fechaFin);
                    fechaIni = fechaFin;
                }   
            }
            
            _db.Configuration.ProxyCreationEnabled = false;
            var sessionLogFiltro = _db.RegistroEventos.Where(u => (numDni == "" || u.Usuario.AspNetUsers.UserName.Contains(numDni)) &&
                                                          (aPaterno == "" || u.Usuario.APaterno.Contains(aPaterno)) &&
                                                          (aMaterno == "" || u.Usuario.AMaterno.Contains(aMaterno)) &&
                                                          (menu == "" || u.TipoObjeto.Descripcion.Contains(menu)) &&
                                                          (menu == "" || u.TipoObjeto.Descripcion.Contains(menu)) &&
                                                          (fechaIni == "" || u.EventoFecha >= fInicial) &&
                                                          (fechaFin == "" || u.EventoFecha <= fFinal));

            if (direccionOrden.ToLower() == "asc")
            {
                switch (campoOrden)
                {
                    case "Dni":
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.Usuario.AspNetUsers.UserName);
                        break;
                    case "APaterno":
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.Usuario.APaterno);
                        break;
                    case "AMaterno":
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.Usuario.AMaterno);
                        break;
                    case "Accion":
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.TipoAccion.Descripcion);
                        break;
                    case "Menu":
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.TipoObjeto.Descripcion);
                        break;
                    default:
                        sessionLogFiltro = sessionLogFiltro.OrderBy(u => u.RegistroEventoKey);
                        break;
                }
            }
            else
            {
                switch (campoOrden)
                {
                    case "Dni":
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.Usuario.AspNetUsers.UserName);
                        break;
                    case "APaterno":
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.Usuario.APaterno);
                        break;
                    case "AMaterno":
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.Usuario.AMaterno);
                        break;
                    case "Accion":
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.TipoAccion.Descripcion);
                        break;
                    case "Menu":
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.TipoObjeto.Descripcion);
                        break;
                    default:
                        sessionLogFiltro = sessionLogFiltro.OrderByDescending(u => u.RegistroEventoKey);
                        break;
                }
            }
            var sessionLogOrden = sessionLogFiltro.Project().To<SesionViewModel>();
            return sessionLogOrden.ToList();
        }

    }
}