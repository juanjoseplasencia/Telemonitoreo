using System.Web;
using System.Web.Optimization;

namespace Telemonitoreo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.numeric.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include(
                        "~/Scripts/i18n/grid.locale-es.js",
                        "~/Scripts/jquery.jqGrid.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqDatePicker").Include(
                        "~/Scripts/i18n/datepicker-es.js",
                        "~/Scripts/jqDatePicker.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqUtils").Include(
                        "~/Scripts/jqUtils.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestantes").Include(
                        "~/Scripts/jqGestantes.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestantesForm").Include(
                        "~/Scripts/jqGestantesForm.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqUsuarios").Include(
                        "~/Scripts/jqUsuarios.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqSesionLog").Include(
                        "~/Scripts/jqSesionLog.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqSesionLogUsuario").Include(
                        "~/Scripts/jqSesionLogUsuario.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestanteCitas").Include(
                        "~/Scripts/jqGestanteCitas.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestanteCitasForm").Include(
                        "~/Scripts/jqGestanteCitasForm.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestanteMedicamentos").Include(
                        "~/Scripts/jqGestanteMedicamentos.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestanteMedicamentosForm").Include(
                        "~/Scripts/jqGestanteMedicamentosForm.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqGestanteMonitoreos").Include(
                        "~/Scripts/jqGestanteMonitoreos.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqEstablecimientos").Include(
                        "~/Scripts/jqEstablecimientos.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jqMensajeEducacional").Include(
                        "~/Scripts/jqMensajeEducacional.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteGestanteParticipante").Include(
                        "~/Scripts/jqReporteGestanteParticipante.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteEvolucionGestante").Include(
                        "~/Scripts/jqReporteEvolucionGestante.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteMedicacionGestante").Include(
                        "~/Scripts/jqReporteMedicacionGestante.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteGestanteProcedencia").Include(
            "~/Scripts/jqReporteGestanteProcedencia.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteGestanteEvolucionList").Include(
            "~/Scripts/jqReporteGestanteEvolucionList.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqReporteResumenGestante").Include(
            "~/Scripts/jqReporteResumenGestante.js"
            ));
            
            bundles.Add(new ScriptBundle("~/bundles/jqMensajeEducacionalForm").Include(
                        "~/Scripts/jqMensajeEducacionalForm.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/themes/base/jqueryuicss").Include(
                      "~/Content/themes/base/jquery-ui.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqGridcss").Include(
                      "~/Content/ui.jqgrid.css",
                      "~/Content/ui.jqgrid-bootstrap*"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                    "~/Scripts/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                    "~/Scripts/DataTables-1.8.1/media/js/jquery.dataTables.js",
                    "~/Scripts/DataTables-1.8.1/extras/TableTools/media/js/TableTools.js",
                    "~/Scripts/DataTables-1.8.1/extras/TableTools/media/js/ZeroClipboard.js"
            ));

            bundles.Add(new StyleBundle("~/Content/DataTables-1.8.1/extras/TableTools/media/css/css").Include(
                    "~/Content/DataTables-1.8.1/extras/TableTools/media/css/TableTools.css",
                    "~/Content/DataTables-1.8.1/extras/TableTools/media/css/TableTools_JUI.css"
            ));
        }
    }
}
