using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataAccess;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace Telemonitoreo.Business
{
    public class UtilManager
    {
        readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public List<Estado> ListarEstados()
        {
            _db.Configuration.ProxyCreationEnabled = false;
            return _db.Estado.ToList();
        }

        public List<Medicamento> ListarMedicamentos()
        {
            _db.Configuration.ProxyCreationEnabled = false;
            return _db.Medicamentos.ToList();
        }

        public List<Diagnostico> ListarDiagnosticos()
        {
            return _db.Diagnosticos.ToList();
        }

        public List<Ubigeo> ListarUbigeos(string nivel)
        {
            return _db.Ubigeos.FilterByBooleanExpression<Ubigeo>(nivel, true).OrderBy(E => E.Nombre).ToList();
        }

        public List<string> ListarHoras()
        {
            var horas = new List<string>();
            var hora = 700;
            while (hora <= 2000)
            {
                horas.Add(hora.ToString().PadLeft(4,'0'));
                hora += 15;
                if (hora % 100 >= 60)
                {
                    hora += 40;
                }
            }
            return horas;
        }

        public bool AddRegistroSesion( string loggedUser, byte tipoAccion, byte tipoObjeto, int? idRegistro, string origen)
        {
            var keyUsuario = 1;
            try
            {
                if (loggedUser != "1")
                {
                    var usuarioLog = _db.Usuarios.FirstOrDefault(u => u.Id == loggedUser);
                    if (usuarioLog != null) keyUsuario = usuarioLog.UsuarioKey;
                }

                var regSesion = new RegistroEvento()
                {
                    UsuarioKey = keyUsuario,
                    EventoFecha = DateTime.Now,
                    IdTipoAccion = tipoAccion,
                    IdTipoObjeto = tipoObjeto,
                    IdRegistro = idRegistro,
                    Origen = origen
                };

                _db.RegistroEventos.Add(regSesion);
                _db.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }
    }
}