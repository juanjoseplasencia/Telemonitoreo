using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
using DataAccess;

namespace ServicioTelemonitoreo
{
    public partial class ProcesosManager : ServiceBase
    {
        private Timer timerProcesos = null;
        private static IProveedorSms proveedorSms;
        public static TelemonitoreoEntities dbContext;

        public ProcesosManager()
        {
            LeerConfiguracion();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timerProcesos = new Timer();
            this.timerProcesos.Interval = 60000; //60 segundos
            this.timerProcesos.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerMedReminderTick);
            this.timerProcesos.Start();
            Library.WriteErrorLog("Servicio iniciado");
        }

        protected override void OnStop()
        {
            this.timerProcesos.Stop();
            dbContext = null;
            Library.WriteErrorLog("Servicio detenido");
        }

        private void TimerMedReminderTick(object sender, ElapsedEventArgs e)
        {
            GenerarMensajesCitas(dbContext);
            GenerarMensajesMedicamentos(dbContext);
            GenerarMensajesEducacionales(dbContext);
            GenerarMensajesMonitoreo(dbContext);
            ProcesarMensajesSms(dbContext);
        }

        public void GenerarMensajesCitas(TelemonitoreoEntities dbContext)
        {
            #region proceso recordatorios de citas
            Library.WriteErrorLog("Inicio de Proceso GenerarMensajesCitas()");
            try
            {
                var returnValue = dbContext.sproc_InsertRecordatorioCitas().SingleOrDefault() ?? 0;
                string resultado = returnValue == 1 ? "Se han evaluado recordatorios" : "NO se han evaluado recordatorios";
                Library.WriteErrorLog("Fin de Proceso GenerarMensajesCitas(): " + resultado);
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
            #endregion
        }

        public void GenerarMensajesMedicamentos(TelemonitoreoEntities dbContext)
        {
            #region proceso recordatorios de toma de medicamentos
            Library.WriteErrorLog("Inicio de Proceso GenerarMensajesMedicamentos()");
            try
            {
                var returnValue = dbContext.sproc_InsertRecordatorioMedicamentos().SingleOrDefault() ?? 0;
                Library.WriteErrorLog("Fin de Proceso GenerarMensajesMedicamentos()");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
            #endregion
        }

        public void GenerarMensajesEducacionales(TelemonitoreoEntities dbContext)
        {
            #region proceso mensajes educacionales
            Library.WriteErrorLog("Inicio de Proceso GenerarMensajesEducacionales()");
            try
            {
                var returnValue = dbContext.sproc_InsertEnviarMensajesEducacionales().SingleOrDefault() ?? 0;
                string resultado = returnValue == 1 ? "Se han evaluado mensajes" : "NO se han evaluado mensajes";
                Library.WriteErrorLog("Fin de Proceso GenerarMensajesEducacionales(): " + resultado);
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
            #endregion
        }

        public void GenerarMensajesMonitoreo(TelemonitoreoEntities dbContext)
        {
            #region proceso mensajes de monitoreo
            Library.WriteErrorLog("Inicio de Proceso GenerarMensajesMonitoreo()");
            try
            {
                var returnValue = dbContext.sproc_InsertRecordatorioReporteMonitoreo().SingleOrDefault() ?? 0;
                string resultado = returnValue == 1 ? "Se han evaluado reportes pendientes" : "NO se han evaluado reportes pendientes";
                Library.WriteErrorLog("Fin de Proceso GenerarMensajesMonitoreo(): " + resultado);
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
            #endregion
        }

        public void ProcesarMensajesSms(TelemonitoreoEntities dbContext)
        {
            #region proceso envio de SMS
            Library.WriteErrorLog("Inicio de Proceso ProcesarMensajesSms()");
            try
            {
                var listaSmsPendientes = dbContext.SmsQueues.Where(E => !E.Procesado).ToList();
                foreach (var SmsItem in listaSmsPendientes)
                {
                    string resultadoEnvioSms = proveedorSms.Enviar(SmsItem.NumeroMovil, SmsItem.CuerpoMensaje);
                    if (resultadoEnvioSms.Contains("OK"))
                    {
                        SmsItem.Procesado = true;
                        SmsItem.FechaProceso = DateTime.Now;
                    }
                    else
                    {
                        SmsItem.ErrorProceso = true;
                    }
                    SmsItem.ResultadoProceso = resultadoEnvioSms;
                    dbContext.SaveChanges();
                }
                Library.WriteErrorLog("Fin de Proceso ProcesarMensajesSms()");
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
            #endregion
        }

        private static void LeerConfiguracion()
        {
            try
            {
                string nombreProveedorSms = ConfigurationManager.AppSettings["ProviderSms"].ToString();
                switch (nombreProveedorSms)
                {
                    case "providerSmsAltiria":
                        proveedorSms = new ProveedorSmsAltiria(nombreProveedorSms);
                        break;
                    case "providerSmsMovistar":
                        proveedorSms = new ProveedorSmsMovistar(nombreProveedorSms);
                        break;
                    default:
                        // TODO
                        break;
                }
                dbContext = new TelemonitoreoEntities();
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex);
            }
        }

    }
}
