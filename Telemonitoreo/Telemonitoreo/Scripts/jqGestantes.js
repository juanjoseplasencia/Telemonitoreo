
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "GestanteKey" },
        colNames: ['GestanteKey', 'DNI', 'Nombres', 'A. Paterno', 'A. Materno', 'Fec. Probable Parto', 'Nro Celular',
                    'Establecimiento'],
        colModel: [
            { name: 'GestanteKey', index: 'GestanteKey', hidden: true, key: true },
            {
                name: 'GestanteNroDocumento', index: 'GestanteNroDocumento', width: 70,
                formatter: linkAccion 
            },
            {
                name: 'Nombres', index: 'Nombres', width: 100,
                formatter: linkAccion 
            },
            { name: 'APaterno', index: 'APaterno', width: 100 },
            { name: 'AMaterno', index: 'AMaterno', width: 100 },
            {
                name: 'FechaProbableParto', index: 'FechaProbableParto', formatter: 'date', width: 120
            },
            {
                name: 'GestanteTelefono', index: 'GestanteTelefono', width: 80
            },
            {
                name: 'Establecimiento', index: 'Establecimiento', width: 150
            }
        ],
        url: getControllerURL('Gestante') + "/BuscarGestantes",
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
            "telefono": function () {
                return $('#GestanteTelefono').val();
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
        emptyrecords: 'No se encontraron gestantes'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function linkAccion(cellValue, options, rowObject) {
    var actionName, tooltip;
    switch (options.colModel.name) {
        case "GestanteNroDocumento":
            actionName = "/Editar/" + rowObject.GestanteKey;
            tooltip = "Editar Gestante";
            break;
        case "Nombres":            
            actionName = "/Ver/" + rowObject.GestanteKey;
            tooltip = "Ver Gestante"
            break;
    }
    var urlLink = getControllerURL("Gestante") + actionName;
    var LinkHtml = "<a href='" + urlLink + "' title='" + tooltip + "'>" + cellValue + "</a>";
    return LinkHtml;
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}
function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoGestantes").submit();
}
function eliminar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow')
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea eliminar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("Gestante") + "/Eliminar",
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
    setUpGrid("GestanteNroDocumento", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", eliminar);
    $("#btnExportarExcel").on("click", exportarExcel);
});