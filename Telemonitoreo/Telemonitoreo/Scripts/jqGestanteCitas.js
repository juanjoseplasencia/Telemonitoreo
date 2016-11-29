
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "GestanteCitaId" },
        colNames: ['Id', 'DNI', 'Nombres', 'A. Paterno', 'A. Materno', 'Fecha de Cita', 'Hora de Cita','Establecimiento'],
        colModel: [
            {
                name: 'GestanteCitaId', index: 'GestanteCitaId', key: true, width: 50,
                formatter: linkAccion
            },
            { name: 'GestanteNroDocumento', index: 'GestanteNroDocumento', width: 70 },
            { name: 'Nombres', index: 'Nombres', width: 100 },
            { name: 'APaterno', index: 'APaterno', width: 100 },
            { name: 'AMaterno', index: 'AMaterno', width: 100 },
            {
                name: 'FechaCita', index: 'FechaCita', formatter: 'date', width: 120
            },
            {
                name: 'HoraCita', index: 'HoraCita', width: 100
            },
            {
                name: 'Establecimiento', index: 'Establecimiento', width: 150
            }
        ],
        url: getControllerURL('GestanteCita') + "/BuscarGestanteCitas",
        datatype: 'json',
        postData: {
            "numDni": function () {
                return $('#GestanteNroDocumento').val();
            },
            "aPaterno": function () {
                return $('#APaterno').val();
            },
            "aMaterno": function () {
                return $('#AMaterno').val();
            },
            "establecimiento": function () {
                return $('#EstablecimientoId').val();
            },
            "fechaCitaInicio": function () {
                return $("#FechaCitaInicio").val();
            },
            "fechaCitaFin": function () {
                return $("#FechaCitaFin").val();
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
        emptyrecords: 'No se encontraron recordatorios de citas'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function linkAccion(cellValue, options, rowObject) {
    var actionName, tooltip;
    switch (options.colModel.name) {
        case "GestanteCitaId":
            actionName = "/Editar/" + rowObject.GestanteCitaId;
            tooltip = "Editar Recordatorio de Cita";
            break;
    }
    var urlLink = getControllerURL("GestanteCita") + actionName;
    var LinkHtml = "<a href='" + urlLink + "' title='" + tooltip + "'>" + cellValue + "</a>";
    return LinkHtml;
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoGestanteCitas").submit();
}

function eliminar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow')
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea eliminar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("GestanteCita") + "/Eliminar",
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
    setUpGrid("GestanteCitaId", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", eliminar);
    $("#btnExportarExcel").on("click", exportarExcel);
});
