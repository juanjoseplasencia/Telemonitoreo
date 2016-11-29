using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.Reporting.WebForms;

namespace Telemonitoreo.Views.Report
{
    public partial class ReportResumenPorGestanteASPX : System.Web.Mvc.ViewPage<dynamic>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var nroDocumento = ViewData["NroDocumento"].ToString();
            var fechaIni = ViewData["FechaIE"].ToString();
            var fechaFin = ViewData["FechaFE"].ToString();
            var perfil = Convert.ToByte(ViewData["idPerfil"].ToString());
            var establecimiento = Convert.ToInt32(ViewData["idEstablecimiento"].ToString());

            if (string.IsNullOrEmpty(nroDocumento))
            {
                nroDocumento = "00000000";
            }

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

            using (var dc = new DataAccess.TelemonitoreoEntities())
            {
                List<sproc_ReportListadoResumenGestante_Result> resumenGestante;
                if (perfil == 2 && establecimiento > 0)
                {
                    resumenGestante = dc.sproc_ReportListadoResumenGestante().Where(g => g.GestanteNroDocumento == nroDocumento && 
                                                                              g.EstablecimientoId == establecimiento &&
                                                                              (fechaIni == "" || g.FechaRegistro >= fInicial || g.Fecha >= fInicial) &&
                                                                              (fechaFin == "" || g.FechaRegistro <= fFinal || g.Fecha <= fFinal)).ToList();
                }
                else
                {
                    resumenGestante = dc.sproc_ReportListadoResumenGestante().Where(g => g.GestanteNroDocumento == nroDocumento &&
                                                                              (fechaIni == "" || g.FechaRegistro >= fInicial || g.Fecha >= fInicial) &&
                                                                              (fechaFin == "" || g.FechaRegistro <= fFinal || g.Fecha <= fFinal)).ToList();
                }
                
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptResumenPorGestante.rdlc");

                var rdc = new ReportDataSource("DSResumen", resumenGestante);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}