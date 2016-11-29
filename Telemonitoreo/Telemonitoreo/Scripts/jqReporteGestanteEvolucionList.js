function buscar() {
    $("#FechaIHidden").val("");
    $("#FechaFHidden").val("");
    $("#RegionHidden").val("");

    var fechaI = $("#FechaI").val();
    var fechaF = $("#FechaF").val();
    var regionId = $("#RegionId").val();
    var region = $("#RegionId option:selected").text();

    $("#FechaIHidden").val(fechaI);
    $("#FechaFHidden").val(fechaF);
    if (regionId) {
        $("#RegionHidden").val(region);
    }
    
    $("#formGesEvolucionList").submit();
}

$(function () {
    $("#btnBuscar").on("click", buscar);
});