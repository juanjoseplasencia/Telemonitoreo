using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telemonitoreo.Enums
{
    public enum ListaSiNo
    {
        Si = 1,
        No = 2
    }

    public enum EstadoType
    {
        Activo = 1,
        Inactivo = 2,
        Suspendido = 3
    }

    public enum AccionSesion
    {
        [StringValue("Acceso a menú")]
        Accesar = 1,
        [StringValue("Búsqueda de registros")]
        Buscar = 2,
        [StringValue("Nuevo registro")]
        Crear = 3,
        [StringValue("Edición de registro")]
        Actualizar = 4,
        [StringValue("Eliminación de registro")]
        Eliminar = 5,
        [StringValue("Visualización de registro")]
        Ver = 6,
        [StringValue("Exportación a excel")]
        ExportExcel = 7,
        [StringValue("Inicio de sesión")]
        InicioSesion = 8
    }

    public enum ObjetoSesion
    {
        [StringValue("Listado de Establecimientos")]
        ListaEstablecimientos = 1,
        [StringValue("Establecimientos desde RENAES")]
        RenaesEstablecimientos = 2,
        [StringValue("Listado de Sesiones")]
        ListaSesiones = 3,
        [StringValue("Listado de Sesiones de Usuario Actual")]
        ListaSesionesUsuario = 4,
        [StringValue("Listado de Mensajes Educacionales")]
        ListaMensEducacion = 5,
        [StringValue("Creación de Mensajes Educacionales")]
        CrearMensEducacion = 6,
        [StringValue("Edición de Mensajes Educacionales")]
        EditarMensEducacion = 7,
        [StringValue("Administración de Roles")]
        RolesUsuario = 8,
        [StringValue("General")]
        General = 9,
        [StringValue("Creación de Usuarios")]
        CrearUsuario = 10,
        [StringValue("Edición de Usuarios")]
        EdicionUsuario = 11,
        [StringValue("Cambio de Contraseña")]
        EdicionPassword = 12,
        [StringValue("Actualizar de Datos Personales")]
        EdicionDatos = 13,
        [StringValue("Listado de Gestantes")]
        ListaGestantes = 14,
        [StringValue("Creación de Gestantes")]
        CrearGestante = 15,
        [StringValue("Edición de Gestantes")]
        EditarGestante = 16,
        [StringValue("Listado de Citas")]
        ListaGestanteCitas = 17,
        [StringValue("Creación de Citas")]
        CrearGestanteCita = 18,
        [StringValue("Edición de Citas")]
        EditarGestanteCita = 19,
        [StringValue("Listado de Medicamentos Asignados")]
        ListaGestanteMedicamento = 20,
        [StringValue("Asignación de Medicamentos")]
        CrearGestanteMedicamento = 21,
        [StringValue("Edición de Asignación de Medicamentos")]
        EditarGestanteMedicamento = 22,
        [StringValue("Listado de Reportes de Monitoreo")]
        ListaGestanteMonitoreo = 23,
        [StringValue("Creación de Reporte de Monitoreo")]
        CrearGestanteMonitoreo = 24,
        [StringValue("Reporte Listado Evolución de Gestantes")]
        ReporteListaEvoGestantes = 25,
        [StringValue("Reporte Evolución de Gestante")]
        ReporteEvoGestante = 26,
        [StringValue("Reporte de Gestantes Participantes")]
        ReporteGestantesParticipantes = 27,
        [StringValue("Reporte de Medicamentos de Gestante")]
        ReporteMedicinaGestante = 28,
        [StringValue("Reporte de Procedencia de Gestantes")]
        ReporteProcedenciaGestantes = 29,
        [StringValue("Listado de Usuarios")]
        ListaUsuarios = 30,
        [StringValue("Reporte Resumen de Gestante")]
        ReporteResumenGestante = 31
    }

    public class StringValueAttribute : System.Attribute
    {

        private readonly string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }
}