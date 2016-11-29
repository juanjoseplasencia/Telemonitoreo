using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;
using Telemonitoreo.Models;

namespace Telemonitoreo.Controllers
{
    public class GestanteMedicamentoController : BaseController
    {
        private readonly EstablecimientoManager _establecimientoManager = new EstablecimientoManager();
        private readonly UtilManager _utilManager = new UtilManager();
        private readonly GestanteManager _gestanteManager = new GestanteManager();
        //
        // GET: /GestanteMedicamento/
        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaGestanteMedicamento, null);
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            return View();
        }
        public ActionResult Crear()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.CrearGestanteMedicamento, null);
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            //ViewBag.MedicamentoId = new SelectList(_utilManager.ListarMedicamentos(), "EstablecimientoId", "Descripcion");
            return View();
        }

        // POST: /Sales/Create 
        /// <summary> 
        /// This method is used for Creating and Updating  Sales Information  
        /// (Sales Contains: 1.SalesMain and *SalesSub ) 
        /// </summary> 
        /// <param name="gestanteMedicamentos"> 
        /// </param> 
        /// <returns> 
        /// Returns Json data Containing Success Status, New Sales ID and Exeception  
        /// </returns> 
        [HttpPost]
        public JsonResult Crear(GestanteMedicamentoViewModel gestanteMedicamentos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _gestanteManager.GrabarGestanteMedicamento(gestanteMedicamentos);
                    RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearGestanteMedicamento, result);
                    return result > 0 ? Json(new { Success = 1, GestanteMedicamentoId = result, ex = "" }) : Json(new { Success = 0, ex = new Exception("No se pudo registrar los medicamentos.").Message.ToString() });
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON 
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("No se pudo registrar los medicamentos.").Message.ToString() });
        } 

        public ActionResult GetGestanteData(string dni)
        {
            return RedirectToAction(actionName: "GetGestanteData",
                controllerName: "Gestante",
                routeValues: new { numDni = dni });
        }

        public ActionResult GetMedicamentosPorNombre(string medicamento)
        {
            var medicamentos =  _gestanteManager.GetMedicamentosPorNombre(medicamento);
            return Json(medicamentos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuscarGestanteMedicamentos(string numDni, string aPaterno, string aMaterno, string establecimiento, string fechaIni, string fechaFin)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaGestanteMedicamento, null);
            var listaGestanteMedicamentos = _gestanteManager.ListarGestanteMedicamentos(numDni, aPaterno, aMaterno, establecimiento, fechaIni, fechaFin,
                sortColumn, sortDirection, EstablecimientoRestriccion());
            var count = listaGestanteMedicamentos.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaGestanteMedicamentos.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult BuscarGestanteMedDetalle(int idGMed)
        {
            var listaGestanteMedDetalle = _gestanteManager.ListarGestanteMedDetalle(idGMed);
            var count = listaGestanteMedDetalle.Count;

            var jsonDataObject = new
            {
                page = 1,
                total = count,
                records = count,
                rows = listaGestanteMedDetalle.Take(count)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EditarGestanteMedicamento, null);
            var gestanteMedicamento = _gestanteManager.MostrarGestanteMedicamento((int)id);

            if (gestanteMedicamento == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion", gestanteMedicamento.EstablecimientoId);
            return View(gestanteMedicamento);
        }

        [HttpPost]
        public JsonResult Edit(GestanteMedicamentoViewModel gestanteMedicamentos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _gestanteManager.GrabarGestanteMedicamento(gestanteMedicamentos);
                    RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EditarGestanteMedicamento, result);
                    return result > 0 ? Json(new { Success = 1, GestanteMedicamentoId = result, ex = "" }) : Json(new { Success = 0, ex = new Exception("No se pudo registrar los medicamentos.").Message.ToString() });
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON 
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("No se pudo registrar los medicamentos.").Message.ToString() });
        }

        // POST: /GestanteMedicamento/Eliminar
        [HttpPost]
        public JsonResult Eliminar(List<int> ids)
        {
            try
            {
                _gestanteManager.EliminarGestantesMedicamento(ids);
                ids.ForEach(i => RegistrarAccion((byte)AccionSesion.Eliminar, (byte)ObjetoSesion.ListaGestanteMedicamento, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }

	}
}