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
    public class GestanteCitaController : BaseController
    {
        EstablecimientoManager establecimientoManager = new EstablecimientoManager();
        UtilManager utilManager = new UtilManager();     
        GestanteManager gestanteManager = new GestanteManager();

        private void ConfigurarViewBag(GestanteCitaModel gestanteCita = null)
        {
            if (gestanteCita != null)
            {
                ViewBag.EstablecimientoId = new SelectList(establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion",
                    gestanteCita.EstablecimientoId);
                ViewBag.Horas = new SelectList(utilManager.ListarHoras(), gestanteCita.FechaCita);
            }
            else
            {
                ViewBag.EstablecimientoId = new SelectList(establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
                ViewBag.Horas = new SelectList(utilManager.ListarHoras());
            }
        }

        private List<string> EjecutarValidaciones(GestanteCitaModel gestanteCita)
        {
            DateTime hoy = DateTime.Today;
            List<string> errores = new List<string>();

            if (gestanteCita.GestanteKey == 0)
            {
                errores.Add("No existe gestante registrada para el DNI ingresado.");
            }

            if (gestanteCita.GestanteCitaId == 0 &&
                gestanteManager.ObtenerGestanteCitasByGestanteYFecha(gestanteCita.GestanteKey, gestanteCita.FechaCita).Any())
            {
                errores.Add("Ya existe una cita programada para la gestante y fecha seleccionadas.");
            }
            if (gestanteCita.GestanteCitaId > 0)
            {
                var fechaCita = gestanteManager.ObtenerFechaCitaByGestanteCitaKey(gestanteCita.GestanteCitaId);
                if (fechaCita != gestanteCita.FechaCita &&
                    gestanteManager.ObtenerGestanteCitasByGestanteYFecha(gestanteCita.GestanteKey, gestanteCita.FechaCita).Any())
                {
                    errores.Add("Ya existe una cita programada para la gestante y fecha seleccionadas.");
                }
            }
            if (gestanteCita.FechaCita.Value <= hoy)
            {
                errores.Add("La fecha de cita no puede ser en el pasado.");
            }
            return errores;
        }

        // GET: GestanteCita
        public ActionResult Index()
        {
            ConfigurarMenues();
            ConfigurarViewBag();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaGestanteCitas, null);
            return View();
        }

        // GET: BuscarGestanteCitas
        public ActionResult BuscarGestanteCitas(string numDni, string aPaterno, string aMaterno, string establecimiento,
                            string fechaCitaInicio, string fechaCitaFin)
        {
            int page, records, count;
            string sortColumn, sortDirection;
            DateTime? fechaInicio = null, fechaFin = null;
            page = Convert.ToInt32(Request.Params["page"]);
            records = Convert.ToInt32(Request.Params["rows"]); 
            sortColumn = Request.Params["sidx"] ?? "";
            sortDirection = Request.Params["sord"] ?? "asc";
            int establecimientoId = string.IsNullOrWhiteSpace(establecimiento) ? 0 : Convert.ToInt32(establecimiento);
            if (!string.IsNullOrWhiteSpace(fechaCitaInicio))
            {
                fechaInicio = Convert.ToDateTime(fechaCitaInicio);
            }
            if (!string.IsNullOrWhiteSpace(fechaCitaFin))
            {
                fechaFin = Convert.ToDateTime(fechaCitaFin);
            }
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaGestanteCitas, null);
            var listaGestanteCitas = gestanteManager.ListarGestanteCitas(numDni, aPaterno, aMaterno, establecimientoId, 
                fechaInicio, fechaFin, sortColumn, sortDirection, EstablecimientoRestriccion());
            count = listaGestanteCitas.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaGestanteCitas.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // GET: GestanteCita/ExportarExcel
        public ActionResult ExportarExcel(FormCollection form)
        {
            try
            {
                DateTime? fechaInicio = null, fechaFin = null;
                string numDni = form["GestanteNroDocumento"];
                string aPaterno = form["APaterno"];
                string aMaterno = form["AMaterno"];
                int establecimientoId = string.IsNullOrWhiteSpace(form["EstablecimientoId"]) ? 0 :
                                            Convert.ToInt32(form["EstablecimientoId"]);
                if (!string.IsNullOrWhiteSpace(form["FechaCitaInicio"]))
                {
                    fechaInicio = Convert.ToDateTime(form["FechaCitaInicio"]);
                }
                if (!string.IsNullOrWhiteSpace(form["FechaCitaFin"]))
                {
                    fechaFin = Convert.ToDateTime(form["FechaCitaFin"]);
                }
                string sortColumn = form["hdSortColumn"] ?? "GestanteCitaId";
                string sortDirection = form["hdSortDirection"] ?? "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaGestanteCitas, null);
                var listaGestanteCitas = gestanteManager.ListarGestanteCitas(numDni, aPaterno, aMaterno, establecimientoId,
                    fechaInicio, fechaFin, sortColumn, sortDirection, EstablecimientoRestriccion());

                var listaGestanteCitasExcel = from O in listaGestanteCitas
                                          select new
                                          {
                                              Id = O.GestanteCitaId,
                                              DNI = O.GestanteNroDocumento,
                                              Nombres = O.Nombres,
                                              A_Paterno = O.APaterno,
                                              A_Materno = O.AMaterno,
                                              Fecha_Cita = O.FechaCita.Value.ToShortDateString(),
                                              Hora_Cita = O.HoraCita,
                                              Establecimiento = O.Establecimiento
                                          };

                ExcelExport.ExportToSpreadsheet(listaGestanteCitasExcel.CopyToDataTable(), "GestanteCitas");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }

        // GET: GestanteCita/Crear
        public ActionResult Crear()
        {
            ConfigurarMenues();
            ConfigurarViewBag();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.CrearGestanteCita,null);
            return View();
        }

        // POST: GestanteCita/Crear
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear([Bind(Include = "GestanteNroDocumento,GestanteKey,FechaCita,HoraCita," + 
                                                  "EstablecimientoId")] GestanteCitaModel gestanteCita)
        {
            foreach (var error in EjecutarValidaciones(gestanteCita))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            if (ModelState.IsValid)
            {
                gestanteCita.UsuarioEditor = ObtenerUsuarioKeyLogeado();
                int newGestanteCitaKey = gestanteManager.GrabarGestanteCita(gestanteCita, 0);
                RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearGestanteCita, newGestanteCitaKey);
                return RedirectToAction("Index");
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestanteCita); 
            return View(gestanteCita);
        }

        // GET: GestanteCita/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EditarGestanteCita, null);                 
            GestanteCitaModel gestanteCita = gestanteManager.MostrarGestanteCita((int)id);
            if (gestanteCita == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestanteCita);
            return View(gestanteCita);
        }

        // POST: Gestante/Editar/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "GestanteCitaId,GestanteNroDocumento,GestanteKey,FechaCita,HoraCita," +
                                                   "EstablecimientoId")] GestanteCitaModel gestanteCita)
        {
            foreach (var error in EjecutarValidaciones(gestanteCita))
            {
                ModelState.AddModelError(string.Empty, error);
            }
            if (ModelState.IsValid)
            {
                gestanteCita.UsuarioEditor = ObtenerUsuarioKeyLogeado();
                int updatedGestanteCitaKey = gestanteManager.GrabarGestanteCita(gestanteCita, 1);
                RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EditarGestanteCita, updatedGestanteCitaKey);
                return RedirectToAction("Index");
            }
            ConfigurarMenues();
            ConfigurarViewBag(gestanteCita);
            return View(gestanteCita);
        }

        // POST: /GestanteCita/Eliminar
        [HttpPost]
        public JsonResult Eliminar(List<int> ids)
        {
            try
            {
                gestanteManager.EliminarGestanteCitas(ids);
                ids.ForEach(i => RegistrarAccion((byte)AccionSesion.Eliminar, (byte)ObjetoSesion.ListaGestanteCitas, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }
        
        public ActionResult GetGestanteData(string dni)
        {
            return RedirectToAction(actionName: "GetGestanteData",
                controllerName: "Gestante",
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
