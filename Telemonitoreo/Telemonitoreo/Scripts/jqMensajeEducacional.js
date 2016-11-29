
function setUpGrid(sortcolumn, sortorder, rowsperpage) {
    $("#gridView").jqGrid({
        height: 'auto',
        autowidth: true,
        styleUI: 'Bootstrap',
        scrollrows: false,
        hidegrid: false,
        jsonReader: { repeatitems: false, id: "IdMensajeEducacional" },
        colNames: ['ID del Registro', 'Semana de Embarazo', 'Configurado Por', 'Est. de Salud', 'Fecha de Creación'],
        colModel: [
            { name: 'IdMensajeEducacional', index: 'IdMensajeEducacional', key: true, formatter: 'showlink', formatoptions: { baseLinkUrl: 'javascript:', showAction: "LinkEdit('", addParam: "');" } },
            { name: 'SemanaEmbarazo', index: 'SemanaEmbarazo'},
            { name: 'UsuarioConfigurador', index: 'UsuarioConfigurador' },
            { name: 'Establecimiento', index: 'Establecimiento' },
            { name: 'FechaCreacion', index: 'FechaCreacion', formatter: 'date', formatoptions: { srcformat: 'd/m/Y H:i:s', newformat: 'd/m/Y H:i:s' } }
        ],
        url: getControllerURL('MensajeEducacional') + "/BuscarMensajeEducacional",
        datatype: 'json',
        postData: {
            "establecimiento": function () {
                return $('#EstablecimientoId').val();
            },
            "fechaIni": function () {
                return $('#FechaI').val();
            },
            "fechaFin": function () {
                return $('#FechaF').val();
            },
            "semana": function () {
                return $('#Semana').val();
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
        emptyrecords: 'No se encontraron mensajes'
    });
    jQuery("#gridView").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false, search: false, refresh: false });
}

function buscar() {
    $("#gridView").trigger("reloadGrid", [{ page: 1 }]);
}

$(function () {
    setUpGrid("IdMensajeEducacional", "asc", 30);
    var x = $('select.ui-pg-selbox option').eq(2);
    x.text("Show All");
    $("#btnBuscar").on("click", buscar);
    $("#btnEliminar").on("click", deshabilitarMensajes);
});

function LinkEdit(id) {
    var row = id.split("=");
    var row_ID = row[1];
    window.location.href = getControllerURL('MensajeEducacional') + "/Edit/" + row_ID;
}

function deshabilitarMensajes() {
    var selectedIds = $("#gridView").jqGrid('getGridParam', 'selarrrow');
    if (selectedIds.length > 0) {
        confirmDialog("Esta seguro que desea deshabilitar las filas seleccionadas?",
        function () {
            $.ajax({
                type: "POST",
                url: getControllerURL("MensajeEducacional") + "/Eliminar",
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
}
