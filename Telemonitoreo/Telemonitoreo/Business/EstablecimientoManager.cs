using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class EstablecimientoManager
    {
        private readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public List<Establecimiento> ListarEstablecimientos()
        {
            _db.Configuration.ProxyCreationEnabled = false;
            return _db.Establecimientos.Where(e => e.EstadoId == 1).OrderBy(e => e.Descripcion).ToList();
        }

        public List<ListaEstabViewModel> BuscarEstablecimientos(string codRenaes, string nomEst, string estado, string campoOrden, string direccionOrden)
        {
            _db.Configuration.ProxyCreationEnabled = false;

            var establecimientoFiltro = _db.sproc_GetEstablecimientos().Where(e => (codRenaes == "" || e.Renaes.ToString() == codRenaes) &&
                                                                        (nomEst == "" || e.Descripcion.ToLower().Contains(nomEst.ToLower())) &&
                                                                        (estado == "" || e.EstadoId.ToString() == estado));
            
            if (direccionOrden.ToLower() == "asc")
            {
                switch (campoOrden)
                {
                    case "Renaes":
                        establecimientoFiltro = establecimientoFiltro.OrderBy(e => e.Renaes);
                        break;
                    case "Descripcion":
                        establecimientoFiltro = establecimientoFiltro.OrderBy(e => e.Renaes);
                        break;
                    default:
                        establecimientoFiltro = establecimientoFiltro.OrderBy(e => e.EstablecimientoId);
                        break;
                }
            }
            else
            {
                switch (campoOrden)
                {
                    case "Renaes":
                        establecimientoFiltro = establecimientoFiltro.OrderByDescending(e => e.Renaes);
                        break;
                    case "Descripcion":
                        establecimientoFiltro = establecimientoFiltro.OrderByDescending(e => e.Renaes);
                        break;
                    default:
                        establecimientoFiltro = establecimientoFiltro.OrderByDescending(e => e.EstablecimientoId);
                        break;
                }
            }

            var establecimientoOrden = establecimientoFiltro.ToList();

            var listaEst = from o in establecimientoOrden
                           select new ListaEstabViewModel
                            {
                                EstablecimientoId= o.EstablecimientoId,
                                Renaes = o.Renaes,
                                Descripcion = o.Descripcion,
                                Direccion  = o.Direccion,
                                Estado = o.Estado,
                                Distrito = o.Distrito,
                                Provincia = o.Provincia,
                                Region = o.Region
                            };
            return listaEst.ToList();
        }

        public List<ListaEstabViewModel> BuscarEstablecimientosPorRegion(string establecimiento, string region)
        {
            return BuscarEstablecimientos(string.Empty, establecimiento, string.Empty, string.Empty, string.Empty).Where(E => E.Region.Equals(region)).ToList();
        }

        public int ActualizarBaseEstablecimientos(string listaUbigeos)
        {
            var tableEst = new DataTable();
            tableEst.Columns.Add("Nombre", typeof(string));
            tableEst.Columns.Add("Direccion", typeof(string));
            tableEst.Columns.Add("UsuarioActualizacion", typeof(int));
            tableEst.Columns.Add("CodUbigeo", typeof(string));
            tableEst.Columns.Add("CodRenaes", typeof(string));

            var regiones = ConfigurationManager.AppSettings["Regiones"];

            var listDptos = String.IsNullOrEmpty(regiones) ? new[] { "Huanuco", "Huancavelica", "Amazonas", "Ucayali" } : regiones.Split(',');
            
            var listUbigeo = new List<string>();
            var codDpto = _db.Ubigeos.Where(u => u.EsDpto && listDptos.Contains(u.Nombre)).ToList();

            foreach (var ubigeos in codDpto.Select(dpto => _db.Ubigeos.Where(u => u.CodDpto == dpto.CodDpto && u.EsDist).ToList()))
            {
                listUbigeo.AddRange(ubigeos.Select(u => u.CodUbigeo));
            }

            var wsRnd = new pe.gob.susalud.app12.ServiceSalud();
            foreach (var es in listUbigeo.Select(lu => wsRnd.GetListaEstablecimientoSalud(lu, "", "", "", "")).SelectMany(establecimientos => establecimientos))
            {
                tableEst.Rows.Add(es.EstabNombre, es.EstabDir, 1, es.EstUbigeo, es.CodigoEstSalud);
            }

            if (tableEst.Rows.Count <= 0) return 1;
            try
            {
                var parameter = new SqlParameter("@TableRenaes", SqlDbType.Structured)
                {
                    Value = tableEst,
                    TypeName = "dbo.EstRenaes"
                };

                _db.Database.ExecuteSqlCommand("exec dbo.sproc_UpdateEstFromRenaes @TableRenaes", parameter);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<int> DeshabilitarEstablecimientos(List<int> ids)
        {
            var updated = new List<int>();
            var queryEliminar = from e in _db.Establecimientos
                                where ids.Contains(e.EstablecimientoId)
                                select e;

            if (!queryEliminar.Any()) return null;

            foreach (var item in queryEliminar)
            {
                item.EstadoId = 2;
                updated.Add(item.EstablecimientoId);
            }
            try
            {
                _db.SaveChanges();
                return updated;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<int> HabilitarEstablecimientos(List<int> ids)
        {
            var updated = new List<int>();
            var queryEliminar = from e in _db.Establecimientos
                                where ids.Contains(e.EstablecimientoId)
                                select e;

            if (!queryEliminar.Any()) return null;

            foreach (var item in queryEliminar)
            {
                item.EstadoId = 1;
                updated.Add(item.EstablecimientoId);
            }
            try
            {
                _db.SaveChanges();
                return updated;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}