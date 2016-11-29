using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccess;
using Telemonitoreo.Models;
using Telemonitoreo.Utils;

namespace Telemonitoreo.Business
{
    public static class NotificacionManager
    {
        private static readonly TelemonitoreoEntities _db = new TelemonitoreoEntities();

        public static int GrabarNotificacionParaAdminUser(string tipoMensaje, string cuerpoMensaje)
        {
            try
            {
                var usuarioAdmin = _db.AspNetUsers.FirstOrDefault(e => e.AspNetRoles.Where(r => r.Name.Equals("Administrador")).Any());
                return GrabarNotificacion(usuarioAdmin.PhoneNumber, tipoMensaje, cuerpoMensaje);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        public static int GrabarNotificacion(string numeroMovil, string tipoMensaje, string cuerpoMensaje)
        {
            try
            {
                SmsQueue notificacionSms = new SmsQueue()
                {
                    NumeroMovil = numeroMovil,
                    TipoMensaje = tipoMensaje,
                    CuerpoMensaje = cuerpoMensaje,
					FechaCreacion = DateTime.Now,
					Procesado = false,
					ResultadoProceso = string.Empty,
					ErrorProceso = false
                };
                _db.SmsQueues.Add(notificacionSms);
                _db.SaveChanges();
                return notificacionSms.SmsQueueId;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int GrabarNotificacion(List<string> numerosMovil, string tipoMensaje, string cuerpoMensaje)
        {
            try
            {
                foreach (var numeroMovil in numerosMovil) 
                {
                    SmsQueue notificacionSms = new SmsQueue()
                    {
                        NumeroMovil = numeroMovil,
                        TipoMensaje = tipoMensaje,
                        CuerpoMensaje = cuerpoMensaje,
                        FechaCreacion = DateTime.Now,
                        Procesado = false,
                        ResultadoProceso = string.Empty,
                        ErrorProceso = false
                    };
                    _db.SmsQueues.Add(notificacionSms);
                    _db.SaveChanges();
                
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}