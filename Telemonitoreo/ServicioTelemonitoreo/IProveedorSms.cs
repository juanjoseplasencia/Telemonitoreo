using System;

namespace ServicioTelemonitoreo
{
    interface IProveedorSms
    {
        string Enviar(string numeroDestino, string textoMensaje);
    }
}
