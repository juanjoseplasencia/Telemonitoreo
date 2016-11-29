using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Controllers
{
    public class ReniecController : Controller
    {
        //Call RENIEC WebService
        [HttpGet]
        public PartialViewResult ConsumeReniecApi(string numDni)
        {
            const string strUrl = "http://wsminsa.minsa.gob.pe/WSRENIEC_DNI/SerDNI.asmx";
            const string strMethod = "GetReniec";
            
            var usuario = new ReniecViewModel();
            var attReniecResult = (XNamespace)"http://tempuri.org/" + "GetReniecResult";
            XDocument xmlDoc;

            try
            {
                var ws = new WebServiceAccess(strUrl, strMethod);
                ws.Params.Add("strDNIAuto", "");
                ws.Params.Add("strDNICon", numDni);
                ws.Invoke();
                // you can get result ws.ResultXML or ws.ResultString
                xmlDoc = ws.ResultXml;

            }
            catch (Exception)
            {
                const string strXml = "<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
                                      "<soap:Body> <GetReniecResponse xmlns=\"http://tempuri.org/\"> " +
                                      "<GetReniecResult> <string>0000</string><string>RAFAEL</string><string>GONZALES</string><string>ROSARIO</string><string>92</string>" +
                                      " <string>33</string><string>12</string><string>01</string><string>01</string><string>000</string><string>AMERICA</string>" +
                                      " <string>PERU</string><string>LA LIBERTAD</string><string>TRUJILLO</string><string>TRUJILLO</string><string> </string> " +
                                      "  <string>MZ. P LT. 01 LOS PORTALES</string><string>1</string><string>19531005</string><string>20110322</string><string>N</string>" +
                                      " <string>19064294</string><string>22</string><string> </string> </GetReniecResult> </GetReniecResponse> </soap:Body> " +
                                      " </soap:Envelope>";

                xmlDoc = XDocument.Parse(strXml);
            }

            if (xmlDoc == null || xmlDoc.Root == null) return PartialView(viewName: "ConsumeReniecAPI", model: usuario);

            try
            {
                var attributesReniec = xmlDoc.Descendants(attReniecResult);

                usuario.NumDni = attributesReniec.Elements().ElementAt(21).Value;
                usuario.Nombres = attributesReniec.Elements().ElementAt(3).Value;
                usuario.APaterno = attributesReniec.Elements().ElementAt(1).Value;
                usuario.AMaterno = attributesReniec.Elements().ElementAt(2).Value;
                usuario.Direccion = attributesReniec.Elements().ElementAt(16).Value;
                usuario.GeneroId = Convert.ToInt32(attributesReniec.Elements().ElementAt(17).Value);
                usuario.FechaNacimiento = DateTime.ParseExact(attributesReniec.Elements().ElementAt(18).Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                usuario.CodRetorno = attributesReniec.Elements().ElementAt(0).Value;                

                return PartialView(viewName: "ConsumeReniecAPI", model: usuario);   
            }
            catch (Exception)
            {

                return PartialView(viewName: "ConsumeReniecAPI", model: usuario);
            }
        }
	}
}