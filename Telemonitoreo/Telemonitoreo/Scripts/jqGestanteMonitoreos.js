
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "GestanteMonitoreoId" },
        colNames: ['Id', 'DNI', 'Nombres', 'A. Paterno', 'A. Materno', 'Presion Sistolica', 'Presion Diastolica', 'Proteinuria', 'Mov. Fetales',
                    'Signos Alarma'],
        colModel: [
            { name: 'GestanteMonitoreoId', index: 'GestanteMonitoreoId', key: true, width: 50 },
            { name: 'GestanteNroDocumento', index: 'GestanteNroDocumento', width: 70 },
            { name: 'Nombres', index: 'Nombres', width: 100 },
            { name: 'APaterno', index: 'APaterno', width: 100 },
            { name: 'AMaterno', index: 'AMaterno', width: 100 },
            {
                name: 'PresionSistolica', index: 'PresionSistolica', width: 130
            },
            {
                name: 'PresionDiastolica', index: 'PresionDiastolica', width: 130
            },
            {
                name: 'Proteinuria', index: 'Proteinuria', width: 100
            },
            {
                name: 'MovimientosFetales', index: 'MovimientosFetales', width: 130
            },
            {
                name: 'SignosAlarma', index: 'SignosAlarma', width: 130
            }
        ],
        url: getControllerURL('GestanteMonitoreo') + "/BuscarGestanteMonitoreos",
        datatype: 'json',
        postData: {
            "numDni": function () {
                return $('#GestanteNroDocumento').val();
            }
        },
        rowNum: rowsperpage,
        rowList: [30, 60, 1000000],
        page: 1,
        pager: jQuery("#pager"),
        pagerpos: 'center',
        sortname: sortcolumn,
        sortorder: sortorder,
        viewrecords: true,
        multiselect: true,
        emptyrecords: 'No se encontraron reportes de monitoreo'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoGestanteMonitoreos").submit();
}

function eliminar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow')
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea eliminar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("GestanteMonitoreo") + "/Eliminar",
                data: { "ids": selectedIds },
                dataType: "json",
                success: function (data) {
                    if (data == true)
                        buscar();
                    else
                        alertDialog(data, 310);
                },
                error: function () {
                    alertDialog("Ocurrió un error al intentar eliminar las filas seleccionadas");
                },
                traditional: true
            });
        },
        function () {
            return false;
        });
    }
    else {
        alertDialog("Debe seleccionar al menos una fila para eliminar", 220);
        return false;
    }
}

$(function () {
    setUpGrid("GestanteMonitoreoId", "desc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", eliminar);
    $("#btnExportarExcel").on("click", exportarExcel);
});
