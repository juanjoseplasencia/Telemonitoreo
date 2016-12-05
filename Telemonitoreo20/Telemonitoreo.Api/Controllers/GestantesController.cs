using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using DataAccess;
using Telemonitoreo.Api.Models;

namespace Telemonitoreo.Api.Controllers
{
    public class GestantesController : ApiController
    {
        TelemonitoreoEntities db = new TelemonitoreoEntities();

        [EnableCors(origins: "http://localhost:8000", headers: "*", methods: "*")]
        [ResponseType(typeof(GestanteListaModel))]
        public IHttpActionResult Get()
        {
            var gestantesLista = db.Gestantes.Select(E => new GestanteListaModel()
                {
                GestanteKey = E.GestanteKey,
                GestanteNroDocumento = E.GestanteNroDocumento,
                Nombres = E.Nombres,
                APaterno = E.APaterno,
                AMaterno = E.AMaterno,
                Edad = 0,
                EstablecimientoId = E.EstablecimientoId,
                FechaProbableParto = E.FechaProbableParto,
                GestanteTelefono = E.GestanteTelefono,
                Establecimiento = E.Establecimiento.Descripcion
            }
            ).ToList();
            return Ok(gestantesLista);
        }
    }

}
