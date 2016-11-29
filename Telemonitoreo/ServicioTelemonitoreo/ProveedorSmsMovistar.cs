using System;
using System.Collections.Specialized;
using System.Configuration;

namespace ServicioTelemonitoreo
{
    class ProveedorSmsMovistar : IProveedorSms
    {
        string urlHttpApi;

        public ProveedorSmsMovistar(string providerSms)
        {
            NameValueCollection cfgSeccion = (NameValueCollection)ConfigurationManager.GetSection(providerSms);
            this.urlHttpApi = cfgSeccion["url"];
            // TODO
        }

        public string Enviar(string numeroDestino, string textoMensaje)
        {
            // TODO 
            return string.Empty;
        }
    }
}
