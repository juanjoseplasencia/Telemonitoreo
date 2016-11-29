function buscar() {
    $("#nroDocumentoHidden").val("");
    $("#FechaIHidden").val("");
    $("#FechaFHidden").val("");

    var fechaI = $("#FechaI").val();
    var fechaF = $("#FechaF").val();
    var nroDocumento = $("#GestanteNroDocumento").val();
    $("#nroDocumentoHidden").val(nroDocumento);
    $("#FechaIHidden").val(fechaI);
    $("#FechaFHidden").val(fechaF);
    
    $("#formEvolGestante").submit();
}

$(function () {
    $("#btnBuscar").on("click", buscar);
});