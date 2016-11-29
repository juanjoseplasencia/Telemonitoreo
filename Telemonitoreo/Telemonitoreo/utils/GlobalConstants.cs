using System;

namespace Telemonitoreo.Utils
{
    public class GlobalConstants
    {
        #region General Constants
        public const string EsPECulture = "es-PE";
        public const String DateTimeFormat = "dd/MM/yy H:mm:ss";
        public const String ShortDateFormat = "dd/MM/yyyy";

        //public const String RegExPhoneNumber = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})[-. ]?(([x][0-9]*)|([0-9]*))?$";
        //public const String RegExSsn = @"^\d{2}-\d{7}$|^\d{3}-\d{2}-\d{4}$";
        //public const String RegExSemiColonEmail = @"^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$";
        //public const String RegExReplacePhoneNumber = "($1) $2-$3 x$4";
        //public const String RegExPhoneNumberNoExt = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        //public const String RegExReplacePhoneNumberNoExt = "($1) $2-$3";
        //public const String RegExRemoveNonDigits = @"[^0-9]";
        //public const String RegExDate = @"^(\d{1,2})\/(\d{1,2})\/(\d{4})$";

        public const String Space = " ";
        public const String Ellipsis = "...";
        public const String SemiColon = ";";
        public const char SemiColonChar = ';';
        public const String Colon = ",";
        public const char ColonChar = ',';
        public const char PeriodChar = '.';
        public const char Dot = '.';

        public const String AdminMenuId = "AdminMenuId";
        public const String PerMenuId = "PerMenuId";
        public const String AnMenuId = "AnMenuId";
        public const String GesMenuId = "GesMenuId";

        public const String SeleccioneEstablecimiento = "Seleccione un establecimiento";
        public const String SeleccioneDistrito = "Seleccione un distrito";
        public const String SeleccioneRegion = "Seleccione una región";
        public const String SeleccioneDiagnostico = "Seleccione un diagnóstico";
        public const String SeleccioneRol = "Seleccione un rol";
        public const String SeleccioneEstado = "Seleccione el estado";
        public const String SeleccioneHora = "Seleccione la hora";
        public const String SeleccioneHorario = "Seleccione el horario";
        public const String SeleccioneDia = "Seleccione el día";

        public const String TurnoAm = "Mañana (8-13)";
        public const String TurnoPm = "Tarde (14-18)";

        public const String Dia1 = "Lunes ";
        public const String Dia2 = "Martes";
        public const String Dia3 = "Miércoles";
        public const String Dia4 = "Jueves";
        public const String Dia5 = "Viernes";
        public const String Dia6 = "Sábado";
        public const String Dia7 = "Domingo";
        
        #endregion

        #region Areas, Controllers and View Names

        #endregion

    }
}