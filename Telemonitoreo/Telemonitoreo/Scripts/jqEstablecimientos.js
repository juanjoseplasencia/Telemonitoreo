
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "EstablecimientoId" },
        colNames: ['EstablecimientoId', 'Cod. Renaes', 'Nombre', 'Dirección', 'Distrito', 'Provincia', 'Región', 'Estado'],
        colModel: [
            { name: 'EstablecimientoId', index: 'EstablecimientoId', hidden: true, key: true },
            { name: 'Renaes', index: 'Renaes', width: 110 },
            { name: 'Descripcion', index: 'Descripcion', width: 250 },
            { name: 'Direccion', index: 'Direccion',sortable: false, width: 350 },
            { name: 'Distrito', index: 'Distrito', sortable: false, width: 150 },
            { name: 'Provincia', index: 'Provincia', sortable: false, width: 150 },
            { name: 'Region', index: 'Region', sortable: false, width: 150 },
            { name: 'Estado', index: 'Estado', sortable: false, width: 100 }
        ],
        url: getControllerURL('Establecimiento') + "/BuscarEstablecimientos",
        datatype: 'json',
        postData: {
            "codRenaes": function () {
                return $('#Codigo').val();
            },
            "nomEst": function () {
                return $('#Nombre').val();
            },
            "estado": function () {
                return $('#Estado').val();
            }
        },
        rowNum: rowsperpage,
        rowList: [10, 60, 1000000],
        page: 1,
        pager: jQuery("#pager"),
        pagerpos: 'center',
        sortname: sortcolumn,
        sortorder: sortorder,
        viewrecords: true,
        multiselect: true,
        emptyrecords: 'No se encontraron establecimientos'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

function RunUpdate() {
    $.ajax({
        url: getControllerURL("Establecimiento") + "/GetEstablecimientosRenaes",
        type: this.method,
        success: function (result) {
            buscar();
        }
    });
}

function deshabilitar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow')
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea deshabilitar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("Establecimiento") + "/Deshabilitar",
                data: { "ids": selectedIds },
                dataType: "json",
                success: function (data) {
                    if (data == true)
                        buscar();
                    else
                        alertDialog(data, 310);
                },
                error: function () {
                    alertDialog("Ocurrió un error al intentar deshabilitar las filas seleccionadas");
                },
                traditional: true
            });
        },
        function () {
            return false;
        });
    }
    else {
        alertDialog("Debe seleccionar al menos una fila para deshabilitar", 220);
        return false;
    }
    return false;
}

function habilitar() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow')
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea habilitar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("Establecimiento") + "/Habilitar",
                data: { "ids": selectedIds },
                dataType: "json",
                success: function (data) {
                    if (data == true)
                        buscar();
                    else
                        alertDialog(data, 310);
                },
                error: function () {
                    alertDialog("Ocurrió un error al intentar habilitar las filas seleccionadas");
                },
                traditional: true
            });
        },
        function () {
            return false;
        });
    }
    else {
        alertDialog("Debe seleccionar al menos una fila para habilitar", 220);
        return false;
    }
    return false;
}

function exportarExcel() {
    $("#hdSortColumn").val($("#gridView").jqGrid('getGridParam', 'sortname'));
    $("#hdSortDirection").val($("#gridView").jqGrid('getGridParam', 'sortorder'));
    $("#ListadoEstablecimientos").submit();
}

$(function () {
    setUpGrid("EstablecimientoId", "asc", 10);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnActualizar").on("click", RunUpdate);
    $("#btnDeshabilitar").on("click", deshabilitar);
    $("#btnHabilitar").on("click", habilitar);
    $("#btnExportarExcel").on("click", exportarExcel);
});
