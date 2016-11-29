using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Telemonitoreo.Models;

namespace Telemonitoreo
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Menu => MenuModel
            AutoMapper.Mapper.CreateMap<Menu, MenuModel>().ReverseMap();
            // Gestante => GestanteListaModel
            AutoMapper.Mapper.CreateMap<Gestante, GestanteListaModel>()
                .ForMember(dto => dto.Edad, conf => conf.MapFrom(e => DateTime.Now.Year - e.FechaNacimiento.Value.Year))                  
                .ForMember(dto => dto.Establecimiento, conf => conf.MapFrom(e => e.Establecimiento.Descripcion));
            // GestanteCita => GestanteCitaListaModel
            AutoMapper.Mapper.CreateMap<GestanteCita, GestanteCitaListaModel>()
                .ForMember(dto => dto.GestanteNroDocumento, conf => conf.MapFrom(e => e.Gestante.GestanteNroDocumento))
                .ForMember(dto => dto.Nombres, conf => conf.MapFrom(e => e.Gestante.Nombres))
                .ForMember(dto => dto.APaterno, conf => conf.MapFrom(e => e.Gestante.APaterno))
                .ForMember(dto => dto.AMaterno, conf => conf.MapFrom(e => e.Gestante.AMaterno))
                .ForMember(dto => dto.Establecimiento, conf => conf.MapFrom(e => e.Establecimiento.Descripcion));
            // GestanteMonitoreo => GestanteMonitoreoListaModel            
            AutoMapper.Mapper.CreateMap<GestanteMonitoreo, GestanteMonitoreoListaModel>()
                .ForMember(dto => dto.GestanteNroDocumento, conf => conf.MapFrom(e => e.Gestante.GestanteNroDocumento))
                .ForMember(dto => dto.Nombres, conf => conf.MapFrom(e => e.Gestante.Nombres))
                .ForMember(dto => dto.APaterno, conf => conf.MapFrom(e => e.Gestante.APaterno))
                .ForMember(dto => dto.AMaterno, conf => conf.MapFrom(e => e.Gestante.AMaterno));
            // Usuario => UsuarioListModel
            AutoMapper.Mapper.CreateMap<Usuario, UsuarioListaModel>()
                .ForMember(dto => dto.UserName, conf => conf.MapFrom(e => e.AspNetUsers.UserName))
                .ForMember(dto => dto.Estado, conf => conf.MapFrom(e => e.Estado.Descripcion))
                .ForMember(dto => dto.RoleName, conf => conf.MapFrom(e => e.AspNetUsers.AspNetRoles.FirstOrDefault().Name))
                .ForMember(dto => dto.Establecimiento, conf => conf.MapFrom(e => e.Establecimiento.Descripcion));
            // Diagnostico => DiagnosticoViewModel
            AutoMapper.Mapper.CreateMap<Diagnostico, DiagnosticoViewModel>()
                .ForMember(dto => dto.DiagnosticoId, conf => conf.MapFrom(e => e.Id))
                .ForMember(dto => dto.Cie10, conf => conf.MapFrom(e => e.Id10))
                .ForMember(dto => dto.Descripcion, conf => conf.MapFrom(e => e.Descripcion));
            // Medicamento => MedicamentoViewModel
            AutoMapper.Mapper.CreateMap<Medicamento, MedicamentoViewModel>()
                .ForMember(dto => dto.MedicamentoId, conf => conf.MapFrom(e => e.MedicamentoId))
                .ForMember(dto => dto.Descripcion, conf => conf.MapFrom(e => e.Descripcion))
                .ForMember(dto => dto.Concentracion, conf => conf.MapFrom(e => e.Concentracion))
                .ForMember(dto => dto.Presentacion, conf => conf.MapFrom(e => e.Presentacion))
                .ForMember(dto => dto.Formato, conf => conf.MapFrom(e => e.Formato));
            // GestanteMedicamento => GestanteMedListViewModel
            AutoMapper.Mapper.CreateMap<GestanteMedicamento, GestanteMedListViewModel>()
                .ForMember(dto => dto.GestanteMedicamentoId, conf => conf.MapFrom(e => e.GestanteMedicamentoId))
                .ForMember(dto => dto.GestanteDni, conf => conf.MapFrom(e => e.Gestante.GestanteNroDocumento))
                .ForMember(dto => dto.GestanteNombres, conf => conf.MapFrom(e => e.Gestante.Nombres))
                .ForMember(dto => dto.GestanteAPaterno, conf => conf.MapFrom(e => e.Gestante.APaterno))
                .ForMember(dto => dto.GestanteAMaterno, conf => conf.MapFrom(e => e.Gestante.AMaterno))
                .ForMember(dto => dto.Establecimiento, conf => conf.MapFrom(e => e.Establecimiento.Descripcion))
                .ForMember(dto => dto.Fecha, conf => conf.MapFrom(e => e.Fecha));
            // GestanteMedicamento => GestanteMedDetalleListViewModel
            AutoMapper.Mapper.CreateMap<GestanteMedicamentoDetalle, GestanteMedDetalleListViewModel>()
                .ForMember(dto => dto.GestanteMedicamentoDetalleId, conf => conf.MapFrom(e => e.GestanteMedicamentoDetalleId))
                .ForMember(dto => dto.Medicamento, conf => conf.MapFrom(e => e.Medicamento.Descripcion))
                .ForMember(dto => dto.Dosis, conf => conf.MapFrom(e => e.Dosis));
            //MensajeEducacional => MensajeEducacionalListViewModel
            AutoMapper.Mapper.CreateMap<MensajeEducacional,MensajeEducacionalListViewModel>()
                .ForMember(dto => dto.IdMensajeEducacional, conf => conf.MapFrom(e => e.IdMensajeEducacional))
                .ForMember(dto => dto.SemanaEmbarazo, conf => conf.MapFrom(e => e.SemanaEmbarazo))
                .ForMember(dto => dto.UsuarioConfigurador, conf => conf.MapFrom(e => e.Usuario.APaterno))
                .ForMember(dto => dto.Establecimiento, conf => conf.MapFrom(e => e.Usuario.Establecimiento.Descripcion))
                .ForMember(dto => dto.FechaCreacion, conf => conf.MapFrom(e => e.FechaCreacion));
            // RegistroEvento => SesionViewModel
            AutoMapper.Mapper.CreateMap<RegistroEvento, SesionViewModel>()
                .ForMember(dto => dto.Dni, conf => conf.MapFrom(e => e.Usuario.AspNetUsers.UserName))
                .ForMember(dto => dto.Nombre, conf => conf.MapFrom(e => e.Usuario.Nombres))
                .ForMember(dto => dto.APaterno, conf => conf.MapFrom(e => e.Usuario.APaterno))
                .ForMember(dto => dto.AMaterno, conf => conf.MapFrom(e => e.Usuario.AMaterno))
                .ForMember(dto => dto.Accion, conf => conf.MapFrom(e => e.TipoAccion.Descripcion))
                .ForMember(dto => dto.Menu, conf => conf.MapFrom(e => e.TipoObjeto.Descripcion));
         }
    }
}