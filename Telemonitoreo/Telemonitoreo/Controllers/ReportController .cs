using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.Reporting.WebForms;
using Telemonitoreo.Business;
using Telemonitoreo.Enums;

namespace Telemonitoreo.Controllers
{
    public class ReportController : BaseController
    {
        readonly UtilManager _utilManager = new UtilManager();

        public ActionResult ReportGestanteParticipante()
        {
            ViewData["FechaI"] = string.Empty;
            ViewData["FechaF"] = string.Empty;
            ViewData["Region"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteGestantesParticipantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        [HttpPost]
        public ViewResult ReportGestanteParticipante(string fechaIHidden, string fechaFHidden, string regionHidden)
        {
            ViewData["FechaI"] = fechaIHidden;
            ViewData["FechaF"] = fechaFHidden;
            ViewData["Region"] = regionHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteGestantesParticipantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        public ActionResult ReportGestanteProcedencia()
        {
            ViewData["FechaIP"] = string.Empty;
            ViewData["FechaFP"] = string.Empty;
            ViewData["RegionP"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteProcedenciaGestantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        [HttpPost]
        public ViewResult ReportGestanteProcedencia(string fechaIHidden, string fechaFHidden, string regionHidden)
        {
            ViewData["FechaIP"] = fechaIHidden;
            ViewData["FechaFP"] = fechaFHidden;
            ViewData["RegionP"] = regionHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteProcedenciaGestantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        public ActionResult ReportGestanteEvolucion()
        {
            ViewData["FechaIE"] = string.Empty;
            ViewData["FechaFE"] = string.Empty;
            ViewData["RegionE"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteListaEvoGestantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        [HttpPost]
        public ViewResult ReportGestanteEvolucion(string fechaIHidden, string fechaFHidden, string regionHidden)
        {
            ViewData["FechaIE"] = fechaIHidden;
            ViewData["FechaFE"] = fechaFHidden;
            ViewData["RegionE"] = regionHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteListaEvoGestantes, null);
            ViewBag.RegionId = new SelectList(_utilManager.ListarUbigeos("EsDpto"), "CodUbigeo", "Nombre");
            return View();
        }

        public ActionResult ReportEvolucionPorGestante()
        {
            ViewData["NroDocumento"] = string.Empty;
            ViewData["FechaIE"] = string.Empty;
            ViewData["FechaFE"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteEvoGestante, null);
            return View();
        }

        [HttpPost]
        public ViewResult ReportEvolucionPorGestante(string nroDocumentoHidden, string fechaIHidden, string fechaFHidden)
        {
            ViewData["NroDocumento"] = nroDocumentoHidden;
            ViewData["FechaIE"] = fechaIHidden;
            ViewData["FechaFE"] = fechaFHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteEvoGestante, null);
            return View();
        }

        public ActionResult ReportMedicacionPorGestante()
        {
            ViewData["NroDocumentoGestante"] = string.Empty;
            ViewData["FechaIE"] = string.Empty;
            ViewData["FechaFE"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteMedicinaGestante, null);
            return View();
        }

        [HttpPost]
        public ViewResult ReportMedicacionPorGestante(string nroDocumentoHidden, string fechaIHidden, string fechaFHidden)
        {
            ViewData["NroDocumentoGestante"] = nroDocumentoHidden;
            ViewData["FechaIE"] = fechaIHidden;
            ViewData["FechaFE"] = fechaFHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteMedicinaGestante, null);
            return View();
        }


        public ActionResult ReportResumenPorGestante()
        {
            ViewData["NroDocumento"] = string.Empty;
            ViewData["FechaIE"] = string.Empty;
            ViewData["FechaFE"] = string.Empty;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Accesar, (byte)ObjetoSesion.ReporteResumenGestante, null);
            return View();
        }

        [HttpPost]
        public ViewResult ReportResumenPorGestante(string nroDocumentoHidden,string fechaIHidden, string fechaFHidden)
        {
            ViewData["NroDocumento"] = nroDocumentoHidden;
            ViewData["FechaIE"] = fechaIHidden;
            ViewData["FechaFE"] = fechaFHidden;
            ViewData["idPerfil"] = ObtenerPerfil().ToString();
            ViewData["idEstablecimiento"] = ObtenerEstablecimiento().ToString();
            ConfigurarMenues();
            RegistrarAccion((byte)AccionSesion.Buscar, (byte)ObjetoSesion.ReporteResumenGestante, null);
            return View();
        }
    }
}