using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Controllers
{
    public class GestanteMonitoreoController : BaseController
    {
        GestanteManager gestanteManager = new GestanteManager();

        // GET: Gestante
        public ActionResult Index()
        {
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaGestanteMonitoreo, null);
            ConfigurarMenues();            
            return View();
        }

        // GET: BuscarGestanteMonitoreos
        public ActionResult BuscarGestanteMonitoreos(string numDni)
        {
            int page, records, count;
            string sortColumn, sortDirection;
            page = Convert.ToInt32(Request.Params["page"]);
            records = Convert.ToInt32(Request.Params["rows"]); 
            sortColumn = Request.Params["sidx"] ?? "";
            sortDirection = Request.Params["sord"] ?? "asc";

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaGestanteMonitoreo, null);
            var listaGestanteMonitoreos = gestanteManager.ListarGestanteMonitoreos(numDni, sortColumn, sortDirection, 
                EstablecimientoRestriccion());
            count = listaGestanteMonitoreos.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaGestanteMonitoreos.Skip((page - 1) * records).Take(records)
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
                string numDni = form["GestanteNroDocumento"];
                string sortColumn = !string.IsNullOrWhiteSpace(form["hdSortColumn"]) ? form["hdSortColumn"] : "GestanteMonitoreoId";
                string sortDirection = !string.IsNullOrWhiteSpace(form["hdSortDirection"]) ? form["hdSortDirection"] : "asc";

                RegistrarAccion((byte)AccionSesion.ExportExcel, (byte)ObjetoSesion.ListaGestanteMonitoreo, null);
                var listaGestanteMonitoreos = gestanteManager.ListarGestanteMonitoreos(numDni, sortColumn, sortDirection,
                                    EstablecimientoRestriccion());

                var listaGestanteMonitoreosExcel = from O in listaGestanteMonitoreos
                                              select new
                                              {
                                                  Id = O.GestanteMonitoreoId,
                                                  DNI = O.GestanteNroDocumento,
                                                  PresionSistolica = O.PresionSistolica,
	                                              Presion_Diastolica = O.PresionDiastolica,
	                                              Proteinuria = O.Proteinuria,
	                                              Movimientos_Fetales = O.MovimientosFetales,
	                                              Signos_Alarma = O.SignosAlarma,
	                                              Fecha_Registro = O.FechaRegistro.Value.ToShortDateString()
                                            };
                ExcelExport.ExportToSpreadsheet(listaGestanteMonitoreosExcel.CopyToDataTable(), "GestanteMonitoreos");
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e;
                return PartialView("Error");
            }
        }

        // POST: GestanteMonitoreo/Crear
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Crear(string tramaTelefono, string tramaDatos)
        {
            int gestanteKey;
            string gestanteNroDocumento;
            string gestanteTelefono = tramaTelefono;
            string[] arregloDatos;
            int presionSistolica;
            int presionDiastolica;
            int proteinuria;
            int movimientosFetales;
            string signosAlarma;
            string mensaje = string.Empty;
            List<string> mensajesAlarma = new List<string>();
            bool esTelefonoRegistrado = false;

            if (!string.IsNullOrWhiteSpace(tramaTelefono))
            {
                if (tramaTelefono.Length < 9)
                {
                    mensaje = "Trama recibida con numero de telefono incorrecto: " + tramaTelefono;
                    NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                    return Content(mensaje);
                }
                if (tramaTelefono.Length > 9)
                    gestanteTelefono = tramaTelefono.Substring(tramaTelefono.Length - 9, 9);

                gestanteKey = gestanteManager.ObtenerGestanteKeyByGestanteTelefono(gestanteTelefono);
                if (gestanteKey == -1)
                {
                    mensaje = "El numero de telefono no esta registrado en el programa.";
                    NotificacionManager.GrabarNotificacion(gestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                esTelefonoRegistrado = true;

                if (string.IsNullOrWhiteSpace(tramaDatos))
                {
                    mensaje = "Trama de datos invalida.";
                    NotificacionManager.GrabarNotificacion(gestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                if (tramaDatos.IndexOf(" ") > -1 && tramaDatos.IndexOf(" ") < tramaDatos.IndexOf(","))
                {
                    tramaDatos = tramaDatos.Substring(tramaDatos.IndexOf(" ") + 1).Trim();
                }

                arregloDatos = tramaDatos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (arregloDatos.Length == 0)
                {
                    mensaje = "Trama de datos invalida.";
                    NotificacionManager.GrabarNotificacion(gestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(tramaDatos))
                {
                    mensaje = "Trama de datos invalida.";
                    NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                    return Content(mensaje);
                }

                if (tramaDatos.IndexOf(" ") > -1 && tramaDatos.IndexOf(" ") < tramaDatos.IndexOf(","))
                {
                    tramaDatos = tramaDatos.Substring(tramaDatos.IndexOf(" ") + 1).Trim();
                }

                arregloDatos = tramaDatos.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (arregloDatos.Length == 0)
                {
                    mensaje = "Trama de datos invalida: " + tramaDatos;
                    NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                    return Content(mensaje);
                }

                gestanteNroDocumento = arregloDatos[0];
                if (string.IsNullOrWhiteSpace(gestanteNroDocumento))
                {
                    mensaje = "El numero de documento enviado es invalido: " + gestanteNroDocumento;
                    NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                    return Content(mensaje);
                }

                gestanteKey = gestanteManager.ObtenerGestanteKeyByGestanteNroDocumento(gestanteNroDocumento);
                if (gestanteKey == -1)
                {
                    mensaje = "El numero de documento enviado no esta registrado en el programa: " + tramaTelefono;
                    NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                    return Content(mensaje);
                }
            
            }

            try
            {
            var gestante = gestanteManager.MostrarGestante(gestanteKey);
            if (esTelefonoRegistrado)
                {
                    if (arregloDatos.Length < 5)
                    {
                        mensaje = "Los valores de monitoreo enviados en la trama de datos estan incompletos.";
                        NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                        return Content(mensaje);
                    }
                    Int32.TryParse(arregloDatos[0], out presionSistolica);
                    Int32.TryParse(arregloDatos[1], out presionDiastolica);
                    Int32.TryParse(arregloDatos[2], out proteinuria);
                    Int32.TryParse(arregloDatos[3], out movimientosFetales);
                    signosAlarma = arregloDatos[4];
                }
            else
                {
                    if (arregloDatos.Length < 6)
                    {
                        mensaje = "Los valores de monitoreo enviados en la trama de datos estan incompletos.";
                        NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                        return Content(mensaje);
                    }
                    Int32.TryParse(arregloDatos[1], out presionSistolica);
                    Int32.TryParse(arregloDatos[2], out presionDiastolica);
                    Int32.TryParse(arregloDatos[3], out proteinuria);
                    Int32.TryParse(arregloDatos[4], out movimientosFetales);
                    signosAlarma = arregloDatos[5];
                }

                bool enviarAlarma = false;

                // validacion presion sistolica
                if (presionSistolica < 10 || presionSistolica > 280)
                {
                    mensaje = "El valor para Presion Sistolica esta fuera del rango valido (10 - 280).";
                    NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                // validacion presion diastolica
                if (presionDiastolica < 10 || presionDiastolica > 280)
                {
                    mensaje = "El valor para Presion Diastolica esta fuera del rango valido (10 - 280).";
                    NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                // validacion proteinuria
                if (proteinuria < 0 || proteinuria > 10)
                {
                    mensaje = "El valor para Proteinuria esta fuera del rango valido (0 - 10).";
                    NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                // validacion movimientos fetales
                if (movimientosFetales < 0 || movimientosFetales > 50)
                {
                    mensaje = "El valor para Movimientos Fetales esta fuera del rango valido (0 - 50).";
                    NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                // validacion signos alarma
                if (signosAlarma.Length > 20 || signosAlarma.Contains("script") || signosAlarma.Contains("insert")
                     || signosAlarma.Contains("update") || signosAlarma.Contains("delete") || signosAlarma.Contains("select")) 
                {
                    mensaje = "El valor para signos de alarma excede a 20 caracteres o incluye caracteres no permitidos";
                    NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Error", mensaje);
                    return Content(mensaje);
                }

                var gestanteMonitoreo = new GestanteMonitoreoModel()
                {
                    GestanteKey = gestanteKey,
                    GestanteNroDocumento = gestante.GestanteNroDocumento,
                    PresionSistolica = presionSistolica,
                    PresionDiastolica = presionDiastolica,
                    Proteinuria = proteinuria,
                    MovimientosFetales = movimientosFetales,
                    SignosAlarma = signosAlarma
                };

                var result = gestanteManager.GrabarGestanteMonitoreo(gestanteMonitoreo);

                // Si presion sistolica > presion sistolica base + 30 ==> Alarma
                if (presionSistolica > gestante.PresionSistolicaBase + 30)
                {
                    mensajesAlarma.Add("PS:" + presionSistolica + "mmHg(" +
                              gestante.PresionSistolicaBase + "mmHg)"); 
                    enviarAlarma = true;                
                }
                // Si presion diastolica > presion diastolica base + 15 ==> Alarma
                if (presionDiastolica > gestante.PresionDiastolicaBase + 15)
                {
                    mensajesAlarma.Add("PD:" + presionDiastolica + "mmHg(" +
                              gestante.PresionDiastolicaBase + "mmHg)");
                    enviarAlarma = true;
                }
                // Si proteinuria >= 1 ==> Alarma
                if (proteinuria >= 1)
                {
                    mensajesAlarma.Add("PR:" + proteinuria);
                    enviarAlarma = true;
                }
                // Si movimientos fetales == 0 ==> Alarma
                if (movimientosFetales == 0)
                {
                    mensajesAlarma.Add("MF:" + movimientosFetales);
                    enviarAlarma = true;
                }

                if (enviarAlarma && mensajesAlarma.Count > 0)
                {
                    var numerosDestino = gestanteManager.ObtenerTelefonosParaAlerta(gestante);
                    string mensajesSecuencia = string.Empty;
                    foreach (var mensajeAlarma in mensajesAlarma)
                    {
                        mensajesSecuencia += mensajeAlarma + ", ";
                    }
                    mensajesSecuencia += "Gestante: " + gestante.GestanteNroDocumento +
                              " Celular: " + gestante.GestanteTelefono + " Reporte: " + result.ToString();
                    NotificacionManager.GrabarNotificacion(numerosDestino, "Msg. Alarma", mensajesSecuencia);
                }
                NotificacionManager.GrabarNotificacion(gestante.GestanteTelefono, "Msg. Confirmacion",
                    "Su reporte de monitoreo se ha registrado correctamente. Numero de reporte: " + result.ToString());

                RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearGestanteMonitoreo, result);
                return Content("OK");
            }
            catch (Exception e) 
            {
                mensaje = "Ha ocurrido un error en la creacion de reporte de monitoreo: " + e.Message;
                NotificacionManager.GrabarNotificacionParaAdminUser("Msg. Error", mensaje);
                return Content(mensaje);
            }
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
