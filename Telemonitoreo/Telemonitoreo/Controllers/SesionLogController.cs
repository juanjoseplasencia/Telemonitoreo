using System;
using System.Linq;
using System.Web.Mvc;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Controllers
{
    public class SesionLogController : BaseController
    {
        readonly SesionManager _sesionManager = new SesionManager();
        //
        // GET: /Establecimiento/
        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaSesiones, null);
            return View();
        }

        public ActionResult FilterIndex()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaSesionesUsuario, null);
            return View();
        }

        // GET: BuscarGestantes
        public ActionResult BuscarSesiones(string numDni, string aPaterno, string aMaterno, string menu, string fechaIni, string fechaFin)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaSesiones, null);

            var listaSesiones = _sesionManager.ListarSesiones(numDni, aPaterno, aMaterno, menu, fechaIni, fechaFin,sortColumn, sortDirection);
            var count = listaSesiones.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaSesiones.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult BuscarSesionesUsuario(string menu, string fechaIni, string fechaFin)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";
            
            var numDni = "ADMIN";
            if (Request.IsAuthenticated)
            {
                numDni = User.Identity.Name;
            }

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaSesionesUsuario, null);

            var listaSesiones = _sesionManager.ListarSesiones(numDni, "", "", menu, fechaIni, fechaFin, sortColumn, sortDirection);
            var count = listaSesiones.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaSesiones.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public ActionResult ExportarExcel(FormCollection form)
        {
            try
            {
                var numDni = form["NroDni"];
                var aPaterno = form["APaterno"];
                var aMaterno = form["AMaterno"];
                var fechaIni = form["FechaI"];
                var fechaFin = form["FechaF"];
                var menu = form["Menu"];
                
                var sortColumn = form["hdSortColumn"] ?? "UsuarioKey";
                var sortDirection = form["hdSortDirection"] ?? "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaSesiones, null);
                var listaSesiones = _sesionManager.ListarSesiones(numDni, aPaterno, aMaterno, menu, fechaIni, fechaFin,sortColumn, sortDirection);

                var listaSesionesExcel = from o in listaSesiones
                                                 select new
                                                 {
                                                     ID_Sesion = o.RegistroEventoKey,
                                                     DNI = o.Dni,
                                                     Nombres = o.Nombre,
                                                     A_Paterno = o.APaterno,
                                                     A_Materno = o.AMaterno,
                                                     o.Accion,
                                                     Menu_Objeto = o.Menu,
                                                     ID_Registro = o.IdRegistro,
                                                     Fecha = o.EventoFecha,
                                                     IP = o.Origen
                                                 };

                ExcelExport.ExportToSpreadsheet(listaSesionesExcel.CopyToDataTable(), "Sesiones");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }

        public ActionResult ExportarExcelUsuario(FormCollection form)
        {
            try
            {
                var fechaIni = form["FechaI"];
                var fechaFin = form["FechaF"];
                var menu = form["Menu"];
                
                var sortColumn = form["hdSortColumn"] ?? "UsuarioKey";
                var sortDirection = form["hdSortDirection"] ?? "asc";

                var numDni = "ADMIN";
                if (Request.IsAuthenticated)
                {
                    numDni = User.Identity.Name;
                }

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaSesionesUsuario, null);
                var listaSesiones = _sesionManager.ListarSesiones(numDni, "", "", menu, fechaIni, fechaFin,sortColumn, sortDirection);

                var listaSesionesExcel = from o in listaSesiones
                                                 select new
                                                 {
                                                     ID_Sesion = o.RegistroEventoKey,
                                                     DNI = o.Dni,
                                                     Nombres = o.Nombre,
                                                     A_Paterno = o.APaterno,
                                                     A_Materno = o.AMaterno,
                                                     o.Accion,
                                                     Menu_Objeto = o.Menu,
                                                     ID_Registro = o.IdRegistro,
                                                     Fecha = o.EventoFecha,
                                                     IP = o.Origen
                                                 };

                ExcelExport.ExportToSpreadsheet(listaSesionesExcel.CopyToDataTable(), "Sesiones de Usuario");
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