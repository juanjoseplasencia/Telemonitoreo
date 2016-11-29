using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text;

namespace ServicioTelemonitoreo
{
    class ProveedorSmsAltiria : IProveedorSms
    {
        string urlHttpApi;
        string domainId;
        string login;
        string password;
        
        public ProveedorSmsAltiria(string providerSms) {
            NameValueCollection cfgSeccion = (NameValueCollection)ConfigurationManager.GetSection(providerSms);
            this.urlHttpApi = cfgSeccion["url"];
            this.domainId = cfgSeccion["domainId"];
            this.login = cfgSeccion["login"];
            this.password = cfgSeccion["passwd"];
        }

        public string Enviar(string numeroDestino, string textoMensaje)
        {
            string lcHtml = string.Empty;
            string lcPostData = "cmd=sendsms&domainId={0}&login={1}&passwd={2}&dest=51" + numeroDestino + "&msg=" + textoMensaje;
            lcPostData = string.Format(lcPostData, this.domainId, this.login, this.password);
            HttpWebRequest loHttp = (HttpWebRequest)WebRequest.Create(this.urlHttpApi);
            byte[] lbPostBuffer = Encoding.GetEncoding("utf-8").GetBytes(lcPostData);
            loHttp.Method = "POST";
            loHttp.ContentType = "application/x-www-form-urlencoded";
            loHttp.ContentLength = lbPostBuffer.Length;
            Stream loPostData = loHttp.GetRequestStream();
            loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
            loPostData.Close();
            HttpWebResponse loWebResponse = (HttpWebResponse)loHttp.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader loResponseStream = new StreamReader(loWebResponse.GetResponseStream(), enc);
            lcHtml = loResponseStream.ReadToEnd();
            loWebResponse.Close();
            loResponseStream.Close();
            return lcHtml;
        }
    }
}
