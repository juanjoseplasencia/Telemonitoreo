using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;
using Telemonitoreo.Models;

namespace Telemonitoreo.Controllers
{
    public class MensajeEducacionalController : BaseController
    {
        private readonly MensajeEducacionalManager _mensajeEducacionalManager = new MensajeEducacionalManager();
        private readonly EstablecimientoManager _establecimientoManager = new EstablecimientoManager();
        //
        // GET: /MensajeEducacional/
        public ActionResult Index()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ListaMensEducacion, null);
            ViewBag.EstablecimientoId = new SelectList(_establecimientoManager.ListarEstablecimientos(), "EstablecimientoId", "Descripcion");
            return View();
        }

        //
        // GET: /MensajeEducacional/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MensajeEducacional/Create
        public ActionResult Crear()
        {
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.CrearMensEducacion, null);
            return View();
        }

        //
        // POST: /MensajeEducacional/Create
        [HttpPost]
        public JsonResult Crear(MensajeEducacionalViewModel mensajeEducacional)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mensajeEducacional.UsuarioEditor = ObtenerUsuarioKeyLogeado();
                    var result = _mensajeEducacionalManager.GrabarMensaje(mensajeEducacional);

                    switch (result)
                    {
                        case 0:
                            return
                                Json(
                                    new
                                    {
                                        Success = 0,
                                        ex = new Exception("No se pudo registrar el mensaje.").Message.ToString()
                                    });
                        case -1:
                            return
                                Json(
                                    new
                                    {
                                        Success = 0,
                                        ex = new Exception("Ya fueron configurados mensajes para la semana.").Message.ToString()
                                    });
                    }

                    RegistrarAccion((byte)AccionSesion.Crear, (byte)ObjetoSesion.CrearMensEducacion, result);
                    return Json(new { Success = 1, IdMensajeEducacional = result, ex = "" });
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON 
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("No se pudo registrar  el mensaje").Message.ToString() });
        }

        //
        // GET: /MensajeEducacional/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.EditarMensEducacion, null);
            var mensajeEducacional = _mensajeEducacionalManager.MostrarMensajeEducacional((int)id);

            if (mensajeEducacional == null)
            {
                return HttpNotFound();
            }
            ConfigurarMenues();
            return View(mensajeEducacional);
        }

        //
        // POST: /MensajeEducacional/Edit/5
        [HttpPost]
        public JsonResult Edit(MensajeEducacionalViewModel mensajeEducacional)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _mensajeEducacionalManager.GrabarMensaje(mensajeEducacional);

                    if (result < 1)
                        return
                            Json(
                                new
                                {
                                    Success = 0,
                                    ex = new Exception("No se pudo registrar el mensaje.").Message.ToString()
                                });

                    RegistrarAccion((byte)AccionSesion.Actualizar, (byte)ObjetoSesion.EditarMensEducacion, result);
                    return Json(new { Success = 1, IdMensajeEducacional = result, ex = "" });
                }
            }
            catch (Exception ex)
            {
                // If Sucess== 0 then Unable to perform Save/Update Operation and send Exception to View as JSON 
                return Json(new { Success = 0, ex = ex.Message.ToString() });
            }

            return Json(new { Success = 0, ex = new Exception("No se pudo registrar el mensaje.").Message.ToString() });
        } 

        //
        // GET: /MensajeEducacional/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MensajeEducacional/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BuscarMensajeEducacional(string establecimiento, string fechaIni, string fechaFin, string semana)
        {
            var page = Convert.ToInt32(Request.Params["page"]);
            var records = Convert.ToInt32(Request.Params["rows"]);
            var sortColumn = Request.Params["sidx"] ?? "";
            var sortDirection = Request.Params["sord"] ?? "asc";

            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ListaMensEducacion, null);
            var listaMensajesEducacionales = _mensajeEducacionalManager.ListarMensajesEducacionales(establecimiento, fechaIni, fechaFin, semana,sortColumn, sortDirection);
            var count = listaMensajesEducacionales.Count;

            var jsonDataObject = new
            {
                page = page,
                total = (int)Math.Ceiling((double)count / records),
                records = count,
                rows = listaMensajesEducacionales.Skip((page - 1) * records).Take(records)
            };
            return new JsonResult()
            {
                Data = jsonDataObject,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // POST: /MensajeEducacional/Eliminar
        [HttpPost]
        public JsonResult Eliminar(List<int> ids)
        {
            try
            {
                _mensajeEducacionalManager.EliminarMensajeEducacional(ids);
                ids.ForEach(i => RegistrarAccion((byte)AccionSesion.Eliminar, (byte)ObjetoSesion.ListaMensEducacion, i));
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
            return Json(true);
        }
    }
}
