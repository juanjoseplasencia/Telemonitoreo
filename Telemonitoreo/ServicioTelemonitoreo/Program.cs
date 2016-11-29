using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioTelemonitoreo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

#if DEBUG
            var testService = new ProcesosManager();
            testService.GenerarMensajesCitas(ProcesosManager.dbContext);
            testService.GenerarMensajesMedicamentos(ProcesosManager.dbContext);
            testService.GenerarMensajesEducacionales(ProcesosManager.dbContext);
            testService.ProcesarMensajesSms(ProcesosManager.dbContext);
#else
            var servicesToRun = new ServiceBase[] 
            { 
                new ProcesosManager() 
            };
            ServiceBase.Run(servicesToRun);
#endif

        }
    }
}
