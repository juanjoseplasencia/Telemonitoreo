using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Controllers
{
    public class EstablecimientoController : BaseController
    {
        readonly EstablecimientoManager _establecimientoManager = new EstablecimientoManager();
        readonly UtilManager _utilManager = new UtilManager();
        //
        // GET: /Establecimiento/
        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaEstablecimientos, null);
            ViewBag.Estado = new SelectList(_utilManager.ListarEstados(), "Id", "Descripcion");
            return View();
        }

        public int GetEstablecimientosRenaes()
        {
            RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.RenaesEstablecimientos, null);

            new Thread(() =>
            {
                //Do an advanced looging here which takes a while
                var updateEstablecimientos = _establecimientoManager.ActualizarBaseEstablecimientos("");
            }).Start();

            
            return 1;
        }

        // GET: BuscarGestantes
        public ActionResult BuscarEstablecimientos(string codRenaes, string nomEst, string estado)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaEstablecimientos, null);

            var listaEstablecimientos = _establecimientoManager.BuscarEstablecimientos(codRenaes, nomEst, estado, sortColumn, sortDirection);
            var count = listaEstablecimientos.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaEstablecimientos.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        [HttpPost]
        public JsonResult Deshabilitar(List<int> ids)
        {
            try
            {
                var listaEst =_establecimientoManager.DeshabilitarEstablecimientos(ids);
                listaEst.ForEach(i => RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.ListaEstablecimientos, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Habilitar(List<int> ids)
        {
            try
            {
                var listaEst = _establecimientoManager.HabilitarEstablecimientos(ids);

                foreach (var item in listaEst)
                {
                    RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.ListaEstablecimientos, item);
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }

        public ActionResult ExportarExcel(FormCollection form)
        {
            try
            {
                var codRenaes = form["Codigo"];
                var nomEst = form["Nombre"];
                var estado = string.IsNullOrWhiteSpace(form["Estado"]) ? "" : form["Estado"];
                var sortColumn = form["hdSortColumn"] ?? "UsuarioKey";
                var sortDirection = form["hdSortDirection"] ?? "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaEstablecimientos, null);
                var listaEstablecimientos = _establecimientoManager.BuscarEstablecimientos(codRenaes, nomEst, estado, sortColumn, sortDirection);

                var listaEstablecimientosExcel = from o in listaEstablecimientos
                                                 select new
                                                 {
                                                     COD_RENAES = o.Renaes != null ? o.Renaes.ToString() : "",
                                                     Nombre = o.Descripcion,
                                                     o.Direccion,
                                                     Distrito = o.Distrito,
                                                     Provincia = o.Provincia,
                                                     Region = o.Region,
                                                     o.Estado
                                                 };

                ExcelExport.ExportToSpreadsheet(listaEstablecimientosExcel.CopyToDataTable(), "Establecimientos");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }
	}
}