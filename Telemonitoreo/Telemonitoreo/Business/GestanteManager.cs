using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Business
{
    public class GestanteManager
    {
        readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public string ObtenerTelefonoGestante(string gestanteNroDocumento)
        {
            var gestante = _db.Gestantes.FirstOrDefault(e => e.GestanteNroDocumento == gestanteNroDocumento);
            return gestante != null ? gestante.GestanteTelefono : string.Empty;
        }

        public int ObtenerGestanteKeyByGestanteNroDocumento(string gestanteNroDocumento)
        {
            var gestante = _db.Gestantes.FirstOrDefault(e => e.GestanteNroDocumento == gestanteNroDocumento && !e.Eliminado.Value);
            return gestante != null ? gestante.GestanteKey : -1;
        }

        public int ObtenerGestanteKeyByGestanteTelefono(string gestanteTelefono)
        {
            var gestante = _db.Gestantes.FirstOrDefault(e => e.GestanteTelefono == gestanteTelefono && !e.Eliminado.Value);
            return gestante != null ? gestante.GestanteKey : -1;
        }

        public List<string> ObtenerTelefonosParaAlerta(GestanteModel gestante)
        {
            var listaTelefonos = _db.Usuarios.Where(e => e.RecibeAlertas == 1 && 
                                            e.EstablecimientoId == gestante.EstablecimientoNotificacionId &&
                                            e.AspNetUsers.PhoneNumber != null &&
                                            e.AspNetUsers.PhoneNumber.Length > 0)
                                            .Select(e => e.AspNetUsers.PhoneNumber);
            return listaTelefonos.ToList() ;
        }

        public List<GestanteListaModel> ListarGestantes(string numDni, string aPaterno, string aMaterno, int establecimientoId, string telefono, 
                                                string campoOrden, string direccionOrden, int establecimientoRestriccion)
        {
            string ordenExpressionFinal = string.Empty;            
            var queryObject = _db.Gestantes.Where(E => (E.Eliminado == false) &&
                (establecimientoRestriccion == 0 || E.EstablecimientoNotificacionId == establecimientoRestriccion) &&
                (numDni.Equals(string.Empty) || E.GestanteNroDocumento == numDni) &&
                (aPaterno.Equals(string.Empty) || E.APaterno.Contains(aPaterno)) &&
                (aMaterno.Equals(string.Empty) || E.AMaterno.Contains(aMaterno)) &&
                (establecimientoId == 0 || E.EstablecimientoId == establecimientoId) &&
                (telefono.Equals(string.Empty) || E.GestanteTelefono.Contains(telefono))
                );
            switch (campoOrden)
            {
                case "Establecimiento":
                    ordenExpressionFinal = "Establecimiento.Descripcion";
                    break;
                default:
                    ordenExpressionFinal = campoOrden;
                    break;
            };
            var queryObjectFinal = queryObject.OrderQueryBy(ordenExpressionFinal, direccionOrden).Project().To<GestanteListaModel>();
            return queryObjectFinal.ToList();
        }

        public GestanteModel MostrarGestante(int id)
        {
            Gestante gestante = _db.Gestantes.Find(id);
            Ubigeo distrito = _db.Ubigeos.FirstOrDefault(E => E.CodUbigeo == gestante.DistritoId);
            Ubigeo provincia = _db.Ubigeos.FirstOrDefault(E => E.CodUbigeo == gestante.ProvinciaId);
            Ubigeo region = _db.Ubigeos.FirstOrDefault(E => E.CodUbigeo == gestante.RegionId);

            if (gestante != null) 
            { 
                 GestanteModel gestanteModel = new GestanteModel();
                 gestanteModel.GestanteKey = gestante.GestanteKey;
                 gestanteModel.GestanteId = gestante.GestanteId;
                 gestanteModel.GestanteNroDocumento = gestante.GestanteNroDocumento;
                 gestanteModel.Nombres = gestante.Nombres;
                 gestanteModel.APaterno = gestante.APaterno;
                 gestanteModel.AMaterno = gestante.AMaterno;
                 gestanteModel.FechaNacimiento = gestante.FechaNacimiento;
                 gestanteModel.EstablecimientoId = gestante.EstablecimientoId;
                 gestanteModel.Establecimiento = gestante.Establecimiento.Descripcion;
                 gestanteModel.EstablecimientoNotificacionId = gestante.EstablecimientoNotificacionId;
                 gestanteModel.EstablecimientoNotificacion = gestante.EstablecimientoNotificacion.Descripcion;
                 gestanteModel.FechaUltimaRegla = gestante.FechaUltimaRegla;
                 gestanteModel.FechaProbableParto = gestante.FechaProbableParto;
                 gestanteModel.PresionSistolicaBase = gestante.PresionSistolicaBase;
                 gestanteModel.PresionDiastolicaBase = gestante.PresionDiastolicaBase;
                 gestanteModel.DiagnosticoIngreso = gestante.DiagnosticoIngreso;
                 gestanteModel.DiagIngreso = gestante.DiagIngreso != null ?
                                                    gestante.DiagIngreso.Descripcion : string.Empty;
                 gestanteModel.DiagIngresoCie10 = gestante.DiagIngreso != null ?
                                                    gestante.DiagIngreso.Id10 : string.Empty;
                 gestanteModel.DiagnosticoIntermedio1 = gestante.DiagnosticoIntermedio1;
                 gestanteModel.DiagIntermedio1 = gestante.DiagIntermedio1 != null ?
                                                    gestante.DiagIntermedio1.Descripcion : string.Empty;
                 gestanteModel.DiagIntermedio1Cie10 = gestante.DiagIntermedio1 != null ?
                                                    gestante.DiagIntermedio1.Id10 : string.Empty;
                 gestanteModel.DiagnosticoIntermedio2 = gestante.DiagnosticoIntermedio2;
                 gestanteModel.DiagIntermedio2 = gestante.DiagIntermedio2 != null ? 
                                                    gestante.DiagIntermedio2.Descripcion : string.Empty;
                 gestanteModel.DiagIntermedio2Cie10 = gestante.DiagIntermedio2 != null ?
                                                    gestante.DiagIntermedio2.Id10 : string.Empty;
                 gestanteModel.DiagnosticoEgreso = gestante.DiagnosticoEgreso;
                 gestanteModel.DiagEgreso = gestante.DiagEgreso != null ?
                                                    gestante.DiagEgreso.Descripcion : string.Empty;
                 gestanteModel.DiagEgresoCie10 = gestante.DiagEgreso != null ?
                                                    gestante.DiagEgreso.Id10 : string.Empty;
                 gestanteModel.DistritoId = gestante.DistritoId;
                 gestanteModel.Distrito = distrito != null ? distrito.Nombre : string.Empty;
                 gestanteModel.ProvinciaId = gestante.ProvinciaId;
                 gestanteModel.Provincia = provincia != null ? provincia.Nombre : string.Empty;
                 gestanteModel.RegionId = gestante.RegionId;
                 gestanteModel.Region = region != null ? region.Nombre : string.Empty;
                 gestanteModel.GestanteDireccion = gestante.GestanteDireccion;
                 gestanteModel.GestanteEmail = gestante.GestanteEmail;
                 gestanteModel.GestanteTelefono = gestante.GestanteTelefono;
                 gestanteModel.HorarioMensaje = gestante.HorarioMensaje;
                 return gestanteModel;
            }
            else
                return null;
        }

        public int GrabarGestante(GestanteModel gestante, short modo)
        {
            return _db.sproc_AddUpdateGestante(modo, 
                            gestante.GestanteKey,                    
                            gestante.GestanteNroDocumento,
                            gestante.Nombres,
                            gestante.APaterno,
                            gestante.AMaterno,
                            gestante.FechaNacimiento,
                            gestante.FechaUltimaRegla,
                            gestante.FechaProbableParto,
                            gestante.PresionSistolicaBase,
                            gestante.PresionDiastolicaBase,
                            gestante.DiagnosticoIngreso,
                            gestante.DiagnosticoIntermedio1,
                            gestante.DiagnosticoIntermedio2,
                            gestante.DiagnosticoEgreso,
                            gestante.EstablecimientoId,
                            gestante.EstablecimientoNotificacionId,
                            gestante.GestanteTelefono,
                            gestante.GestanteDireccion,
                            gestante.GestanteEmail,
                            gestante.DistritoId,
                            string.Empty,
                            string.Empty,
                            gestante.UsuarioEditor,
                            gestante.HorarioMensaje
                            ).SingleOrDefault() ?? 0;
        }

        public void EliminarGestantes(List<int> ids)
        {
            var queryEliminar = from E in _db.Gestantes
                                where ids.Contains(E.GestanteKey)
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

        public List<GestanteCitaListaModel> ObtenerGestanteCitasByGestanteYFecha(int gestanteKey, DateTime? fechaCita)
        {
            var queryObject = _db.GestanteCitas.Where(E => (E.Eliminado == false) &&
                (E.GestanteKey == gestanteKey) &&
                (E.FechaCita == fechaCita));
            return queryObject.Project().To<GestanteCitaListaModel>().ToList();
        }

        public DateTime? ObtenerFechaCitaByGestanteCitaKey(int gestanteCitaKey)
        {
             GestanteCita gestanteCita = _db.GestanteCitas.Find(gestanteCitaKey);
             if (gestanteCita != null)
             {
                 return gestanteCita.FechaCita;
             }
             else
                 return null;
        }

        public List<GestanteCitaListaModel> ListarGestanteCitas(string numDni, string aPaterno, string aMaterno, int establecimientoId, 
                                                    DateTime? fechaCitaInicio, DateTime? fechaCitaFin,
                                                    string campoOrden, string direccionOrden, int establecimientoRestriccion)
        {
            string ordenExpressionFinal = string.Empty;
            var queryObject = _db.GestanteCitas.Where(E => (E.Eliminado.Value == false) && (E.Gestante.Eliminado.Value == false) &&
                (establecimientoRestriccion == 0 || E.Gestante.EstablecimientoNotificacionId == establecimientoRestriccion) &&
                (numDni.Equals(string.Empty) || E.Gestante.GestanteNroDocumento == numDni) &&
                (aPaterno.Equals(string.Empty) || E.Gestante.APaterno.Contains(aPaterno)) &&
                (aMaterno.Equals(string.Empty) || E.Gestante.AMaterno.Contains(aMaterno)) &&
                (establecimientoId == 0 || E.EstablecimientoId == establecimientoId) &&
                (!fechaCitaInicio.HasValue || E.FechaCita >= fechaCitaInicio) &&
                (!fechaCitaFin.HasValue || E.FechaCita <= fechaCitaFin) 
                );
            switch (campoOrden)
            {
                case "GestanteNroDocumento":
                    ordenExpressionFinal = "Gestante.GestanteNroDocumento";
                    break;
                case "Nombres":
                    ordenExpressionFinal = "Gestante.Nombres";
                    break;
                case "APaterno":
                    ordenExpressionFinal = "Gestante.APaterno";
                    break;
                case "AMaterno":
                    ordenExpressionFinal = "Gestante.AMaterno";
                    break;
                case "Establecimiento":
                    ordenExpressionFinal = "Establecimiento.Descripcion"; 
                    break;
                default:
                    ordenExpressionFinal = campoOrden;
                    break;
            };
            var queryObjectFinal = queryObject.OrderQueryBy(ordenExpressionFinal, direccionOrden).Project().To<GestanteCitaListaModel>();
            return queryObjectFinal.ToList();
        }

        public GestanteCitaModel MostrarGestanteCita(int id)
        {
            GestanteCita gestanteCita = _db.GestanteCitas.Find(id);

            if (gestanteCita != null)
            {
                GestanteCitaModel gestanteModel = new GestanteCitaModel();
                gestanteModel.GestanteCitaId = gestanteCita.GestanteCitaId;
                gestanteModel.GestanteNroDocumento = gestanteCita.Gestante.GestanteNroDocumento;
                gestanteModel.GestanteKey = gestanteCita.GestanteKey;
                gestanteModel.FechaCita = gestanteCita.FechaCita;
                gestanteModel.HoraCita = gestanteCita.HoraCita;
                gestanteModel.NombreMedico = gestanteCita.NombreMedico;
                gestanteModel.EstablecimientoId = gestanteCita.EstablecimientoId;
                gestanteModel.Establecimiento = gestanteCita.Establecimiento.Descripcion;
                return gestanteModel;
            }
            else
                return null;
        }


        public int GrabarGestanteCita(GestanteCitaModel gestanteCita, short modo)
        {
            return _db.sproc_AddUpdateGestanteCita(modo,
                            gestanteCita.GestanteCitaId,
                            gestanteCita.GestanteKey,
                            gestanteCita.FechaCita,
                            gestanteCita.HoraCita,
                            gestanteCita.NombreMedico,
                            gestanteCita.EstablecimientoId,
                            gestanteCita.UsuarioEditor
                            ).SingleOrDefault() ?? 0;
        }

        public void EliminarGestanteCitas(List<int> ids)
        {
            var queryEliminar = from E in _db.GestanteCitas
                                where ids.Contains(E.GestanteCitaId)
                                select E;
            if (queryEliminar.Count() > 0)
            {
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

        public List<GestanteMonitoreoListaModel> ListarGestanteMonitoreos(string numDni, 
                                                    string campoOrden, string direccionOrden, int establecimientoRestriccion)
        {
            // TODO : Complete data filters !
            string ordenExpressionFinal = string.Empty;
            var queryObject = _db.GestanteMonitoreos.Where(E =>
                (numDni.Equals(string.Empty) || E.Gestante.GestanteNroDocumento == numDni) &&
                (establecimientoRestriccion == 0 || E.Gestante.EstablecimientoNotificacionId == establecimientoRestriccion) && 
                E.Gestante.Eliminado.Value == false
                );
            switch (campoOrden)
            {
                case "GestanteNroDocumento":
                    ordenExpressionFinal = "Gestante.GestanteNroDocumento";
                    break;
                case "Nombres":
                    ordenExpressionFinal = "Gestante.Nombres";
                    break;
                case "APaterno":
                    ordenExpressionFinal = "Gestante.APaterno";
                    break;
                case "AMaterno":
                    ordenExpressionFinal = "Gestante.AMaterno";
                    break;
                case "Establecimiento":
                    ordenExpressionFinal = "Establecimiento.Descripcion";
                    break;
                default:
                    ordenExpressionFinal = campoOrden;
                    break;
            };

            var queryObjectFinal = queryObject.OrderQueryBy(ordenExpressionFinal, direccionOrden).Project().To<GestanteMonitoreoListaModel>();
            return queryObjectFinal.ToList();
        }

        public int GrabarGestanteMonitoreo(GestanteMonitoreoModel gestanteMonitoreo)
        {
            return _db.sproc_AddUpdateGestanteMonitoreo(
                            gestanteMonitoreo.GestanteKey,
                            gestanteMonitoreo.PresionSistolica,
                            gestanteMonitoreo.PresionDiastolica,
                            gestanteMonitoreo.Proteinuria,
                            gestanteMonitoreo.MovimientosFetales,
                            gestanteMonitoreo.SignosAlarma).SingleOrDefault() ?? 0;
        }

        public GestanteDataViewModel GetGestanteByDni(string numDni, int establecimientoRestriccion)
        {
            var fromDb = _db.Gestantes.FirstOrDefault(g => g.GestanteNroDocumento == numDni && g.Eliminado == false &&
                        (establecimientoRestriccion == 0 || g.EstablecimientoNotificacionId == establecimientoRestriccion));

            var gestante = new GestanteDataViewModel();

            if (fromDb != null)
            {
                gestante.GestanteKey = fromDb.GestanteKey;
                gestante.GestanteNroDocumento = fromDb.GestanteNroDocumento;
                gestante.Nombres = fromDb.Nombres;
                gestante.APaterno = fromDb.APaterno;
                gestante.AMaterno = fromDb.AMaterno;
            };
            return gestante;
        }

        public List<DiagnosticoViewModel> GetDiagnosticosPorNombre(string diagnostico)
        {
            var fromDb = _db.Diagnosticos.Where(e => e.Descripcion.Contains(diagnostico)).Project().To<DiagnosticoViewModel>();
            return fromDb.ToList();
        }

        public List<DiagnosticoViewModel> GetDiagnosticosPorCie10(string diagnosticoCie10)
        {
            var fromDb = _db.Diagnosticos.Where(e => e.Id10.Contains(diagnosticoCie10)).Project().To<DiagnosticoViewModel>();
            return fromDb.ToList();
        }

        public List<MedicamentoViewModel> GetMedicamentosPorNombre(string medicamento)
        {
            var fromDb = _db.Medicamentos.Where(m => m.Descripcion.Contains(medicamento)).Project().To<MedicamentoViewModel>();
            return fromDb.ToList();
        }

        public int GrabarGestanteMedicamento(GestanteMedicamentoViewModel gestanteMedicamentos)
        {
            try
            {
                var gmId = gestanteMedicamentos.GestanteMedicamentoId;

                GestanteMedicamento gestanteMed;
                if (gmId > 0)
                {
                    gestanteMed = _db.GestanteMedicamentos.FirstOrDefault(gm => gm.GestanteMedicamentoId == gmId);

                    if (gestanteMed == null) return 0;
                    
                    gestanteMed.Fecha = gestanteMedicamentos.Fecha;
                    gestanteMed.NombreMedico = gestanteMedicamentos.NombreMedico;
                    gestanteMed.EstablecimientoId = gestanteMedicamentos.EstablecimientoId;
                    _db.Entry(gestanteMed).State = EntityState.Modified;

                    if (gestanteMedicamentos.Medicamentos != null && gestanteMedicamentos.Medicamentos.Any())
                    {
                        foreach (var detail in gestanteMedicamentos.Medicamentos)
                        {
                            detail.GestanteMedicamento = gestanteMedicamentos;
                            if (detail.GestanteMedicamentoDetalleId == 0)
                            {
                                var newMed = new GestanteMedicamentoDetalle()
                                {
                                    GestanteMedicamentoId = gmId,
                                    MedicamentoId = detail.MedicamentoId,
                                    Dosis = detail.Dosis,
                                    Dias = detail.Dias,
                                    Cantidad = detail.Cantidad,
                                    Instrucciones = detail.Instrucciones
                                };
                                gestanteMed.GestanteMedicamentoDetalle.Add(newMed);
                            }
                            else
                            {
                                var gesMedDetalle = _db.GestanteMedicamentoDetalle.Single(x => x.GestanteMedicamentoDetalleId == detail.GestanteMedicamentoDetalleId);
                                gesMedDetalle.MedicamentoId = detail.MedicamentoId;
                                gesMedDetalle.Dosis = detail.Dosis;
                                gesMedDetalle.Dias = detail.Dias;
                                gesMedDetalle.Cantidad = detail.Cantidad;
                                gesMedDetalle.Instrucciones = detail.Instrucciones;
                                _db.Entry(gesMedDetalle).State = EntityState.Modified;
                            }
                        }

                        if (gestanteMed.GestanteMedicamentoDetalle != null)
                        {
                            foreach (var gmr in gestanteMed.GestanteMedicamentoDetalle.Where(x => gestanteMedicamentos.Medicamentos.All(u => u.GestanteMedicamentoDetalleId != x.GestanteMedicamentoDetalleId)).ToList())
                            {
                                _db.GestanteMedicamentoDetalle.Remove(gmr);
                            }
                        }
                    }
                    else
                    {
                        gestanteMed.GestanteMedicamentoDetalle = null;
                        var detailsToRemove = _db.GestanteMedicamentoDetalle.Where(x => x.GestanteMedicamentoId == gmId);
                        foreach (var gdm in detailsToRemove)
                        {
                            _db.GestanteMedicamentoDetalle.Remove(gdm);
                        }
                    }
                    _db.SaveChanges();
                    return gmId;
                }

                gestanteMed = new GestanteMedicamento()
                {
                    GestanteKey = gestanteMedicamentos.GestanteKey,
                    Fecha = gestanteMedicamentos.Fecha,
                    NombreMedico = gestanteMedicamentos.NombreMedico,
                    EstablecimientoId = gestanteMedicamentos.EstablecimientoId
                };

                foreach (var medicamento in gestanteMedicamentos.Medicamentos.Select(med => new GestanteMedicamentoDetalle()
                {
                    MedicamentoId = med.MedicamentoId,
                    Dosis = med.Dosis,
                    Dias = med.Dias,
                    Cantidad = med.Cantidad,
                    Instrucciones = med.Instrucciones
                }))
                {
                    gestanteMed.GestanteMedicamentoDetalle.Add(medicamento);
                }

                _db.GestanteMedicamentos.Add(gestanteMed);
                _db.SaveChanges();
                return gestanteMed.GestanteMedicamentoId;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public List<GestanteMedListViewModel> ListarGestanteMedicamentos(string numDni, string aPaterno, string aMaterno, string establecimiento, string fechaIni, string fechaFin, 
                                              string campoOrden, string direccionOrden, int establecimientoRestriccion)
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

            var gestanteMedFiltro = _db.GestanteMedicamentos.Where(u => !u.Eliminado && (numDni == string.Empty || u.Gestante.GestanteNroDocumento == numDni) && 
									(establecimientoRestriccion == 0 || u.Gestante.EstablecimientoNotificacionId == establecimientoRestriccion) &&
                                    (aPaterno == string.Empty || u.Gestante.APaterno.Contains(aPaterno)) && 
                                    (aMaterno == string.Empty || u.Gestante.AMaterno.Contains(aMaterno)) && 
                                    (establecimiento == string.Empty || u.EstablecimientoId.ToString() == establecimiento)&&
                                    (fechaIni == "" || u.Fecha >= fInicial) &&
                                    (fechaFin == "" || u.Fecha <= fFinal));
            
            if (direccionOrden.ToLower() == "asc")
            {
                switch (campoOrden)
                {
                    case "GestanteDni":
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.Gestante.GestanteNroDocumento);
                        break;
                    case "GestanteAPaterno":
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.Gestante.APaterno);
                        break;
                    case "GestanteAMaterno":
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.Gestante.AMaterno);
                        break;
                    case "Establecimiento":
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.Establecimiento.Descripcion);
                        break;
                    case "Fecha":
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.Fecha);
                        break;
                    default:
                        gestanteMedFiltro = gestanteMedFiltro.OrderBy(u => u.GestanteMedicamentoId);
                        break;
                }
            }
            else
            {
                switch (campoOrden)
                {
                    case "GestanteDni":
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.Gestante.GestanteNroDocumento);
                        break;
                    case "GestanteAPaterno":
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.Gestante.APaterno);
                        break;
                    case "GestanteAMaterno":
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.Gestante.AMaterno);
                        break;
                    case "Establecimiento":
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.Establecimiento.Descripcion);
                        break;
                    case "Fecha":
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.Fecha);
                        break;
                    default:
                        gestanteMedFiltro = gestanteMedFiltro.OrderByDescending(u => u.GestanteMedicamentoId);
                        break;
                }
            }

            var gesMedOrden = gestanteMedFiltro.Project().To<GestanteMedListViewModel>();
            return gesMedOrden.ToList();
        }

        public List<GestanteMedDetalleListViewModel> ListarGestanteMedDetalle(int id)
        {
            var gestanteMedDetalle = _db.GestanteMedicamentoDetalle.Where(md => md.GestanteMedicamentoId == id).Project().To<GestanteMedDetalleListViewModel>();
            return gestanteMedDetalle.ToList();
        }

        public GestanteMedicamentoViewModel MostrarGestanteMedicamento(int id)
        {
            var gestanteMedicamento = _db.GestanteMedicamentos.Find(id);

            if (gestanteMedicamento == null) return null;

            var gestanteMedModel = new GestanteMedicamentoViewModel
            {
                GestanteMedicamentoId = gestanteMedicamento.GestanteMedicamentoId,
                GestanteKey = gestanteMedicamento.GestanteKey,
                EstablecimientoId = gestanteMedicamento.EstablecimientoId,
                Fecha = gestanteMedicamento.Fecha,
                NombreMedico = gestanteMedicamento.NombreMedico,
                GestanteDni = gestanteMedicamento.Gestante.GestanteNroDocumento,
                Nombres = gestanteMedicamento.Gestante.Nombres,
                APaterno = gestanteMedicamento.Gestante.APaterno,
                AMaterno = gestanteMedicamento.Gestante.AMaterno,
                Medicamentos = new List<GestanteMedicamentoDetalleViewModel>()
            };


            foreach (var gestanteMedDetalleModel in gestanteMedicamento.GestanteMedicamentoDetalle.Select(gdm => new GestanteMedicamentoDetalleViewModel
            {
                GestanteMedicamentoDetalleId = gdm.GestanteMedicamentoDetalleId,
                GestanteMedicamentoId = gdm.GestanteMedicamentoId,
                MedicamentoId = gdm.MedicamentoId,
                Descripcion = gdm.Medicamento.Descripcion,
                Dosis = gdm.Dosis,
                Dias = gdm.Dias,
                Cantidad = gdm.Cantidad,
                Instrucciones = gdm.Instrucciones
            }))
            {
                gestanteMedModel.Medicamentos.Add(gestanteMedDetalleModel);
            }

            return gestanteMedModel;
        }

        public void EliminarGestantesMedicamento(List<int> ids)
        {
            var queryEliminar = from E in _db.GestanteMedicamentos
                                where ids.Contains(E.GestanteMedicamentoId)
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