using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.Reporting.WebForms;

namespace Telemonitoreo.Views.Report
{
    public partial class ReportGestanteProcedenciaASPX : System.Web.Mvc.ViewPage<dynamic>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var fechaIni = ViewData["FechaIP"].ToString();
            var fechaFin = ViewData["FechaFP"].ToString();
            var region = ViewData["RegionP"].ToString();
            var perfil = Convert.ToByte(ViewData["idPerfil"].ToString());
            var establecimiento = Convert.ToInt32(ViewData["idEstablecimiento"].ToString());

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
                List<sproc_ReportProcedenciaGestantes_Result> gesCita;
                if (perfil == 2 && establecimiento > 0)
                {
                    gesCita = dc.sproc_ReportProcedenciaGestantes().Where(u => (region == "" || u.departamento == region) &&
                                                                               (fechaIni == "" || u.FechaCreacion >= fInicial) &&
                                                                               (fechaFin == "" || u.FechaCreacion <= fFinal) && u.EstablecimientoId == establecimiento).ToList();
                }
                else
                {
                    gesCita = dc.sproc_ReportProcedenciaGestantes().Where(u => (region == "" || u.departamento == region) &&
                                                                                    (fechaIni == "" || u.FechaCreacion >= fInicial) &&
                                                                                    (fechaFin == "" || u.FechaCreacion <= fFinal)).ToList();                    
                }

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RptProcedenciaGestantes.rdlc");

                var rdc = new ReportDataSource("DSProcedenciaGestante", gesCita);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}