using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telemonitoreo.Business;
using Telemonitoreo.Models;
using Telemonitoreo.Enums;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Controllers
{
    public class GestanteController : BaseController
    {
        EstablecimientoManager establecimientoManager = new EstablecimientoManager();
        UtilManager utilManager = new UtilManager();        
        GestanteManager gestanteManager = new GestanteManager();

        private void ConfigurarViewBag(GestanteModel gestante = null)
        {
            if (gestante != null) 
            {
                var listaEntidadesEstablecimiento = establecimientoManager.ListarEstablecimientos();
                ViewBag.EstablecimientoId = new SelectList(listaEntidadesEstablecimiento, "EstablecimientoId", "Descripcion", 
                    gestante.EstablecimientoId);
                ViewBag.EstablecimientoNotificacionId = new SelectList(listaEntidadesEstablecimiento, "EstablecimientoId", "Descripcion",
                    gestante.EstablecimientoNotificacionId);
                ViewBag.DistritoId = new SelectList(utilManager.ListarUbigeos("EsDist"), "CodUbigeo", "Nombre", gestante.DistritoId);            
            }
            else
            {
                var listaEstablecimientos = new SelectList(establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
                ViewBag.EstablecimientoId = listaEstablecimientos;
                ViewBag.EstablecimientoNotificacionId = listaEstablecimientos;
                ViewBag.DistritoId = new SelectList(utilManager.ListarUbigeos("EsDist"), "CodUbigeo", "Nombre");
            }

        }
        
        private List<string> EjecutarValidaciones(GestanteModel gestante)
        {
            int year = DateTime.Today.Year;
            DateTime hoy = DateTime.Today;
            List<string> errores = new List<string>();
            
            if (gestante.GestanteKey == 0 && 
                gestanteManager.ObtenerGestanteKeyByGestanteNroDocumento(gestante.GestanteNroDocumento) > 0)
            {
                errores.Add("Ya existe una gestante registrada en el programa con el DNI ingresado.");
            }
            int gestanteKey = gestanteManager.ObtenerGestanteKeyByGestanteTelefono(gestante.GestanteTelefono);
            if ( gestanteKey > 0 && gestanteKey != gestante.GestanteKey)
            {
                errores.Add("Ya existe una gestante registrada en el programa con el numero de celular ingresado.");
            }
            if (gestante.FechaNacimiento.Value > hoy)
            {
                errores.Add("La fecha de nacimiento no puede ser en el futuro.");
            }
            if (year - gestante.FechaUltimaRegla.Value.Year > 1)
            {
                errores.Add("La fecha de última regla no puede ser anterior a 1 año.");
            }
            if (gestante.FechaUltimaRegla.Value > hoy)
            {
                errores.Add("La fecha de última regla no puede ser en el futuro.");
            }
            if (gestante.FechaUltimaRegla.Value <= gestante.FechaNacimiento.Value)
            {
                errores.Add("La fecha de última regla no puede ser menor que la fecha de nacimiento.");
            }
            if (gestante.FechaProbableParto.Value <= hoy)
            {
                errores.Add("La fecha  probable de parto no puede ser en el pasado.");
            }
            if (gestante.FechaProbableParto.Value <= gestante.FechaUltimaRegla.Value)
            {
                errores.Add("La fecha probable de parto no puede ser menor que la fecha de última regla.");
            }
            if (gestante.PresionSistolicaBase < 10 || gestante.PresionSistolicaBase > 280)
            {
                errores.Add("La presion sistólica base está fuera del rango válido (10 - 280).");
            }
            if (gestante.PresionDiastolicaBase < 10 || gestante.PresionDiastolicaBase > 280)
            {
                errores.Add("La presion diastólica base está fuera del rango válido (10 - 280).");
            }
            return errores;
        }

        // GET: Gestante
        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaGestantes, null);
            ViewBag.EstablecimientoId = new SelectList(establecimientoManager.ListarEstablecimientos(), 
                                            "EstablecimientoId", "Descripcion");            
            return View();
        }

        // GET: Gestante/GetDiagnosticosPorNombre
        public ActionResult GetDiagnosticosPorNombre(string diagnostico)
        {
            var diagnosticos = gestanteManager.GetDiagnosticosPorNombre(diagnostico);
            return Json(diagnosticos, JsonRequestBehavior.AllowGet);
        }

        // GET: Gestante/GetDiagnosticosPorCie10
        public ActionResult GetDiagnosticosPorCie10(string diagnosticoCie10)
        {
            var diagnosticos = gestanteManager.GetDiagnosticosPorCie10(diagnosticoCie10);
            return Json(diagnosticos, JsonRequestBehavior.AllowGet);
        }

        // GET: Gestante/GetRegiones
        public ActionResult GetRegiones(string region)
        {
            var regiones = new SelectList(utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre").Where(A => A.Text.ToLower().Contains(region.ToLower()));
            return Json(regiones, JsonRequestBehavior.AllowGet);
        }

        // GET: Gestante/GetEstablecimientosPorNombre
        public ActionResult GetEstablecimientosPorRegion(string establecimiento, string region)
        {
            var establecimientos = establecimientoManager.BuscarEstablecimientosPorRegion(establecimiento, region);
            return Json(establecimientos, JsonRequestBehavior.AllowGet);
        }

        // GET: Gestante/BuscarGestantes
        public ActionResult BuscarGestantes(string numDni, string aPaterno, string aMaterno, string establecimiento,
                            string telefono)
        {
            int page, records, count;
            string sortColumn, sortDirection;
            page = Convert.ToInt32(Request.Params["page"]);
            records = Convert.ToInt32(Request.Params["rows"]); 
            sortColumn = Request.Params["sidx"] ?? "";
            sortDirection = Request.Params["sord"] ?? "asc";
            int establecimientoId = string.IsNullOrWhiteSpace(establecimiento) ? 0 : Convert.ToInt32(establecimiento);
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaGestantes, null);
            var listaGestantes = gestanteManager.ListarGestantes(numDni, aPaterno, aMaterno, establecimientoId, telefono,
                sortColumn, sortDirection, EstablecimientoRestriccion());
            count = listaGestantes.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaGestantes.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // GET: Gestante/ExportarExcel
        public ActionResult ExportarExcel(FormCollection form)
        {
            try
            {
                string numDni = form["GestanteNroDocumento"];
                string aPaterno = form["APaterno"];
                string aMaterno = form["AMaterno"];
                int establecimientoId = string.IsNullOrWhiteSpace(form["EstablecimientoId"]) ? 0 : 
                                            Convert.ToInt32(form["EstablecimientoId"]);
                string telefono = form["GestanteTelefono"];
                string sortColumn = form["hdSortColumn"] ?? "GestanteKey";
                string sortDirection = form["hdSortDirection"] ?? "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaGestantes, null);
                var listaGestantes = gestanteManager.ListarGestantes(numDni, aPaterno, aMaterno, establecimientoId, telefono,
                    sortColumn, sortDirection, EstablecimientoRestriccion());

                var listaGestantesExcel = from O in listaGestantes
                                        select new
                                        {
                                            Nombres = O.Nombres, 
                                            A_Paterno = O.APaterno, 
                                            A_Materno = O.AMaterno,
                                            Fecha_Probable_Parto = O.FechaProbableParto.Value.ToShortDateString(),
                                            Nro_Celular = O.GestanteTelefono,
                                            Establecimiento = O.Establecimiento
                                        };

                ExcelExport.ExportToSpreadsheet(listaGestantesExcel.CopyToDataTable(), "Gestantes");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }

        // GET: Gestante/Ver/5
        public ActionResult Ver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GestanteModel gestante = gestanteManager.MostrarGestante((int)id);
            if (gestante == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            return View(gestante);
        }

        // GET: Gestante/Crear
        public ActionResult Crear()
        {
            ConfigurarMenues();
            ConfigurarViewBag();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.CrearGestante, null);
            return View();
        }

        // POST: Gestante/Crear
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "GestanteNroDocumento,Nombres,APaterno,AMaterno,FechaNacimiento,EstablecimientoId,EstablecimientoNotificacionId,FechaUltimaRegla," +
                                                  "FechaProbableParto,PresionSistolicaBase,PresionDiastolicaBase,DiagnosticoIngreso,DiagnosticoIntermedio1,DiagnosticoIntermedio2,DiagnosticoEgreso," +
                                                  "GestanteDireccion,GestanteEmail,GestanteTelefono,HorarioMensaje,DistritoId")] GestanteModel gestante)
        {
            foreach (var error in EjecutarValidaciones(gestante))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            if (ModelState.IsValid)
            {
                gestante.UsuarioEditor = ObtenerUsuarioKeyLogeado();
                int newGestanteKey = gestanteManager.GrabarGestante(gestante, 0);
                NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Confirmacion",
                                "Bienvenida. Usted ha sido registrada en el programa de monitoreo a distancia de gestantes con preeclampsia leve. Gracias");
                RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearGestante, newGestanteKey);
                return RedirectToAction("Index");
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestante); 
            return View(gestante);
        }

        // GET: Gestante/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EditarGestante, null);            
            GestanteModel gestante = gestanteManager.MostrarGestante((int)id);
            if (gestante == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestante); 
            return View(gestante);
        }

        // POST: Gestante/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "GestanteKey,GestanteNroDocumento,Nombres,APaterno,AMaterno,FechaNacimiento,EstablecimientoId,EstablecimientoNotificacionId,FechaUltimaRegla," +
                                                   "FechaProbableParto,PresionSistolicaBase,PresionDiastolicaBase,DiagnosticoIngreso,DiagnosticoIntermedio1,DiagnosticoIntermedio2,DiagnosticoEgreso," +
                                                   "GestanteDireccion,GestanteEmail,GestanteTelefono,HorarioMensaje,DistritoId")] GestanteModel gestante)
        {
            foreach (var error in EjecutarValidaciones(gestante))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            if (ModelState.IsValid)
            {
                gestante.UsuarioEditor = ObtenerUsuarioKeyLogeado();
                int updatedGestanteKey = gestanteManager.GrabarGestante(gestante, 1);
                RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EditarGestante, updatedGestanteKey);
                return RedirectToAction("Index");
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestante); 
            return View(gestante);
        }

        // POST: /Gestante/Eliminar
        [HttpPost]
        public JsonResult Eliminar(List<int> ids)
        {
            try
            {
                gestanteManager.EliminarGestantes(ids);
                ids.ForEach(i => RegistrarAccion((byte)AccionSesion.Eliminar, (byte)ObjetoSesion.ListaGestantes, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }

        [HttpGet]
        public PartialViewResult GetGestanteData(string numDni)
        {
            var gestante = gestanteManager.GetGestanteByDni(numDni, EstablecimientoRestriccion());
            return PartialView(viewName: "GetGestanteData", model: gestante);
        }

        public ActionResult GetReniecData(string dni)
        {
            return RedirectToAction(actionName: "ConsumeReniecApi",
                controllerName: "Reniec",
                routeValues: new { numDni = dni });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
