using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAccess;
using Telemonitoreo.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Telemonitoreo.Business
{
    public class MensajeEducacionalManager
    {
        readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public int GrabarMensaje(MensajeEducacionalViewModel mensajeEducacional)
        {
            try
            {
                var meId = mensajeEducacional.IdMensajeEducacional;

                MensajeEducacional mesEducacional;
                if (meId > 0)
                {
                    mesEducacional = _db.MensajeEducacional.FirstOrDefault(me => me.IdMensajeEducacional == meId);
                    
                    if (mesEducacional == null) return 0;

                    _db.Entry(mesEducacional).State = EntityState.Modified;

                    if (mensajeEducacional.Contenido != null && mensajeEducacional.Contenido.Any())
                    {
                        foreach (var detail in mensajeEducacional.Contenido)
                        {
                            detail.MensajeEducacional = mensajeEducacional;
                            if (detail.IdContenidoMensajeEducacional == 0)
                            {
                                var newCont = new ContenidoMensajeEducacional()
                                {
                                    IdMensajeEducacional = meId,
                                    DiaSemana = detail.DiaSemana,
                                    Contenido = detail.Contenido
                                };
                                mesEducacional.ContenidoMensajeEducacional.Add(newCont);
                            }
                            else
                            {
                                var contMesEducacional = _db.ContenidoMensajeEducacional.Single(x => x.IdContenidoMensajeEducacional == detail.IdContenidoMensajeEducacional);
                                contMesEducacional.DiaSemana = detail.DiaSemana;
                                contMesEducacional.Contenido = detail.Contenido;
                                _db.Entry(contMesEducacional).State = EntityState.Modified;
                            }
                        }
                        
                        if (mesEducacional.ContenidoMensajeEducacional != null)
                        {
                            foreach (var cme in mesEducacional.ContenidoMensajeEducacional.Where(x => mensajeEducacional.Contenido.All(u => u.IdContenidoMensajeEducacional != x.IdContenidoMensajeEducacional)).ToList())
                            {
                                _db.ContenidoMensajeEducacional.Remove(cme);
                            }
                        }
                    }
                    else
                    {
                        mesEducacional.ContenidoMensajeEducacional = null;
                        var contenidoToRemove = _db.ContenidoMensajeEducacional.Where(x => x.IdMensajeEducacional == meId);
                        foreach (var mec in contenidoToRemove)
                        {
                            _db.ContenidoMensajeEducacional.Remove(mec);
                        }
                    }
                    _db.SaveChanges();
                    return meId;
                }

                var mesEnSemana = _db.MensajeEducacional.FirstOrDefault(me => me.SemanaEmbarazo == mensajeEducacional.SemanaEmbarazo);

                if (mesEnSemana != null) return -1;

                mesEducacional = new MensajeEducacional()
                {
                    SemanaEmbarazo  = mensajeEducacional.SemanaEmbarazo,
                    UsuarioEditor = mensajeEducacional.UsuarioEditor,
                    EstadoId = 1,
                    FechaCreacion = DateTime.Now
                };

                foreach (var contenido in mensajeEducacional.Contenido.Select(content => new ContenidoMensajeEducacional()
                {
                    DiaSemana = content.DiaSemana,
                    Contenido = content.Contenido
                }))
                {
                    mesEducacional.ContenidoMensajeEducacional.Add(contenido);
                }

                _db.MensajeEducacional.Add(mesEducacional);
                _db.SaveChanges();
                return mesEducacional.IdMensajeEducacional;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<MensajeEducacionalListViewModel> ListarMensajesEducacionales(string establecimiento, string fechaIni, string fechaFin, string semana, string campoOrden, string direccionOrden)
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

            var menEducacionalFiltro = _db.MensajeEducacional.Where(u => !u.Eliminado && (semana == "" || u.SemanaEmbarazo == semana) && 
                                      (establecimiento == "" || u.Usuario.EstablecimientoId.ToString() == establecimiento) &&
                                                          (fechaIni == "" || u.FechaCreacion >= fInicial) &&
                                                          (fechaFin == "" || u.FechaCreacion <= fFinal));

            if (direccionOrden.ToLower() == "asc")
            {
                switch (campoOrden)
                {
                    case "SemanaEmbarazo":
                        menEducacionalFiltro = menEducacionalFiltro.OrderBy(u => u.SemanaEmbarazo);
                        break;
                    case "UsuarioConfigurador":
                        menEducacionalFiltro = menEducacionalFiltro.OrderBy(u => u.UsuarioEditor);
                        break;
                    case "Establecimiento":
                        menEducacionalFiltro = menEducacionalFiltro.OrderBy(u => u.Usuario.Establecimiento.Descripcion);
                        break;
                    case "FechaCreacion":
                        menEducacionalFiltro = menEducacionalFiltro.OrderBy(u => u.FechaCreacion);
                        break;
                    default:
                        menEducacionalFiltro = menEducacionalFiltro.OrderBy(u => u.IdMensajeEducacional);
                        break;
                }
            }
            else
            {
                switch (campoOrden)
                {
                    case "SemanaEmbarazo":
                        menEducacionalFiltro = menEducacionalFiltro.OrderByDescending(u => u.SemanaEmbarazo);
                        break;
                    case "UsuarioConfigurador":
                        menEducacionalFiltro = menEducacionalFiltro.OrderByDescending(u => u.UsuarioEditor);
                        break;
                    case "Establecimiento":
                        menEducacionalFiltro = menEducacionalFiltro.OrderByDescending(u => u.Usuario.Establecimiento.Descripcion);
                        break;
                    case "FechaCreacion":
                        menEducacionalFiltro = menEducacionalFiltro.OrderByDescending(u => u.FechaCreacion);
                        break;
                    default:
                        menEducacionalFiltro = menEducacionalFiltro.OrderByDescending(u => u.IdMensajeEducacional);
                        break;
                }
            }

            var menEducacionalOrden = menEducacionalFiltro.Project().To<MensajeEducacionalListViewModel>();
            return menEducacionalOrden.ToList();
        }

        public MensajeEducacionalViewModel MostrarMensajeEducacional(int id)
        {
            var mensajeEducacional = _db.MensajeEducacional.Find(id);

            if (mensajeEducacional == null) return null;

            var mensEducacionalModel = new MensajeEducacionalViewModel
            {
                IdMensajeEducacional = mensajeEducacional.IdMensajeEducacional,
                SemanaEmbarazo = mensajeEducacional.SemanaEmbarazo,
                EstadoId = mensajeEducacional.EstadoId,
                UsuarioEditor = mensajeEducacional.UsuarioEditor,
                Contenido = new List<ContenidoMensajeEducacionalViewModel>()
            };


            foreach (var mensContenidoModel in mensajeEducacional.ContenidoMensajeEducacional.Select(mcd => new ContenidoMensajeEducacionalViewModel
            {
                IdContenidoMensajeEducacional = mcd.IdContenidoMensajeEducacional,
                IdMensajeEducacional = mcd.IdMensajeEducacional,
                DiaSemana = mcd.DiaSemana,
                Contenido = mcd.Contenido 
            }))
            {
                mensEducacionalModel.Contenido.Add(mensContenidoModel);
            }

            return mensEducacionalModel;
        }

        public void EliminarMensajeEducacional(List<int> ids)
        {
            var queryEliminar = from E in _db.MensajeEducacional
                                where ids.Contains(E.IdMensajeEducacional)
                                select E;

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

    }
}